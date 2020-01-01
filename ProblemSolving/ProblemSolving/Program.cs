using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
#if DEBUG // delete
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Diagnostics;
#endif

public class Program
{
#if DEBUG // delete
    [STAThread]
#endif
    public static void Main(string[] args)
    {
#if DEBUG // delete
        var problemNumber = "3613";
        var inputOutputList = BojUtils.MakeInputOutput(problemNumber, useLocalInput: false);
        var checkAll = true;
        foreach (var inputOutput in inputOutputList)
        {
            IO.SetInputOutput(inputOutput);
#endif
            Solve();
#if DEBUG // delete
            var result = IO.IsCorrect().Dump();
            checkAll = checkAll && result;
            Console.WriteLine();
        }
        DebugUtils.CopyCode();
        if (checkAll)
        {
            Process.Start($"https://www.acmicpc.net/submit/{problemNumber}");
        }
#endif
    }

    public static void Solve()
    {
        var text = IO.GetLine();
        if (text.IsJava())
        {

        }
        else if (text.IsCpp())
        {
        }
        else
        {
            "Error!".Dump();
        }
    }
}


public static class Extensionss
{
}

public class Node
{
    public int Number;
    public bool Mark = false;
    public List<Edge> EdgeList = new List<Edge>();

    public Node(int number)
    {
        Number = number;
    }
}

public class Edge
{
    public int Weight;
    public Node Target;

    public Edge() { }
    public Edge(Node target)
    {
        Weight = 1;
        Target = target;
    }

    public Edge(int weight, Node target)
    {
        Weight = weight;
        Target = target;
    }
}

public static class Algorithm
{
    public static void BFS(this Node startNode, Action<Node> action)
    {
        var queue = new Queue<Node>();
        queue.Enqueue(startNode);
        startNode.Mark = true;

        while (queue.Any())
        {
            var node = queue.Dequeue();
            action(node);

            node.EdgeList.ForEach(edge =>
            {
                if (!edge.Target.Mark)
                {
                    edge.Target.Mark = true;
                    queue.Enqueue(edge.Target);
                }
            });
        }
    }

    public static void DFS(this Node startNode, Action<Node> action, bool nonReq = false)
    {
        if (nonReq)
        {
            startNode.DFS_non_req(action);
            return;
        }

        startNode.Mark = true;
        action(startNode);

        startNode.EdgeList.ForEach(edge =>
        {
            if (!edge.Target.Mark)
            {
                edge.Target.DFS(action);
            }
        });
    }

    public static void DFS_non_req(this Node startNode, Action<Node> action)
    {
        var stack = new Stack<Node>();
        stack.Push(startNode);

        while (stack.Any())
        {
            var node = stack.Pop();
            if (node.Mark)
                continue;

            node.Mark = true;
            action(node);

            for (var i = node.EdgeList.Count - 1; i >= 0; i--)
            {
                var targetNode = node.EdgeList[i].Target;
                if (!targetNode.Mark)
                {
                    stack.Push(targetNode);
                }
            }
        }
    }
}

public static class IO
{
#if DEBUG // delete
    static List<string> _input;
    static string _answer;
    static string _output = "";
    static int _readInputCount = 0;

    public static bool IsCorrect() => _output.Trim() == _answer.Trim();
    public static int InputCount => _input.Count();

    static IO()
    {
        _input = File.ReadAllLines("input.txt", Encoding.UTF8).ToList();
        _answer = File.ReadAllText("output.txt", Encoding.UTF8);
        Init();
    }

    public static void Init()
    {
        _output = "";
        _readInputCount = 0;
    }

    public static void SetInputOutput(InputOutput inputOutput)
    {
        Init();
        _input = inputOutput.Input.Replace("\r", "").Split('\n').ToList();
        _answer = inputOutput.Output;
    }
#endif

    public static string GetLine()
    {
#if DEBUG
        return _input[_readInputCount++];
#else
        return Console.ReadLine();
#endif
    }

    public static List<int> GetIntList()
        => GetLine().Split(' ').Where(x => x.Length > 0).Select(x => x.ToInt()).ToList();

    public static (int, int) GetIntTuple2()
    {
        var arr = GetIntList();
        return (arr[0], arr[1]);
    }

    public static (int, int, int) GetIntTuple3()
    {
        var arr = GetIntList();
        return (arr[0], arr[1], arr[2]);
    }

