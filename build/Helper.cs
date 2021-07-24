using System.Text.RegularExpressions;
using NuGet.Versioning;

public static class Helper
{
    public static bool IsValidVersionTag(string version, out SemanticVersion semanticVersion)
    {
        Match match = Regex.Match(version, @"v(?<major>\d+).(?<minor>\d+).(?<patch>\d+)");

        semanticVersion = new SemanticVersion(0, 0, 0);

        if (!match.Success)
            return false;
        
        semanticVersion = new SemanticVersion(
            int.Parse(match.Groups["major"].Value),
            int.Parse(match.Groups["minor"].Value),
            int.Parse(match.Groups["patch"].Value)
        );

        return true;
    }
}