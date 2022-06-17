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
        [Fact]
        public void 행렬_곱셈_일반연산()
        {
            var m1 = new Matrix(2, 2, 4, -1, 1, 0);
            var m2 = new Matrix(2, 1, 11, 3);

            var m3 = m1 * m2;

            Assert.Equal(41, m3[0][0]);
            Assert.Equal(11, m3[1][0]);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 3)]
        [InlineData(4, 11)]
        [InlineData(6, 41)]
        [InlineData(16, 29681)]
        [InlineData(28, 80198051)]
        [InlineData(32, 117014746)]
        [InlineData(36, 558008386)]
        [InlineData(68, 42141203)]
        [InlineData(100, 436252889)]
        [InlineData(1000000000000000000, 558008386)]
        public void Test(long N, long expected)
        {
            Assert.Equal(expected, Program.Solve(N));
        }
    }
}
