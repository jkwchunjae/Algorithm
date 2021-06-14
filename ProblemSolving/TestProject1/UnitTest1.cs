using ConsoleApp1;
using System;
using System.Collections.Generic;
using Xunit;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test_IsIntersect_교차1()
        {
            var line1 = new Line { P1 = new Point(0, 0), P2 = new Point(5, 0) };
            var line2 = new Line { P1 = new Point(1, 5), P2 = new Point(1, -1) };

            var isIntersect = Ex.IsIntersect(line1, line2);

            Assert.True(isIntersect);
        }

        [Fact]
        public void Test_IsIntersect_교차2()
        {
            var line1 = new Line { P1 = new Point(0, 0), P2 = new Point(5, 0) };
            var line2 = new Line { P1 = new Point(5, 0), P2 = new Point(10, 0) };

            var isIntersect = Ex.IsIntersect(line1, line2);

            Assert.True(isIntersect);
        }

        [Fact]
        public void Test_IsIntersect_교차4()
        {
            var line1 = new Line { P1 = new Point(0, 0), P2 = new Point(5, 0) };
            var line2 = new Line { P1 = new Point(10, 0), P2 = new Point(5, 0) };

            var isIntersect = Ex.IsIntersect(line1, line2);

            Assert.True(isIntersect);
        }

        [Fact]
        public void Test_IsIntersect_교차3()
        {
            var line1 = new Line { P1 = new Point(0, 0), P2 = new Point(5, 0) };
            var line2 = new Line { P1 = new Point(1, 5), P2 = new Point(1, 0) };

            var isIntersect = Ex.IsIntersect(line1, line2);

            Assert.True(isIntersect);
        }

        [Fact]
        public void Test_IsIntersect_교차아님1()
        {
            var line1 = new Line { P1 = new Point(0, 0), P2 = new Point(5, 0) };
            var line2 = new Line { P1 = new Point(6, 0), P2 = new Point(10, 0) };

            var isIntersect = Ex.IsIntersect(line1, line2);

            Assert.False(isIntersect);
        }

        [Fact]
        public void Test_IsIntersect_교차아님2()
        {
            var line1 = new Line { P1 = new Point(0, 0), P2 = new Point(5, 0) };
            var line2 = new Line { P1 = new Point(1, 5), P2 = new Point(1, 1) };

            var isIntersect = Ex.IsIntersect(line1, line2);

            Assert.False(isIntersect);
        }


    }
}
