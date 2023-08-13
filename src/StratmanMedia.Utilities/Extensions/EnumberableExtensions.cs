using System.Text;

namespace StratmanMedia.Utilities.Extensions;

public static class EnumberableExtensions
{
    public static string JoinFormat(this IEnumerable<string> items, string itemFormat, string separator = "", bool ignoreEmptyOrNullItems = true)
    {
        var flag = false;
        var stringBuilder = new StringBuilder();
        foreach (var item in items)
        {
            if (ignoreEmptyOrNullItems && item.IsNullOrWhitespace()) continue;

            if (flag)
            {
                stringBuilder.Append(separator);
            }

            stringBuilder.AppendFormat(itemFormat, item);
            flag = true;
        }

        return stringBuilder.ToString();
    }
}