    public static int GetInt()
        => GetLine().ToInt();

    public static List<long> GetLongList()
        => GetLine().Split(' ').Where(x => x.Length > 0).Select(x => x.ToLong()).ToList();

    public static long GetLong()
        => GetLine().ToLong();

    public static T Dump<T>(this T obj, string format = "")
    {
        var text = string.IsNullOrEmpty(format) ? $"{obj}" : string.Format(format, obj);
        Console.WriteLine(text);
#if DEBUG // delete
        _output += Environment.NewLine + text;
#endif
        return obj;
    }

    public static List<T> Dump<T>(this List<T> list)
    {
        Console.WriteLine(list.StringJoin(" "));
        return list;
    }
}

public enum LoopResult
{
    Void,
    Break,
    Continue,
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

    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (var item in source)
            action(item);
    }

    public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
    {
        var index = 0;
        foreach (var item in source)
            action(item, index++);
    }

    public static void ForEach1<T>(this IEnumerable<T> source, Action<T, int> action)
    {
        var index = 1;
        foreach (var item in source)
            action(item, index++);
    }

    public static void ForEach<T>(this IEnumerable<T> source, Func<T, LoopResult> action)
    {
        foreach (var item in source)
        {
            var result = action(item);
            switch (result)
            {
                case LoopResult.Break:
                    break;
                case LoopResult.Continue:
                    continue;
            }
        }
    }

    public static void ForEach<T>(this IEnumerable<T> source, Func<T, int, LoopResult> action)
    {
        var index = 0;
        foreach (var item in source)
        {
            var result = action(item, index++);
            switch (result)
            {
                case LoopResult.Break:
                    break;
                case LoopResult.Continue:
                    continue;
            }
        }
    }

    public static void ForEach1<T>(this IEnumerable<T> source, Func<T, int, LoopResult> action)
    {
        var index = 1;
        foreach (var item in source)
        {
            var result = action(item, index++);
            switch (result)
            {
                case LoopResult.Break:
                    break;
                case LoopResult.Continue:
                    continue;
            }
        }
    }

    public static bool ForEachBool<T>(this IEnumerable<T> source, Func<T, bool> func)
    {
        var result = true;
        foreach (var item in source)
        {
            if (!func(item))
                result = false;
        }
        return result;
    }

    public static bool ForEachBool<T>(this IEnumerable<T> source, Func<T, int, bool> func)
    {
        var result = true;
        var index = 0;
        foreach (var item in source)
        {
            if (!func(item, index++))
                result = false;
        }
        return result;
    }

    public static void For(this int count, Action<int> action)
    {
        for (var i = 0; i < count; i++)
        {
            action(i);
        }
    }

    public static void For1(this int count, Action<int> action)
    {
        for (var i = 1; i <= count; i++)
        {
            action(i);
        }
    }

    public static void For(this int count, Func<int, LoopResult> action)
    {
        for (var i = 0; i < count; i++)
        {
            var result = action(i);
            switch (result)
            {
                case LoopResult.Break:
                    break;
                case LoopResult.Continue:
                    continue;
            }
        }
    }

    public static void For1(this int count, Func<int, LoopResult> action)
    {
        for (var i = 1; i <= count; i++)
        {
            var result = action(i);
            switch (result)
            {
                case LoopResult.Break:
                    break;
                case LoopResult.Continue:
                    continue;
            }
        }
    }

    public static List<T> MakeList<T>(this int count, Func<int, T> func)
    {
        var result = new List<T>();
        for (var i = 0; i < count; i++)
        {
            result.Add(func(i));
        }
        return result;
    }
}

#if DEBUG // delete

public static class DebugUtils
{
    public static void CopyCode()
    {
        var programDirPath = Environment.CurrentDirectory;
        while (!File.Exists(Path.Combine(programDirPath, "Program.cs")))
        {
            programDirPath = Directory.GetParent(programDirPath).FullName;
        }
        var programPath = Path.Combine(programDirPath, "Program.cs");
        var sourceCode = File.ReadAllText(programPath);

        var filteredCode = sourceCode
            .DeleteDebug().DeleteDebug().DeleteDebug().DeleteDebug().DeleteDebug().DeleteDebug()
            .DeleteDebug().DeleteDebug().DeleteDebug().DeleteDebug().DeleteDebug().DeleteDebug()
            .DeleteDebug().DeleteDebug().DeleteDebug().DeleteDebug().DeleteDebug().DeleteDebug()
            ;

        Clipboard.SetText(filteredCode);
    }

