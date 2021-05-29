using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
#if DEBUG // delete
using System.Net.Http;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using WinFormsLibrary1;
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
        var problemNumber = "11438";
        var inputOutputList = BojUtils.MakeInputOutput(problemNumber, useLocalInput: false);
        var checkAll = true;
        foreach (var inputOutput in inputOutputList)
        {
            IO.SetInputOutput(inputOutput);
#endif

            var N = IO.GetInt();
            var nodes = (N + 1).MakeList(i => new Node(i));
            (N - 1).For(_ =>
            {
                var (n1, n2) = IO.GetIntTuple2();
                var node1 = nodes[n1];
                var node2 = nodes[n2];

                node1.NodeList.Add(node2);
                node2.NodeList.Add(node1);
            });

            var M = IO.GetInt();
            var list = M.MakeList(_ =>
            {
                var (n1, n2) = IO.GetIntTuple2();
                var node1 = nodes[n1];
                var node2 = nodes[n2];

                return (node1, node2);
            });

            Solve(nodes[1], list);

#if DEBUG // delete
            var result = IO.IsCorrect().Dump();
            checkAll = checkAll && result;
            Console.WriteLine();
        }
        DebugUtils.CopyCode();
        if (checkAll)
        {
            var url = $"https://www.acmicpc.net/submit/{problemNumber}";
            Process.Start(new ProcessStartInfo($"powershell", $"start chrome {url}"));
        }
#endif
    }

    public static void Solve(Node root, List<(Node Node1, Node Node2)> nodes)
    {
        root.SetParent();
        nodes.ForEach(x =>
        {
            var lca = GetLCA(x.Node1, x.Node2);
            lca.Number.Dump();
        });
        return;
    }

    public static Node GetLCA(Node node1, Node node2)
    {
        List<Node> list1 = new();
        List<Node> list2 = new();

        while (node1 != null && node1.Number != 0)
        {
            list1.Add(node1);
            node1 = node1.Parent;
        }
        while (node2 != null && node2.Number != 0)
        {
            list2.Add(node2);
            node2 = node2.Parent;
        }

        if (list1.Count > list2.Count)
        {
            var tmp = list1;
            list1 = list2;
            list2 = tmp;
        }

        var index1 = 0;
        var index2 = list2.Count - list1.Count;

        while (index1 < list1.Count)
        {
            if (list1[index1] == list2[index2])
                break;
            index1++;
            index2++;
        }

        return list1[index1];
    }
}

public static class Extensionss
{
}

public class Node
{
    public int Number;
    public bool Mark = false;
    public Node Parent;
    public List<Edge> EdgeList = new();
    public List<Node> NodeList = new();

    public Node(int number)
    {
        Number = number;
    }

    public void SetParent()
    {
        foreach(var node in NodeList.Where(x => x.Number != Parent?.Number))
        {
            node.Parent = this;
            node.SetParent();
        }
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
    static List<string> _input = new();
#if DEBUG // delete
    static string _answer;
    static string _output = "";
    static int _readInputCount = 0;

    public static bool IsCorrect() => _output.Replace("\r", "").Trim() == _answer.Replace("\r", "").Trim();
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
        var input = Console.ReadLine();
        _input.Add(input);
        return input;
#endif
    }

    public static string GetInput()
    {
        return string.Join(' ', _input);
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

    public static string Left(this string value, int length = 1)
    {
        if (value.Length < length)
            return value;
        return value.Substring(0, length);
    }

    public static string Right(this string value, int length = 1)
    {
        if (value.Length < length)
            return value;
        return value.Substring(value.Length - length, length);
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
    public static TResult Reduce<TSource, TResult>(this IEnumerable<TSource> source, TResult initValue, Func<TResult, TSource, TResult> fn)
    {
        return Reduce(source, initValue, (value, item, index, list) => fn(value, item));
    }

    public static TResult Reduce<TSource, TResult>(this IEnumerable<TSource> source, TResult initValue, Func<TResult, TSource, int, TResult> fn)
    {
        return Reduce(source, initValue, (value, item, index, list) => fn(value, item, index));
    }

    public static TResult Reduce<TSource, TResult>(this IEnumerable<TSource> source, TResult initValue, Func<TResult, TSource, int, IEnumerable<TSource>, TResult> fn)
    {
        var value = initValue;

        var index = 0;
        foreach (var item in source)
        {
            value = fn(value, item, index++, source);
        }

        return value;
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

        Helper.CopyText(filteredCode);
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
        var localInputList = new List<InputOutput>();
        if (useLocalInput)
        {
            var inputText = File.ReadAllText("input.txt", Encoding.UTF8);
            var outputText = File.ReadAllText("output.txt", Encoding.UTF8);

            Func<string, List<string>> Normalize = text =>
                Regex.Split(text.Replace("\r", ""), "\n\n")
                    .Select(x => x.Trim())
                    .Where(x => !string.IsNullOrEmpty(x))
                    .ToList();

            var inputList = Normalize(inputText);
            var outputList = Normalize(outputText);

            if (inputList.Count != outputList.Count)
            {
                throw new Exception("input, output 개수가 다릅니다.");
            }

            localInputList = inputList
                .Zip(outputList, (input, output) => new InputOutput
                {
                    Number = 0,
                    Input = input,
                    Output = output,
                })
                .ToList();
        }

        if (AppCache.Exists(problemNumber))
        {
            var cached = AppCache.GetCachedInputOutput(problemNumber);
            if (useLocalInput)
            {
                cached = localInputList.Concat(cached).ToList();
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
            result = localInputList.Concat(result).ToList();
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
