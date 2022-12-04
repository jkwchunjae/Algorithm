using ConsoleLeetCode;

namespace TestProject2;

public class UnitTest1
{
    [Theory]
    [InlineData("tree", "eert")]
    [InlineData("cccaaa", "aaaccc")]
    [InlineData("Aabb", "bbAa")]
    public void Test(string input, string expected)
    {
        var sol = new Solution();
        var result = sol.FrequencySort(input);

        Assert.Equal(expected, result);
    }
}
