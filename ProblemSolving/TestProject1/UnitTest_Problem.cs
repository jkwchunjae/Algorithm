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
        public void Test_GetLastIndex_BinarySearch1()
        {
            var arr = new List<int> { 2, 3, 4, 5, 6 };
            var value = 3;
            var lastIndex = Program.GetLastIndex_BinarySearch(arr, value);
            Assert.Equal(1, lastIndex);
        }

        [Fact]
        public void Test_GetLastIndex_BinarySearch2()
        {
            var arr = new List<int> { 2, 2, 2, 3, 3, 3, 4, 5, 6 };
            var value = 3;
            var lastIndex = Program.GetLastIndex_BinarySearch(arr, value);
            Assert.Equal(5, lastIndex);
        }

        [Fact]
        public void Test_GetLastIndex_BinarySearch3()
        {
            var arr = new List<int> { 2, 2, 2, 3, 3, 3, 4, 5, 6 };
            var value = 6;
            var lastIndex = Program.GetLastIndex_BinarySearch(arr, value);
            Assert.Equal(8, lastIndex);
        }

        [Fact]
        public void Test_GetLastIndex_BinarySearch4()
        {
            var arr = new List<int> { 2, 2, 2, 3, 3, 3, 4, 5, 6, 6 };
            var value = 6;
            var lastIndex = Program.GetLastIndex_BinarySearch(arr, value);
            Assert.Equal(9, lastIndex);
        }

        [Fact]
        public void Test_GetLastIndex_BinarySearch5()
        {
            var arr = new List<int> { 2, 2, 2, 3, 3, 3, 5, 6, 6 };
            var value = 4;
            var lastIndex = Program.GetLastIndex_BinarySearch(arr, value);
            Assert.Equal(5, lastIndex);
        }

        [Fact]
        public void Test_GetLastIndex_BinarySearch6()
        {
            var arr = new List<int> { 425, 481, 578, 625, 1556, 2196, 2533, 2665, 2873, 3098, 5330 };
            var value = 961;
            var lastIndex = Program.GetLastIndex_BinarySearch(arr, value);
            Assert.Equal(3, lastIndex);
        }
    }
}
