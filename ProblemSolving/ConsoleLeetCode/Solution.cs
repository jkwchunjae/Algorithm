namespace ConsoleLeetCode;

public class Solution
{
    public int SumSubarrayMins(int[] arr)
    {
        var mod = 1000_000_007;
        var result = 0L;
        var beginIndex = 0L;
        var endIndex = 0L;
        var currentIndex = 0L;

        return 0;
    }

}

public static class Ex
{
    /// <summary>
    /// array에 내가 index번 위치에 있을 때 나를 포함해서 만들 수 있는 subarray의 개수
    /// </summary>
    /// <param name="arraySize"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static long CountOfSubarray(long arraySize, long index)
    {
        var n = index + 1;
        var m = arraySize - n;

        return n * (m + 1);
    }
}
