using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace TestProject1
{
    public class UnitTest_Problem
    {
        Random random = new Random((int)(DateTime.Now.Ticks));

        private readonly ITestOutputHelper output;

        public UnitTest_Problem(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Test()
        {
            for (var i = 0; i < 1000; i++)
            {
                var sampleResult = CreateSampleResult();
                var leftLargeOrder = CalcLeftLargeOrder(sampleResult);
                var result = Program.Solve(leftLargeOrder.Length, leftLargeOrder);

                var check = result.SequenceEqual(sampleResult);
                if (!check)
                {
                    output.WriteLine($"Sample: {string.Join(", ", sampleResult)}");
                    output.WriteLine($"LeftLargeOrder: {string.Join(", ", leftLargeOrder)}");
                    output.WriteLine($"Result: {string.Join(", ", result)}");
                }
                output.WriteLine($"{i}: {sampleResult.Length} {check}");
                Assert.True(check);
            }
        }

        public int[] CreateSampleResult()
        {
            return Enumerable.Range(1, random.Next(1, 10))
                .Select(x => new { Value = x, R = random.Next() })
                .OrderBy(x => x.R)
                .Select(x => x.Value)
                .ToArray();
        }

        public int[] CalcLeftLargeOrder(int[] arr)
        {
            var result = new int[arr.Length];
            for (var i = 1; i <= arr.Length; i++)
            {
                var leftLarge = arr.TakeWhile(x => x != i)
                    .Count(x => x > i);
                result[i - 1] = leftLarge;
            }
            return result;
        }
    }
}
