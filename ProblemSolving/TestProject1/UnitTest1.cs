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
            var add = Program.Add(1, 2);
            Assert.Equal(3, add);
        }
    }
}
