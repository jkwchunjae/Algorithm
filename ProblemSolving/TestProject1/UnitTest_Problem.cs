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
        [InlineData(1, 5)]
        [InlineData(2, 27)]
        [InlineData(3, 143)]
        [InlineData(4, 751)]
        [InlineData(5, 935)]
        [InlineData(6, 607)]
        [InlineData(7, 903)]
        [InlineData(8, 991)]
        [InlineData(9, 335)]
        [InlineData(10, 47)]
        [InlineData(11, 943)]
        [InlineData(12, 471)]
        [InlineData(13, 55)]
        [InlineData(14, 447)]
        [InlineData(15, 463)]
        [InlineData(16, 991)]
        [InlineData(17, 95)]
        public void Test_Solve(int N, int last3)
        {
            var result = Program.Solve(N);
            Assert.Equal(last3, result);
        }

        //[Theory]
        //[InlineData(1, 6, -4, 1, 0)]
        //[InlineData(2, 32, -24, 6, -4)]
        //[InlineData(3, 168, -128, 32, -24)]
        //[InlineData(4, 880, -672, 168, -128)]
        //public void Test_Matrix1(int N, int a, int b, int c, int d)
        //{
        //    var m = Program.GetMatrix(N);
        //    Assert.Equal(a, m[0][0]);
        //    Assert.Equal(b, m[0][1]);
        //    Assert.Equal(c, m[1][0]);
        //    Assert.Equal(d, m[1][1]);
        //}
    }
}
