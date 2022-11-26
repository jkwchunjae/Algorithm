namespace ConsoleLeetCode;

public class Solution
{
    public int ConsecutiveNumbersSum(int N)
    {
        var maxSplitCount = Ex.GetMaxSplitCount(N);
        var result = Enumerable.Range(1, maxSplitCount)
            .Count(split => Ex.SplitPossible(N, split));

        return result;
    }
}

public static class Ex
{
    public static int GetMaxSplitCount(int N)
    {
        double n = 2L * N + 0.25;
        return (int)(Math.Sqrt(n) - 0.5);
    }
    public static bool SplitPossible(int N, int split)
    {
        if (split % 2 == 0)
        {
            var leftMid = N / split;
            var left = leftMid - (split / 2 - 1);
            var right = leftMid + (split / 2);
            var sum = SumRange(left, right);
            return sum == N;
        }
        else
        {
            var mid = N / split;
            var left = mid - split / 2;
            var right = mid + split / 2;
            var sum = SumRange(left, right);
            return sum == N;
        }
    }
    public static long SumRange(int left, int right)
    {
        long count = right - left + 1;
        long sum = ((left + right) * count) / 2;
        return sum;
    }
}