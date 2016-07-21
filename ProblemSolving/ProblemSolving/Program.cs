using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class Program
{
	public static void Main(string[] args)
	{
		int t = IOExt.GetInputLine().ToInt();
		while (t-- > 0)
		{
			var N = IOExt.GetInputInt();
			var a = IOExt.GetInputIntList();
			var b = IOExt.GetInputIntList();
			var A = Enumerable.Range(1, N).ToList();
			var B = Enumerable.Range(1, N).ToList();
			var C = Enumerable.Range(1, N).ToList();
			A[0] = a[0];
			B[0] = b[0];
			C[0] = 0;
			for (int i = 1; i < N; i++)
			{
				A[i] = Math.Max(B[i - 1], C[i - 1]) + a[i];
				B[i] = Math.Max(A[i - 1], C[i - 1]) + b[i];
				C[i] = Math.Max(A[i - 1], B[i - 1]);
			}
			Math.Max(A[N - 1], Math.Max(B[N - 1], C[N - 1])).Dump();
		}
	}
}

public static class IOExt
{
#if DEBUG
	static List<string> _input;
	static int _readInputCount = 0;
#endif
	static IOExt()
	{
#if DEBUG
		_input = File.ReadAllLines("input.txt", Encoding.UTF8).ToList();
#endif
	}

	public static string GetInputLine()
	{
#if DEBUG
		return _input[_readInputCount++];
#else
		return Console.ReadLine();
#endif
	}

	public static List<int> GetInputIntList()
	{
		return GetInputLine().Split(' ').Select(x => x.ToInt()).ToList();
	}

	public static int GetInputInt()
	{
		return GetInputLine().ToInt();
	}

	public static T Dump<T>(this T obj, string format = "")
	{
		if (format == "")
		{
			Console.WriteLine(obj);
		}
		else
		{
			Console.WriteLine(format, obj);
		}
		return obj;
	}
}

public static class Extensions
{
	public static IEnumerable<long> GetPrimeList(int maximum)
	{
		if (maximum < 2)
			yield break;

		var isPrime = Enumerable.Range(0, maximum + 1).Select(x => false).ToList();

		yield return 2;
		for (var prime = 3; prime <= maximum; prime += 2)
		{
			if (isPrime[prime] == true)
				continue;
			yield return prime;
			for (var i = prime; i <= maximum; i += prime)
				isPrime[i] = true;
		}
	}

	public static string With(this string format, params object[] obj)
	{
		return string.Format(format, obj);
	}

	public static string StringJoin<T>(this IEnumerable<T> list, string separator = " ")
	{
		return string.Join(separator, list);
	}

	public static int ToInt(this string str)
	{
		return int.Parse(str);
	}

	public static long ToLong(this string str)
	{
		return long.Parse(str);
	}

}