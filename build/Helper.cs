using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using NuGet.Versioning;
using Nuke.Common;
using Nuke.Common.IO;
using Serilog;

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

    /// <summary>
    /// Checks recursively if all files and folders have a Unity meta file.
    /// </summary>
    /// <param name="directory">The directory to check.</param>
    /// <param name="excludePredicate">All paths to check are passed to this function. Return TRUE to exclude the current path.</param>
    public static void AssertThatUnityMetaFilesExist(AbsolutePath directory, Func<AbsolutePath, bool> excludePredicate = null)
    {
        Log.Information("Checking if all necessary Unity .meta files exist...");
        
        var directories = directory.GlobDirectories("**").Where(d => d != directory);

        foreach (AbsolutePath d in directories)
        {
            if (excludePredicate != null && excludePredicate(d))
                continue;

            Assert.True((d.Parent / (d.Name + ".meta")).FileExists(), $"The directory '{d}' does not have a Unity meta file!");
        }

        var files = directory.GlobFiles("**/*").Where(f => !f.ToString().EndsWith(".meta"));

        foreach (AbsolutePath f in files)
        {
            if (excludePredicate != null && excludePredicate(f))
                continue;

            Assert.True((f.Parent / (f.Name + ".meta")).FileExists(), $"The file '{f}' does not have a Unity meta file!");
        }
    }

    public static bool StartsWith(this AbsolutePath path, AbsolutePath other)
    {
        return path.ToString().StartsWith(other);
    }

    public static SemanticVersion GetVersionFromUnityPackageFile(AbsolutePath file)
    {
        dynamic packageFile = JsonConvert.DeserializeObject(File.ReadAllText(file));
        return SemanticVersion.Parse(packageFile.version.ToString());
    }
}