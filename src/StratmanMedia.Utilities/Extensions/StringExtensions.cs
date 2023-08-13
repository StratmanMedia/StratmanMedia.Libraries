namespace StratmanMedia.Utilities.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrWhitespace(this string str)
    {
        return string.IsNullOrWhiteSpace(str);
    }

    public static bool IsNotNullOrWhiteSpace(this string str)
    {
        return !str.IsNullOrWhitespace();
    }
}