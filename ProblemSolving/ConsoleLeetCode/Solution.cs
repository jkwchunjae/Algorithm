namespace ConsoleLeetCode;

public class Solution
{
    readonly long mod = 1000_000_007;
    public int SumSubarrayMins(int[] arr)
    {
        var result = 0L;

        for (var i = 0; i < arr.Length; i++)
        {
            var curr = arr[i];
            var left = i;
            var right = i;

            while (left > 0 && arr[left - 1] >= curr)
                left--;
            while (right < arr.Length - 1 && curr < arr[right + 1])
                right++;

            var arraySize = right - left + 1;
            var position = i - left;

            var subarrayCount = Ex.CountOfSubarray(arraySize, position);
            result += subarrayCount * curr;
            result %= mod;
	    }

        return (int)result;
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
