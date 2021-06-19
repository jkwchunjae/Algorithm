using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestProject1
{
    public class UnitTest_Math
    {
        [Theory]
        [InlineData(1, 3, 1)]
        [InlineData(2, 3, 1)]
        [InlineData(4, 6, 2)]
        [InlineData(20, 112, 4)]
        public void Test_Gcd(int a, int b, int expectedGcd)
        {
            var gcd = Ex.Gcd(a, b);
            Assert.Equal(expectedGcd, gcd);
        }

        [Theory]
        [InlineData(216, 152, 16, true)]
        [InlineData(152, 216, 16, true)]
        [InlineData(7, 5, 1, true)]
        [InlineData(6, 4, 1, false)]
        public void Test_디오판토스_방정식_선형합동식(int a, int b, int c, bool goalSuccess)
        {
            var (found, x, y) = Ex.FindDiophantusEquation(a, b, c);

            Assert.Equal(goalSuccess, found);
            if (found)
            {
                Assert.Equal(c, a * x + b * y);
            }
        }

        private void Base_중국인의_나머지_정리(List<(long A, long M)> arr)
        {
            var x = Ex.ChineseRemainderTheorem(arr);
            foreach (var data in arr)
            {
                Assert.Equal(data.A, x % data.M);
            }
        }

        [Fact]
        public void Test_중국인의_나머지_정리1()
        {
            var arr = new List<(long A, long M)>
            {
                (2, 3),
                (3, 5),
                (1, 7),
            };
            Base_중국인의_나머지_정리(arr);
        }

        [Fact]
        public void Test_중국인의_나머지_정리2()
        {
            var arr = new List<(long A, long M)>
            {
                (2, 3),
                (3, 5),
                (3, 7),
            };
            Base_중국인의_나머지_정리(arr);
        }

        [Fact]
        public void Test_중국인의_나머지_정리3()
        {
            var arr = new List<(long A, long M)>
            {
                (1, 3),
                (2, 5),
            };
            Base_중국인의_나머지_정리(arr);
        }
    }
}
