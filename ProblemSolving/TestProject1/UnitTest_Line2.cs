using ConsoleLeetCode;
using Xunit;

namespace TestProject1
{
    public class UnitTest_Line2
    {
        [Fact]
        public void 겹치지않는_선1()
        {
            var line1 = new Line(1, 3);
            var line2 = new Line(3, 5);

            Assert.False(Utils.IsOverlap(line1, line2));
        }
        [Fact]
        public void 겹치지않는_선2()
        {
            var line1 = new Line(5, 8);
            var line2 = new Line(3, 5);

            Assert.False(Utils.IsOverlap(line1, line2));
        }
        [Fact]
        public void 겹치지않는_선3()
        {
            var line1 = new Line(1, 2);
            var line2 = new Line(3, 5);

            Assert.False(Utils.IsOverlap(line1, line2));
        }
        [Fact]
        public void 겹치지않는_선4()
        {
            var line1 = new Line(6, 9);
            var line2 = new Line(3, 5);

            Assert.False(Utils.IsOverlap(line1, line2));
        }
        [Fact]
        public void 겹치는_선1()
        {
            var line1 = new Line(5, 8);
            var line2 = new Line(1, 6);

            Assert.True(Utils.IsOverlap(line1, line2));
        }
        [Fact]
        public void 겹치는_선2()
        {
            var line1 = new Line(5, 8);
            var line2 = new Line(7, 9);

            Assert.True(Utils.IsOverlap(line1, line2));
        }
        [Fact]
        public void 겹치는_선3()
        {
            var line1 = new Line(5, 8);
            var line2 = new Line(6, 7);

            Assert.True(Utils.IsOverlap(line1, line2));
        }
        [Fact]
        public void 겹치는_선4()
        {
            var line1 = new Line(5, 8);
            var line2 = new Line(1, 9);

            Assert.True(Utils.IsOverlap(line1, line2));
        }
        [Fact]
        public void 겹치는_선6()
        {
            var line1 = new Line(5, 8);
            var line2 = new Line(5, 6);

            Assert.True(Utils.IsOverlap(line1, line2));
        }
        [Fact]
        public void 겹치는_선7()
        {
            var line1 = new Line(5, 8);
            var line2 = new Line(6, 8);

            Assert.True(Utils.IsOverlap(line1, line2));
        }

    }
}
