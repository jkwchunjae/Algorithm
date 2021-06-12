using ConsoleApp1;
using System;
using System.Collections.Generic;
using Xunit;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test_MakeList()
        {
            var arr = new List<int> { 75, 30, 100, 38, 50, 51, 52, 20, 81, 5 };
            var root = Program.MakeTree(arr, 0, arr.Count - 1);

            Assert.Equal(5, root.Min);
            Assert.Equal(100, root.Max);
        }
    }
}
