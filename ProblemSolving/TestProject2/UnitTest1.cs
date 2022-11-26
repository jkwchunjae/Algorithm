using ConsoleLeetCode;

namespace TestProject2;

public class UnitTest1
{
    [Theory]
    [InlineData(1, 1)]
    [InlineData(2, 1)]
    [InlineData(3, 2)]
    [InlineData(4, 2)]
    [InlineData(5, 2)]
    [InlineData(6, 3)]
    [InlineData(9, 3)]
    [InlineData(10, 4)]
    [InlineData(14, 4)]
    [InlineData(15, 5)]
    [InlineData(20, 5)]
    [InlineData(21, 6)]
    [InlineData(28, 7)]
    [InlineData(1000006280, 44720)]
    [InlineData(1000006281, 44721)]
    public void CalcMaxSplitCount1(int N, int expected)
    {
        var result = Ex.GetMaxSplitCount(N);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1, 3, 6)]
    [InlineData(4, 7, 22)]
    [InlineData(1, 44721, 1000006281)]
    public void TestSumRange(int left, int right, long expected)
    {
        var result = Ex.SumRange(left, right);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1, 1, true)]
    [InlineData(2, 2, false)]
    [InlineData(2, 1, true)]
    [InlineData(3, 2, true)]
    [InlineData(4, 2, false)]
    [InlineData(15, 2, true)]
    [InlineData(15, 3, true)]
    [InlineData(15, 4, false)]
    [InlineData(15, 5, true)]
    [InlineData(4942, 4, true)]
    [InlineData(1000006281, 44721, true)]
    public void SplitTest(int N, int split, bool expected)
    {
        var result = Ex.SplitPossible(N, split);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(5, 2)]
    [InlineData(9, 3)]
    [InlineData(15, 4)]
    [InlineData(1, 1)]
    public void SolutionTest(int N, int expected)
    {
        var sol = new Solution();
        var result = sol.ConsecutiveNumbersSum(N);
        Assert.Equal(expected, result);
    }
}
