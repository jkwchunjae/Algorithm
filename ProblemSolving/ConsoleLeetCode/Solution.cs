namespace ConsoleLeetCode;

public class Solution
{
    public int ComputeArea(int ax1, int ay1, int ax2, int ay2, int bx1, int by1, int bx2, int by2)
    {
        var hLineA = new Line(ax1, ax2);
        var vLineA = new Line(ay1, ay2);
        var hLineB = new Line(bx1, bx2);
        var vLineB = new Line(by1, by2);

        var areaA = hLineA.Length * vLineA.Length;
        var areaB = hLineB.Length * vLineB.Length;
        if (Utils.IsOverlap(hLineA, hLineB) && Utils.IsOverlap(vLineA, vLineB))
        {
            var xList = new[] { ax1, ax2, bx1, bx2 }.OrderBy(x => x).ToArray();
            var yList = new[] { ay1, ay2, by1, by2 }.OrderBy(y => y).ToArray();

            var hLine = new Line(xList[1], xList[2]);
            var vLine = new Line(yList[1], yList[2]);
            var areaIntersect = hLine.Length * vLine.Length;

            return areaA + areaB - areaIntersect;
        }
        else
        {
            return areaA + areaB;
        }
    }
}

public class Line
{
    public int A { get; init; }
    public int B { get; init; }
    public int Length => B - A;
    public Line(int a, int b)
    {
        A = a;
        B = b;
    }
}

public static class Utils
{
    public static bool IsOverlap(Line line1, Line line2)
    {
        if (line2.A < line1.A && line2.B > line1.A)
            return true;
        if (line2.B > line1.B && line2.A < line1.B)
            return true;
        if (line2.A >= line1.A && line2.B <= line1.B)
            return true;
        return false;
    }
}
