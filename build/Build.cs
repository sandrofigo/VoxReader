using System;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;
using NuGet.Versioning;
using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitHub;
using Octokit;
using static Nuke.Common.IO.FileSystemTasks;

class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.Test);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")] readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    // [Parameter] readonly string GitHubAccessToken;

    [Solution] readonly Solution Solution;

    readonly AbsolutePath ProjectPath = (AbsolutePath)Path.Combine(RootDirectory, "VoxReader");

    readonly AbsolutePath PackOutputPath = (AbsolutePath)Path.Combine(RootDirectory, "publish");

    SemanticVersion PackageVersion;

    // [Parameter("The branch or tag name on which the build is executed (GitLab)")] readonly string CI_COMMIT_REF_NAME = string.Empty;

    // bool IsOnMasterBranch => CI_COMMIT_REF_NAME == "master";
    // bool IsOnDevelopBranch => CI_COMMIT_REF_NAME == "develop";

    // bool IsOnVersionTag => Helper.IsValidVersionTag(CI_COMMIT_REF_NAME, out SemanticVersion _);

    Target Clean => _ => _
        .Executes(() =>
        {
            DeleteDirectory(PackOutputPath);
        });

    Target Compile => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetBuildSettings settings = new DotNetBuildSettings()
                .SetProjectFile(ProjectPath);

            DotNetTasks.DotNetBuild(settings);
        });

    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTestSettings settings = new DotNetTestSettings()
                .SetProcessWorkingDirectory(RootDirectory);

            DotNetTasks.DotNetTest(settings);
        });

    // Target ExtractVersionFromTag => _ => _
    //     .Executes(() =>
    //     {
    //         bool success = Helper.IsValidVersionTag(CI_COMMIT_REF_NAME, out PackageVersion);
    //
    //         if (!success)
    //             Logger.Normal($"Could not extract version from '{CI_COMMIT_REF_NAME}'");
    //
    //         Logger.Info($"Package Version: {PackageVersion.ToString()}");
    //     });

    // Target Pack => _ => _
    //     .DependsOn(Test)
    //     .DependsOn(ExtractVersionFromTag)
    //     .Executes(() =>
    //     {
    //         DotNetPackSettings settings = new DotNetPackSettings()
    //             .SetConfiguration(Configuration.Release)
    //             .SetProject(ProjectPath)
    //             .SetVersion(PackageVersion.ToString())
    //             .SetCopyright($"Copyright {DateTime.UtcNow.Year} (c) Sandro Figo")
    //             .SetOutputDirectory(PackOutputPath);
    //
    //         DotNetTasks.DotNetPack(settings);
    //     });

    // Target GitHubRelease => _ => _
    //     .Requires(() => GitHubAccessToken)
    //     .DependsOn(Pack)
    //     .OnlyWhenDynamic(() => IsOnVersionTag)
    //     .Executes(() =>
    //     {
    //         GitHubTasks.GitHubClient = new GitHubClient(new ProductHeaderValue("VoxReader"))
    //         {
    //             Credentials = new Credentials(GitHubAccessToken)
    //         };
    //
    //         var release = new NewRelease($"v{PackageVersion}")
    //         {
    //             Body = "Changes:\n - TODO",
    //             Draft = true,
    //             Name = PackageVersion.ToString(),
    //             TargetCommitish = "master"
    //         };
    //
    //         Release createdRelease = GitHubTasks.GitHubClient.Repository.Release.Create("sandrofigo", "VoxReader", release).Result;
    //
    //         // Add artifacts to release
    //         foreach (AbsolutePath artifact in PackOutputPath.GlobFiles("*"))
    //         {
    //             if (!FileSystemTasks.FileExists(artifact))
    //                 continue;
    //
    //             if (!new FileExtensionContentTypeProvider().TryGetContentType(artifact, out string assetContentType))
    //             {
    //                 assetContentType = "application/x-binary";
    //             }
    //
    //             var releaseAssetUpload = new ReleaseAssetUpload
    //             {
    //                 ContentType = assetContentType,
    //                 FileName = Path.GetFileName(artifact),
    //                 RawData = File.OpenRead(artifact)
    //             };
    //
    //             ReleaseAsset createdReleaseAsset = GitHubTasks.GitHubClient.Repository.Release.UploadAsset(createdRelease, releaseAssetUpload).Result;
    //
    //             Logger.Info($"Added '{releaseAssetUpload.FileName}' to '{release.Name}'.");
    //         }
    //     });

    // Target PublishNuGetPackage => _ => _
    //     .DependsOn(GitHubRelease)
    //     .Executes(() =>
    //     {
    //     });
}