using ConsoleApp1;
using System;
using Xunit;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var n2 = Program.Calc((3, 1), (3, 1));
            var n4 = Program.Calc(n2, n2);
            var n8 = Program.Calc(n4, n4);
            var pow8 = Program.Pow(8);

            Assert.Equal(n8, pow8);
        }
    }
}
