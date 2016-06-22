using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class Program
{
	public static void Main(string[] args)
	{
		Extensions.GetInputLine();
		Extensions.GetInputLine().Split(' ')
			.Select(x => x.ToInt())
			.Where(x => x > 0)
			.Select(x => new { Y = (x / 30 + 1) * 10, M = (x / 60 + 1) * 15 })
			.GroupBy(x => 1)
			.Select(x => new { Y = x.Sum(e => e.Y), M = x.Sum(e => e.M) })
			.Select(x => "{0} {1}".With(x.Y == x.M ? "Y M" : x.Y < x.M ? "Y" : "M", Math.Min(x.Y, x.M)))
			.First()
			.Dump();
	}
}

public static class Extensions
{
#if DEBUG
	static List<string> _input;
	static int _readInputCount = 0;
#endif
	static Extensions()
	{
#if DEBUG
		_input = File.ReadAllLines("input.txt", Encoding.UTF8).ToList();
#endif
	}

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

	public static string GetInputLine()
	{
#if DEBUG
		return _input[_readInputCount++];
#else
		return Console.ReadLine();
#endif
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