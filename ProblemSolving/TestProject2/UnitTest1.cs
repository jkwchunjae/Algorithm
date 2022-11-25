using ConsoleLeetCode;

namespace TestProject2;

public class UnitTest1
{
    [Fact]
    public void SumSubMin1()
    {
        var arr = new[] { 5, 3, 1, 2, 4 };
        var sol = new Solution();
        sol.SumSubarrayMins(arr);
    }









    [Fact]
    public void CountOfSubarray_1()
    {
        // [1, 2, 3] 중에 1이 포함되는 경우
        var count = Ex.CountOfSubarray(3, 0);
        Assert.Equal(3, count);
    }
    [Fact]
    public void CountOfSubarray_2()
    {
        // [4, 1, 2, 3] 중에 1이 포함되는 경우
        var count = Ex.CountOfSubarray(4, 1);
        Assert.Equal(6, count);
    }
    [Fact]
    public void CountOfSubarray_3()
    {
        // [5, 4, 1, 2, 3] 중에 1이 포함되는 경우
        var count = Ex.CountOfSubarray(5, 2);
        Assert.Equal(9, count);
    }
    [Fact]
    public void CountOfSubarray_4()
    {
        // [6, 5, 4, 1, 2, 3] 중에 1이 포함되는 경우
        var count = Ex.CountOfSubarray(6, 3);
        Assert.Equal(12, count);
    }
    [Fact]
    public void CountOfSubarray_5()
    {
        // [7, 6, 5, 4, 1, 2, 3] 중에 1이 포함되는 경우
        var count = Ex.CountOfSubarray(7, 4);
        Assert.Equal(15, count);
    }
}
