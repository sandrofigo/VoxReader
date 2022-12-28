using System.Text.RegularExpressions;
using NuGet.Versioning;
using Nuke.Common.IO;

public static class ChangelogTasksExtensions
{
    public static bool TryGetLatestVersionInChangelog(AbsolutePath changelog, out SemanticVersion version, out string rawVersionValue)
    {
        string[] lines = TextTasks.ReadAllLines(changelog);

        foreach (string line in lines)
        {
            Match match = Regex.Match(line, @"##\s*\[(?<version>.*)\]");

            if (!match.Success)
                continue;

            rawVersionValue = match.Groups["version"].Value;

            return SemanticVersion.TryParse(rawVersionValue, out version);
        }

        rawVersionValue = "";
        version = null;
        return false;
    }
}