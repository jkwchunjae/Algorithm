using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestProject1
{
    public class UnitTest_Problem
    {
        [Theory]
        [InlineData("1=", 1)]
        [InlineData("0-123=", -123)]
        [InlineData("0-123+124=", 1)]
        [InlineData("0=", 0)]
        [InlineData("(((((1123)))))=", 1123)]
        [InlineData("1*(0-123)=", -123)]
        [InlineData("#9+2*3+1=", 10)]
        [InlineData("#(7^2-34+5*2)^3/8+7*23=", 176)]
        [InlineData("(2*(3+6/2)+2)/4+3*(2*(3+6/2)+2)/4+3=", 16)]
        [InlineData("####4611686018427387904=", 14)]
        [InlineData("####4294967296=", 4)]
        [InlineData("100/20*2=", 10)]
        [InlineData("100/#400*2=", 10)]
        [InlineData("100/##160000*2=", 10)]
        [InlineData("2^3^2=", 512)]
        public void CalculatorTest(string inputText, long expected)
        {
            Tokens token = new Tokens(inputText);
            ICalculator input = new Input(token);
            long result = input.Calculate();

            Assert.Equal(expected, result);
        }
    }
}
