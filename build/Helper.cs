using System.Text.RegularExpressions;

public static class Helper
{
    public static bool IsValidVersionTag(string version)
    {
        return Regex.IsMatch(version, "^v[0-9]*.[0-9]*.[0-9]*");
    }
}