using ConsoleLeetCode;
using System;
using System.Diagnostics;
using Xunit;
using Xunit.Abstractions;

namespace TestProject1;

public class UnitTest_LeetCode
{
    private readonly ITestOutputHelper output;

    public UnitTest_LeetCode(ITestOutputHelper output)
    {
        this.output = output;
    }

    [Fact]
    public void IntersectArea()
    {
        var rnd = new Random((int)DateTime.Now.Ticks);

        for (var i = 0; i < 1000; i++)
        {
            var hLineA = NewLine(rnd);
            var vLineA = NewLine(rnd);
            var hLineB = NewLine(rnd);
            var vLineB = NewLine(rnd);
            var solution = new Solution();
            var area1 = solution.ComputeArea(hLineA.A, vLineA.A, hLineA.B, vLineA.B, hLineB.A, vLineB.A, hLineB.B, vLineB.B);
            var area2 = Area(hLineA, vLineA, hLineB, vLineB);
            if (area1 != area2)
                output.WriteLine($"({hLineA.A}, {vLineA.A})  ({hLineA.B}, {vLineA.B})  ({hLineB.A}, {vLineB.A})  ({hLineB.B}, {vLineB.B})");
            Assert.Equal(area2, area1);
        }
    }

    private Line NewLine(Random rnd)
    {
        var a = rnd.Next(0, 99);
        var b = rnd.Next(0, 99);
        while (a == b)
            b = rnd.Next(0, 99);

        return new Line(Math.Min(a, b), Math.Max(a, b));
    }

    private int Area(Line hLineA, Line vLineA, Line hLineB, Line vLineB)
    {
        var map = new int[100, 100];
        for (var x = hLineA.A; x < hLineA.B; x++)
            for (var y = vLineA.A; y < vLineA.B; y++)
                map[x, y] = 1;
        for (var x = hLineB.A; x < hLineB.B; x++)
            for (var y = vLineB.A; y < vLineB.B; y++)
                map[x, y] = 1;

        var area = 0;
        for (var x = 0; x < 100; x++)
            for (var y = 0; y < 100; y++)
                area += map[x, y];

        return area;
    }
}
