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
        public void Test_Split3_1()
        {
            var arr = new List<int> { 0, 4, 3, 6, 5, 1, 2 };
            var split = Program.Split3(arr, 1, 6);
            Assert.Equal(3, split.Count);
            Assert.Equal((1, 2), split[0]);
            Assert.Equal((3, 4), split[1]);
            Assert.Equal((5, 6), split[2]);
        }
        [Fact]
        public void Test_Split3_2()
        {
            var arr = new List<int> { 0, 4, 3, 2, 6, 5, 1 };
            var split = Program.Split3(arr, 1, 6);
            Assert.Equal(3, split.Count);
            Assert.Equal((1, 3), split[0]);
            Assert.Equal((4, 5), split[1]);
            Assert.Equal((6, 6), split[2]);
        }
        [Fact]
        public void Test_ReverseArray1()
        {
            var arr = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            // 3 ~ 7
            var begin = 2;
            var end = 6;

            var reversed = Program.Reverse(arr, begin, end);
            var expected = new List<int> { 1, 2, 7, 6, 5, 4, 3, 8, 9 };
            Assert.Equal(expected, reversed);
        }
        [Fact]
        public void Test_ReverseArray2()
        {
            var arr = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            // 7 ~ 9
            var begin = 6;
            var end = 8;

            var reversed = Program.Reverse(arr, begin, end);
            var expected = new List<int> { 1, 2, 3, 4, 5, 6, 9, 8, 7 };
            Assert.Equal(expected, reversed);
        }
        [Fact]
        public void Test_IsAllRight1()
        {
            var arr = new List<int> { 0, 1, 2 };
            var isAllRight = Program.IsAllRight(arr);
            Assert.True(isAllRight);
        }
        [Fact]
        public void Test_IsAllRight2()
        {
            var arr = new List<int> { 0, 2, 1 };
            var isAllRight = Program.IsAllRight(arr);
            Assert.False(isAllRight);
        }
        [Fact]
        public void Test_바꾼게없는경우()
        {
            var arr = new List<int> { 0, 1, 2 };
            var diffs = Program.Solve(arr).ToList();
            var r1 = Program.Reverse(arr, diffs[0].Begin, diffs[0].End);
            var r2 = Program.Reverse(r1, diffs[1].Begin, diffs[1].End);
            Assert.Equal(arr.OrderBy(x => x).ToList(), r2);
        }
        [Fact]
        public void Test_한번만뒤집은경우()
        {
            var arr = new List<int> { 0, 1, 5, 4, 3, 2, 6, 7, 8 };
            var diffs = Program.Solve(arr).ToList();
            var r1 = Program.Reverse(arr, diffs[0].Begin, diffs[0].End);
            var r2 = Program.Reverse(r1, diffs[1].Begin, diffs[1].End);
            Assert.Equal(arr.OrderBy(x => x).ToList(), r2);
        }
        [Fact]
        public void Test_한번만뒤집은경우2()
        {
            var arr = new List<int> { 0, 7, 6, 5, 4, 3, 2, 1, 8, 9, 10, 11, 12 };
            var diffs = Program.Solve(arr).ToList();
            var r1 = Program.Reverse(arr, diffs[0].Begin, diffs[0].End);
            var r2 = Program.Reverse(r1, diffs[1].Begin, diffs[1].End);
            Assert.Equal(arr.OrderBy(x => x).ToList(), r2);
        }
        [Fact]
        public void Test_뒤집은곳이독립적인경우()
        {
            var arr = new List<int> { 0, 4, 3, 2, 1, 5, 6, 10, 9, 8, 7, 11 };
            var diffs = Program.Solve(arr).ToList();
            var r1 = Program.Reverse(arr, diffs[0].Begin, diffs[0].End);
            var r2 = Program.Reverse(r1, diffs[1].Begin, diffs[1].End);
            Assert.Equal(arr.OrderBy(x => x).ToList(), r2);
        }
        [Fact]
        public void Test_포함되어있는데정중앙에서뒤집은경우()
        {
            var arr = new List<int> { 0, 1, 2, 9, 8, 5, 6, 7, 4, 3, 10 };
            var diffs = Program.Solve(arr).ToList();
            var r1 = Program.Reverse(arr, diffs[0].Begin, diffs[0].End);
            var r2 = Program.Reverse(r1, diffs[1].Begin, diffs[1].End);
            Assert.Equal(arr.OrderBy(x => x).ToList(), r2);
        }
        [Fact]
        public void Test_겹치는데두동강난경우()
        {
            var arr = new List<int> { 0, 4, 3, 2, 6, 5, 1 };
            var diffs = Program.Solve(arr).ToList();
            var r1 = Program.Reverse(arr, diffs[0].Begin, diffs[0].End);
            var r2 = Program.Reverse(r1, diffs[1].Begin, diffs[1].End);
            Assert.Equal(arr.OrderBy(x => x).ToList(), r2);
        }
        [Fact]
        public void Test_각각인데이어져보이는경우()
        {
            var arr = new List<int> { 0, 3, 2, 1, 6, 5, 4 };
            var diffs = Program.Solve(arr).ToList();
            var r1 = Program.Reverse(arr, diffs[0].Begin, diffs[0].End);
            var r2 = Program.Reverse(r1, diffs[1].Begin, diffs[1].End);
            Assert.Equal(arr.OrderBy(x => x).ToList(), r2);
        }
        [Fact]
        public void Test_겹쳐진경우일반1()
        {
            var arr = new List<int> { 0, 4, 3, 6, 5, 1, 2 };
            var diffs = Program.Solve(arr).ToList();
            var r1 = Program.Reverse(arr, diffs[0].Begin, diffs[0].End);
            var r2 = Program.Reverse(r1, diffs[1].Begin, diffs[1].End);
            Assert.Equal(arr.OrderBy(x => x).ToList(), r2);
        }
        [Fact]
        public void Test_겹쳐진경우일반2()
        {
            var arr = new List<int> { 0, 4, 3, 2, 6, 5, 1 };
            var diffs = Program.Solve(arr).ToList();
            var r1 = Program.Reverse(arr, diffs[0].Begin, diffs[0].End);
            var r2 = Program.Reverse(r1, diffs[1].Begin, diffs[1].End);
            Assert.Equal(arr.OrderBy(x => x).ToList(), r2);
        }
        [Fact]
        public void Test_겹쳐진경우일반3()
        {
            var arr = new List<int> { 0, 5, 6, 2, 1, 4, 3 };
            var diffs = Program.Solve(arr).ToList();
            var r1 = Program.Reverse(arr, diffs[0].Begin, diffs[0].End);
            var r2 = Program.Reverse(r1, diffs[1].Begin, diffs[1].End);
            Assert.Equal(arr.OrderBy(x => x).ToList(), r2);
        }
        [Fact]
        public void Test_겹쳐진경우일반4()
        {
            var arr = new List<int> { 0, 6, 3, 2, 1, 5, 4 };
            var diffs = Program.Solve(arr).ToList();
            var r1 = Program.Reverse(arr, diffs[0].Begin, diffs[0].End);
            var r2 = Program.Reverse(r1, diffs[1].Begin, diffs[1].End);
            Assert.Equal(arr.OrderBy(x => x).ToList(), r2);
        }
        [Fact]
        public void Test_겹쳐진경우일반5()
        {
            var arr = new List<int> { 0, 6, 4, 5, 3, 2, 1 };
            var diffs = Program.Solve(arr).ToList();
            var r1 = Program.Reverse(arr, diffs[0].Begin, diffs[0].End);
            var r2 = Program.Reverse(r1, diffs[1].Begin, diffs[1].End);
            Assert.Equal(arr.OrderBy(x => x).ToList(), r2);
        }
        [Fact]
        public void Test_1()
        {
            var arr = new List<int> { 4, 3, 2, 1, 8, 7, 6, 5, 0, 9 };
            var diffs = Program.Solve(arr).ToList();
            var r1 = Program.Reverse(arr, diffs[0].Begin, diffs[0].End);
            var r2 = Program.Reverse(r1, diffs[1].Begin, diffs[1].End);
            Assert.Equal(arr.OrderBy(x => x).ToList(), r2);
        }
        [Fact]
        public void Test_랜덤()
        {
            var rnd = new Random((int)DateTime.Now.Ticks);
            (100).For(_ =>
            {
                var N = 100;
                var pureArr = Enumerable.Range(0, N).ToList();
                var begin1 = rnd.Next(0, (int)(N * 0.8));
                var end1 = rnd.Next(begin1, N - 1);
                var begin2 = rnd.Next(0, (int)(N * 0.8));
                var end2 = rnd.Next(begin2, N - 1);

                var arr1 = Program.Reverse(pureArr, begin1, end1);
                var arr = Program.Reverse(arr1, begin2, end2);

                var diffs = Program.Solve(arr).ToList();

                Assert.True(2 == diffs.Count, $"{arr.StringJoin(",")} / {begin1}-{end1} / {begin2}-{end2} ");

                var r1 = Program.Reverse(arr, diffs[0].Begin, diffs[0].End);
                var r2 = Program.Reverse(r1, diffs[1].Begin, diffs[1].End);
                for (var i = 0; i < N; i++)
                {
                    Assert.True(i == r2[i], $"{pureArr.StringJoin(",")}\n{arr.StringJoin(",")} / {begin1}-{end1} / {begin2}-{end2} ");
                }
            });
        }
    }
}
