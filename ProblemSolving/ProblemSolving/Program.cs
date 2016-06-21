using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class Program
{
	public static void Main(string[] args)
	{
		var inputs = Extensions.GetInputLine().Split(' ');
		var A = inputs[0].ToLong();
		var B = inputs[1].ToLong();
		var cnt = (int)(B - A + 1);

		var isAnswer = Enumerable.Range(1, cnt).Select(x => true).ToList();

		foreach (var square in Extensions.GetPrimeList(1001000).Select(x => x * x).Where(x => x <= B))
		{
			var index = A % square == 0 ? 0 : square - (A % square);
			for (var i = index; i < cnt; i += square)
			{
				isAnswer[(int)i] = false;
			}
		}

		isAnswer.Count(x => x).Dump();
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