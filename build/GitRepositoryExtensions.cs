using System.Linq;
using NuGet.Versioning;
using Nuke.Common.Git;
using Nuke.Common.Utilities.Collections;

public static class GitRepositoryExtensions
{
    public static bool CurrentCommitHasVersionTag(this GitRepository gitRepository)
    {
        var versionTagsOnCurrentCommit = gitRepository.Tags.Select(t => SemanticVersion.TryParse(t.TrimStart('v'), out SemanticVersion v) ? v : null).WhereNotNull();

        return versionTagsOnCurrentCommit.Any();
    }

    public static SemanticVersion GetLatestVersionTag(this GitRepository gitRepository)
    {
        var versionTagsOnCurrentCommit = gitRepository.Tags.Select(t => SemanticVersion.TryParse(t.TrimStart('v'), out SemanticVersion v) ? v : null).WhereNotNull().OrderByDescending(t => t);

        return versionTagsOnCurrentCommit.First();
    }
}