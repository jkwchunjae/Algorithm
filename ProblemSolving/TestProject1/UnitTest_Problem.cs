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
        public void Test1()
        {
            var column = 5;
            var result = Program.ConvertColumnNumToStr(column);

            Assert.Equal("E", result);
        }
        [Fact]
        public void Test2()
        {
            var column = 26;
            var result = Program.ConvertColumnNumToStr(column);

            Assert.Equal("Z", result);
        }
        [Fact]
        public void Test3()
        {
            var column = 27;
            var result = Program.ConvertColumnNumToStr(column);

            Assert.Equal("AA", result);
        }
        [Fact]
        public void Test4()
        {
            var column = 30;
            var result = Program.ConvertColumnNumToStr(column);

            Assert.Equal("AD", result);
        }
        [Fact]
        public void Test5()
        {
            var column = 134;
            var result = Program.ConvertColumnNumToStr(column);

            Assert.Equal("ED", result);
        }
        [Fact]
        public void Test6()
        {
            var column = 703;
            var result = Program.ConvertColumnNumToStr(column);

            Assert.Equal("AAA", result);
        }
        [Fact]
        public void Test7()
        {
            var column = 962;
            var result = Program.ConvertColumnNumToStr(column);

            Assert.Equal("AJZ", result);
        }
        [Fact]
        public void Test9()
        {
            var column = 300000000;
            var result = Program.ConvertColumnNumToStr(column);

            Assert.Equal("YFLRYN", result);
        }
        [Fact]
        public void Test10()
        {
            var column = int.MaxValue;
            var result = Program.ConvertColumnNumToStr(column);

            Assert.Equal("FXSHRXW", result);
        }

        [Fact]
        public void Test8()
        {
            var rnd = new Random((int)DateTime.Now.Ticks);
            Enumerable.Range(1, 100).ForEach(_ =>
            {
                var column = rnd.Next(1, 100);
                var columnText = Program.ConvertColumnNumToStr(column);
                var newColumn = Program.ConvertColumnStrToNum(columnText);

                Assert.Equal(column, newColumn);
            });
        }
    }
}
