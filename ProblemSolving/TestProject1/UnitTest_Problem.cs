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
        public void Test_PointSequence()
        {
            var list = Program.GetPoints(100).ToList();

            var count = 0;
            Assert.Equal(new Point(0, 0), list[count++]);
            Assert.Equal(new Point(1, 0), list[count++]);
            Assert.Equal(new Point(0, 1), list[count++]);
            Assert.Equal(new Point(-1, 1), list[count++]);
            Assert.Equal(new Point(-1, 0), list[count++]);
            Assert.Equal(new Point(0, -1), list[count++]);
            Assert.Equal(new Point(1, -1), list[count++]);
            Assert.Equal(new Point(2, -1), list[count++]);
            Assert.Equal(new Point(2, 0), list[count++]);
            Assert.Equal(new Point(1, 1), list[count++]);
            Assert.Equal(new Point(0, 2), list[count++]);
            Assert.Equal(new Point(-1, 2), list[count++]);
            Assert.Equal(new Point(-2, 2), list[count++]);
            Assert.Equal(new Point(-2, 1), list[count++]);
            Assert.Equal(new Point(-2, 0), list[count++]);
            Assert.Equal(new Point(-1, -1), list[count++]);
            Assert.Equal(new Point(0, -2), list[count++]);
            Assert.Equal(new Point(1, -2), list[count++]);
            Assert.Equal(new Point(2, -2), list[count++]);
            Assert.Equal(new Point(3, -2), list[count++]);
            Assert.Equal(new Point(3, -1), list[count++]);
            Assert.Equal(new Point(3, 0), list[count++]);
        }
    }
}