    private static string DeleteDebug(this string sourceCode)
    {
        var beginIndex = sourceCode.IndexOf("#if DEBUG // delete");
        if (beginIndex == -1)
            return sourceCode;
        var endIf = "_endif".Replace("_", "#");
        var endIndex = sourceCode.IndexOf(endIf, beginIndex);

        return sourceCode.Substring(0, beginIndex - 1) + sourceCode.Substring(endIndex + 8);
    }
}

#region Input Output

public class InputOutput
{
    public int Number;
    public string Input;
    public string Output;
}

public static class BojUtils
{
    private static string GetHtml(this string problemNumber)
    {
        var client = new HttpClient();
        var task = client.GetStringAsync($"https://www.acmicpc.net/problem/{problemNumber}");
        task.Wait();
        return task.Result;
    }

    public static List<InputOutput> MakeInputOutput(string problemNumber, bool useLocalInput = false)
    {
        InputOutput localInput = null;
        if (useLocalInput)
        {
            localInput = new InputOutput
            {
                Number = 1,
                Input = File.ReadAllText("input.txt", Encoding.UTF8),
                Output = File.ReadAllText("output.txt", Encoding.UTF8),
            };
        }

        if (AppCache.Exists(problemNumber))
        {
            var cached = AppCache.GetCachedInputOutput(problemNumber);
            if (useLocalInput)
            {
                cached.Add(localInput);
            }
            return cached;
        }

        var sampleDic = new Dictionary<(string, string), string>();

        var sampleDataPattern = @"class\s*=\s*\""sampledata\""";
        var sampleNumberPattern = @"id\s*=\s*\""sample-(input|output)-(\d+)\""";

        var html = problemNumber.GetHtml();

        var startIndex = 0;
        while (true)
        {
            var a = html.IndexOf("<pre", startIndex);
            if (a == -1)
                break;
            var b = html.IndexOf(">", a);
            var c = html.IndexOf("</pre>", a);
            var preHeader = html.Substring(a, b - a);
            var preData = html.Substring(b + 1, c - b - 1).Trim();

            // preHeader.Dump();
            // preData.Dump();
            if (!Regex.IsMatch(preHeader, sampleDataPattern))
                continue;

            var match = Regex.Match(preHeader, sampleNumberPattern);
            if (match.Success)
            {
                var type = match.Groups[1].Captures[0].Value;
                var number = match.Groups[2].Captures[0].Value;
                // type.Dump();
                // number.Dump();
                sampleDic[(type, number)] = preData;
            }

            startIndex = a + 1;
        }
        // sampleDic.Dump();
        var result = sampleDic
            .GroupBy(x => new { Number = x.Key.Item2 })
            .Select(x => new InputOutput
            {
                Number = int.Parse(x.Key.Number),
                Input = x.First(e => e.Key.Item1 == "input").Value,
                Output = x.First(e => e.Key.Item1 == "output").Value,
            })
            .ToList();

        AppCache.SaveInputOutput(problemNumber, result);

        if (useLocalInput)
        {
            result.Add(localInput);
        }

        return result;
    }
}

public static class AppCache
{
    private static readonly string DirPath = Environment.CurrentDirectory;
    public static bool Exists(string problemNumber)
    {
        var problemPath = Path.Combine(DirPath, $"{problemNumber}.json");
        return File.Exists(problemPath);
    }

    public static List<InputOutput> GetCachedInputOutput(string problemNumber)
    {
        if (!Exists(problemNumber))
            return null;

        var problemPath = Path.Combine(DirPath, $"{problemNumber}.json");
        var text = File.ReadAllText(problemPath, Encoding.UTF8);
        return JsonConvert.DeserializeObject<List<InputOutput>>(text);
    }

    public static void SaveInputOutput(string problemNumber, List<InputOutput> data)
    {
        if (!Directory.Exists(DirPath))
        {
            Directory.CreateDirectory(DirPath);
        }

        var text = JsonConvert.SerializeObject(data, Formatting.Indented);
        var problemPath = Path.Combine(DirPath, $"{problemNumber}.json");

        File.WriteAllText(problemPath, text, Encoding.UTF8);
    }
}

#endregion

#endif
