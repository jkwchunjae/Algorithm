using ConsoleApp1;
using System;
using Xunit;

namespace TestProject1
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(49, 4)]
        [InlineData(123, 6)]
        [InlineData(999, 9)]
        public void Test_Dig(int value, int dig)
        {
            var calcDig = Program.Dig(value);

            Assert.Equal(dig, calcDig);
        }

        [Theory]
        [InlineData(2, 0, 6)]
        [InlineData(12, 2, 1)]
        public void Test_Pattern(int M, int patternBeginIndex, int patternLength)
        {
            var pattern = Program.GetPattern(M);

            Assert.Equal((patternBeginIndex, patternLength), pattern);
        }
    }
}
