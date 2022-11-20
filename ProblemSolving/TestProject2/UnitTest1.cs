using ConsoleLeetCode;

namespace TestProject2;

public class UnitTest1
{
    [Fact]
    public void CalculatorTest1()
    {
        var solution = new Solution();
        var result = solution.Calculate("1 + 1");
        Assert.Equal(2, result);
    }

    [Fact]
    public void CalculaorTest2()
    { 
        var solution = new Solution();
        var result = solution.Calculate(" 2-1 + 2 ");
        Assert.Equal(3, result);
    }

    [Fact]
    public void CalculatorTest3()
    { 
        var solution = new Solution();
        var result = solution.Calculate("(1+(4+5+2)-3)+(6+8)");
        Assert.Equal(23, result);
    }

    [Fact]
    public void CalculatorTest4()
    { 
        var solution = new Solution();
        var result = solution.Calculate("-1");
        Assert.Equal(-1, result);
    }

    [Fact]
    public void CalculatorTest5()
    { 
        var solution = new Solution();
        var result = solution.Calculate("-(2 + 3)");
        Assert.Equal(-5, result);
    }

    [Fact]
    public void CalculatorTest6()
    { 
        var solution = new Solution();
        var result = solution.Calculate("-2+ 1");
        Assert.Equal(-1, result);
    }

    [Fact]
    public void CalculatorTest7()
    { 
        var solution = new Solution();
        var result = solution.Calculate("-(3+4)+5");
        Assert.Equal(-2, result);
    }
}
