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
#endif

public class Program
{
#if DEBUG // delete
    [STAThread]
#endif
    public static void Main(string[] args)
    {
#if DEBUG // delete
        var inputOutputList = BojUtils.MakeInputOutput("17969", useLocalInput: false);
        foreach (var inputOutput in inputOutputList)
        {
            IO.SetInputOutput(inputOutput);
#endif
            Solve();
#if DEBUG // delete
            IO.IsCorrect().Dump();
            Console.WriteLine();
        }
        DebugUtils.CopyCode();
#endif
    }

    public static void Solve()
    {
        var nodeCount = IO.GetInt();
        var edgeList = new List<Edge>();
        for (var i = 0; i < nodeCount - 1; i++)
        {
            var arr = IO.GetIntList();
            edgeList.Add(new Edge
            {
                Node1 = arr[0] - 1,
                Node2 = arr[1] - 1,
                Weight = arr[2],
            });
        }
        var result = Solve(edgeList, nodeCount);
        result.Dump();
    }

    public static long Solve(List<Edge> edgeList, int nodeCount)
    {
        var nodeList = new Node[nodeCount];
        for (var i = 0; i < nodeCount; i++)
            nodeList[i] = new Node(i);

        edgeList.ForEach(edge =>
        {
            nodeList[edge.Node1].EdgeList.Add(edge);
            nodeList[edge.Node2].EdgeList.Add(edge);
        });

        var rootNode = nodeList.First(node => !node.IsLeaf);

        var result = GetTreeResult(nodeList, rootNode, -1, rootNode.Number);

        return result.Result;
    }

    public static TreeResult GetTreeResult(Node[] tree, Node currentNode, int parentNodeNumber, int rootNodeNumber)
    {
        if (currentNode.IsLeaf)
        {
            return new TreeResult
            {
                LeafCount = 1,
                Sum = currentNode.EdgeList[0].Weight,
                SquareSum = currentNode.EdgeList[0].Weight * currentNode.EdgeList[0].Weight,
                Result = currentNode.EdgeList[0].Weight,
            };
        }

        TreeResult result = null;
        Edge parentEdge = null;
        foreach (var edge in currentNode.EdgeList)
        {
            if (edge.Node1 == parentNodeNumber || edge.Node2 == parentNodeNumber)
            {
                parentEdge = edge;
            }
            else
            {
                var childNumber = edge.Node1 == currentNode.Number ? edge.Node2 : edge.Node1;
                var childResult = GetTreeResult(tree, tree[childNumber], currentNode.Number, rootNodeNumber);
                if (result == null)
                {
                    result = childResult;
                }
                else
                {
                    result = new TreeResult
                    {
                        LeafCount = result.LeafCount + childResult.LeafCount,
                        Sum = result.Sum + childResult.Sum,
                        SquareSum = result.SquareSum + childResult.SquareSum,
                        Result = childResult.LeafCount * result.SquareSum
                            + result.LeafCount * childResult.SquareSum
                            + 2 * result.Sum * childResult.Sum
                            + (result.LeafCount > 1 ? result.Result : 0)
                            + (childResult.LeafCount > 1 ? childResult.Result : 0)
                            ,
                    };
                }
            }
        }

        if (currentNode.Number != rootNodeNumber)
        {
            var sum = result.Sum + parentEdge.Weight * result.LeafCount;
            var squareSum = result.SquareSum + 2 * parentEdge.Weight * result.Sum + result.LeafCount * parentEdge.Weight * parentEdge.Weight;
            result.Sum = sum;
            result.SquareSum = squareSum;
        }

        return result;
    }
}

public class Edge
{
    public int Node1;
    public int Node2;
    public int Weight;
}

public class Node
{
    public int Number;
    public List<Edge> EdgeList = new List<Edge>();
    public bool IsLeaf => EdgeList.Any() && EdgeList.Count == 1;

    public Node(int number)
    {
        Number = number;
    }
}

public class TreeResult
{
    public int LeafCount;
    public long Sum;
    public long SquareSum;
    public long Result;
}

public static class Extensionss
{
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
        var endIf = new List<string> { "#", "e", "n", "d", "i", "f" }.StringJoin("");
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
        if (useLocalInput)
        {
            return new List<InputOutput>
            {
                new InputOutput
                {
                    Number = 1,
                    Input = File.ReadAllText("input.txt", Encoding.UTF8),
                    Output = File.ReadAllText("output.txt", Encoding.UTF8),
                },
            };
        }

        if (AppCache.Exists(problemNumber))
        {
            return AppCache.GetCachedInputOutput(problemNumber);
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
            var preData = html.Substring(b + 1, c - b - 2).Trim();

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

        return result;
    }
}

public static class AppCache
{
    private static readonly string DirPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Boj");
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
