using System;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;
using NuGet.Versioning;
using Nuke.Common;
using Nuke.Common.ChangeLog;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitHub;
using Nuke.Common.Utilities.Collections;
using Octokit;
using Serilog;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;

[GitHubActions(
    "build",
    GitHubActionsImage.UbuntuLatest,
    AutoGenerate = false,
    FetchDepth = 0,
    OnPushBranches = new[] { "**" },
    InvokedTargets = new[] { nameof(Test) },
    EnableGitHubToken = true)]
[GitHubActions(
    "release",
    GitHubActionsImage.UbuntuLatest,
    AutoGenerate = false,
    FetchDepth = 0,
    OnPushTags = new[] { "v[0-9]+.[0-9]+.[0-9]+" },
    InvokedTargets = new[] { nameof(PrepareGitHubRelease) },
    EnableGitHubToken = true,
    ImportSecrets = new[] { nameof(NuGetApiKey) })]
class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.Pack);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")] readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Parameter("NuGet API Key"), Secret] readonly string NuGetApiKey;

    [Solution(GenerateProjects = true)] readonly Solution Solution;

    readonly AbsolutePath PublishDirectory = (AbsolutePath)Path.Combine(RootDirectory, "publish");

    [GitRepository] readonly GitRepository GitRepository;

    protected override void OnBuildInitialized()
    {
        // Validate latest version in changelog file matches the version tag in git
        Assert.True(ChangelogTasksExtensions.TryGetLatestVersionInChangelog(RootDirectory / "CHANGELOG.md", out SemanticVersion version, out string rawVersionValue) && version == GitRepository.GetLatestVersionTag(),
            $"Latest version '{rawVersionValue}' in the changelog file does not match the version tag '{GitRepository.GetLatestVersionTag()}'!");
    }

    Target Clean => _ => _
        .Executes(() =>
        {
            EnsureCleanDirectory(PublishDirectory);
            DotNetTasks.DotNetClean(s => s
                .SetProject(Solution));
        });

    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetTasks.DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuildSettings settings = new DotNetBuildSettings()
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore();

            if (GitRepository.CurrentCommitHasVersionTag())
            {
                SemanticVersion version = GitRepository.GetLatestVersionTag();

                settings = settings
                    .SetVersion(version.ToString())
                    .SetAssemblyVersion($"{version.Major}.0.0.0") // See https://learn.microsoft.com/en-us/dotnet/standard/library-guidance/versioning
                    .SetInformationalVersion(version.ToString())
                    .SetFileVersion(version.ToString())
                    .SetCopyright($"Copyright {DateTime.UtcNow.Year} (c) Sandro Figo");
            }

            DotNetTasks.DotNetBuild(settings);
        });

    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTasks.DotNetTest(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetLoggers("trx;logfilename=test-results.trx")
                .EnableNoRestore()
                .EnableNoBuild());
        });

    Target Pack => _ => _
        .DependsOn(Test)
        .OnlyWhenStatic(() => GitRepository.CurrentCommitHasVersionTag())
        .Produces(PublishDirectory / "*.nupkg")
        .Executes(() =>
        {
            SemanticVersion version = GitRepository.GetLatestVersionTag();

            Log.Information("Version: {Version}", version);

            DotNetTasks.DotNetPack(s => s
                .SetProject(Solution.VoxReader)
                .SetConfiguration(Configuration)
                .SetOutputDirectory(PublishDirectory)
                .SetVersion(version.ToString())
                .EnableNoRestore()
                .EnableNoBuild());
        });

    Target PrepareGitHubRelease => _ => _
        .Consumes(Pack)
        .DependsOn(Pack)
        .Triggers(PublishPackageToGithub, PublishPackageToNuGet)
        .Executes(async () =>
        {
            var unreleasedChangelogSectionNotes = ChangelogTasks.ExtractChangelogSectionNotes(RootDirectory / "CHANGELOG.md");
            string changelog = string.Join(Environment.NewLine, unreleasedChangelogSectionNotes);

            GitHubTasks.GitHubClient = new GitHubClient(new ProductHeaderValue("VoxReader"))
            {
                Credentials = new Credentials(GitHubActions.Instance.Token)
            };

            string owner = GitRepository.GetGitHubOwner();
            string name = GitRepository.GetGitHubName();

            SemanticVersion version = GitRepository.GetLatestVersionTag();

            var newRelease = new NewRelease($"v{version}")
            {
                Draft = true,
                Name = version.ToString(),
                Prerelease = version.IsPrerelease,
                Body = changelog
            };

            Release createdRelease = await GitHubTasks.GitHubClient.Repository.Release.Create(owner, name, newRelease);


            foreach (AbsolutePath file in PublishDirectory.GlobFiles("*.nupkg"))
            {
                await using FileStream fileStream = File.OpenRead(file);

                if (!new FileExtensionContentTypeProvider().TryGetContentType(file, out string contentType))
                {
                    contentType = "application/octet-stream";
                }

                var assetUpload = new ReleaseAssetUpload
                {
                    FileName = file.Name,
                    ContentType = contentType,
                    RawData = fileStream
                };

                await GitHubTasks.GitHubClient.Repository.Release.UploadAsset(createdRelease, assetUpload);
            }

            await GitHubTasks.GitHubClient.Repository.Release.Edit(owner, name, createdRelease.Id, new ReleaseUpdate { Draft = false });
        });

    Target PublishPackageToGithub => _ => _
        .Executes(() =>
        {
            foreach (AbsolutePath file in PublishDirectory.GlobFiles("*.nupkg"))
            {
                DotNetTasks.DotNetNuGetPush(s => s
                    .SetTargetPath(file)
                    .SetSource($"https://nuget.pkg.github.com/{GitHubActions.Instance.RepositoryOwner}/index.json")
                    .SetApiKey(GitHubActions.Instance.Token)
                    .EnableSkipDuplicate()
                );
            }
        });

    Target PublishPackageToNuGet => _ => _
        .Executes(() =>
        {
            foreach (AbsolutePath file in PublishDirectory.GlobFiles("*.nupkg"))
            {
                DotNetTasks.DotNetNuGetPush(s => s
                    .SetTargetPath(file)
                    .SetSource("https://api.nuget.org/v3/index.json")
                    .SetApiKey(NuGetApiKey)
                    .EnableSkipDuplicate()
                );
            }
        });
}