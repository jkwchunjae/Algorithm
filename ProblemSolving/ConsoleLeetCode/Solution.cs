using System.Text;

namespace ConsoleLeetCode;

public class Solution
{
    public string FrequencySort(string s)
    {
        return s
            .GroupBy(c => c)
            .Select(x => new { Chr = x.Key, Count = x.Count() })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.Chr)
            .Aggregate(new StringBuilder(s.Length), (builder, item) => builder.Append(item.Chr, item.Count))
            .ToString();
    }
}

