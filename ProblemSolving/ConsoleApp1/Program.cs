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

namespace ConsoleApp1
{
    public class Program
    {
#if DEBUG // delete
        [STAThread]
#endif
        public static int Main(string[] args)
        {
            using var io = new IoInstance();
#if DEBUG // delete
            var problemNumber = "1289";
            var inputOutputList = BojUtils.MakeInputOutput(problemNumber, useLocalInput: true);
            var checkAll = true;
            foreach (var inputOutput in inputOutputList)
            {
                IO.SetInputOutput(inputOutput);
#endif
                var N = IO.GetInt();
                var nodeInfo = (N - 1).MakeList(_ =>
                {
                    var info = IO.GetIntTuple3();
                    return (info.Item1 - 1, info.Item2 - 1, info.Item3);
                });
                Solve(N, nodeInfo).Dump();
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
            return 0;
        }

        public static long Solve(int N, List<(int Node1, int Node2, int Weight)> nodeInfo)
        {
            var root = MakeTree(N, nodeInfo);

            var treeWeight = root.GetTreeWeight();

            return treeWeight;
        }

        private static Node MakeTree(int N, List<(int Node1, int Node2, int Weight)> nodeInfo)
        {
            var nodes = N.MakeList(i => new Node(i));

            nodeInfo.ForEach(info =>
            {
                var node1 = nodes[info.Node1];
                var node2 = nodes[info.Node2];

                node1.Children.Add((node2, info.Weight));
                node2.Children.Add((node1, info.Weight));
            });

            nodes.First().SetParent(null);

            return nodes.First();
        }
    }

    public class Node
    {
        public int Index;
        public long Sum = 0;

        public Node Parent;
        public List<(Node Node, long Weight)> Children = new();

        public Node(int index)
        {
            Index = index;
        }

        public void SetParent(Node parent)
        {
            Parent = parent;
            //var index = Children.FindIndex(x => x.Node == parent);
            //if (index != -1)
            //{
            //    Children.RemoveAt(index);
            //}

            Children.Where(child => child.Node != parent)
                .ForEach(child => child.Node.SetParent(this));
        }

        public long GetTreeWeight()
        {
            long M = 1_000_000_007;
            var treeWeight = 0L;

            Children.Where(child => child.Node != Parent).ForEach(child =>
            {
                treeWeight = (treeWeight + child.Node.GetTreeWeight()) % M;
            });
            for (var i = 0; i < Children.Count; i++)
            {
                var child1 = Children[i];
                if (child1.Node == Parent)
                    continue;

                for (var j = i + 1; j < Children.Count; j++)
                {
                    var child2 = Children[j];
                    if (child2.Node == Parent)
                        continue;

                    // Child1과 Child2의 관계에서 나오는 Tree Weight.
                    var crossWeight = (((((child1.Node.Sum * child2.Node.Sum + child1.Node.Sum + child2.Node.Sum + 1) % M) * child1.Weight) % M) * child2.Weight) % M;
                    treeWeight = (treeWeight + crossWeight) % M;
                }

                // root 부터 child1밑의 자손들까지 거리
                var rootWeight = (child1.Node.Sum * child1.Weight) % M;
                treeWeight = (treeWeight + rootWeight) % M;

                // root 부터 child1까지 거리
                treeWeight = (treeWeight + child1.Weight) % M;
            }

            Children.Where(child => child.Node != Parent).ForEach(child1 =>
            {
                var childSum = (child1.Node.Sum * child1.Weight) % M;
                Sum = (Sum + childSum) % M; // root 부터 child1의 자손들까지 합
                Sum = (Sum + child1.Weight) % M; // root 부터 child1 까지 거리
            });

            return treeWeight % M;
        }
    }

    public static partial class Ex
    {
    }

    public class IoInstance : IDisposable
    {
        public void Dispose()
        {
#if !DEBUG
            IO.Dispose();
#endif
        }
    }

    public static class IO
    {
#if DEBUG // delete
        static List<string> _input = new();
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

#if !DEBUG
        static StreamReader _inputReader;
        static StringBuilder _outputBuffer = new();

        static IO()
        {
            _inputReader = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        }
#endif

        public static string GetLine()
        {
#if DEBUG
            return _input[_readInputCount++];
#else
            return _inputReader.ReadLine();
#endif
        }

        public static string GetString()
            => GetLine();

        public static string[] GetStringList()
            => GetLine().Split(' ');

        public static (string, string) GetStringTuple2()
        {
            var arr = GetStringList();
            return (arr[0], arr[1]);
        }

        public static (string, string, string) GetStringTuple3()
        {
            var arr = GetStringList();
            return (arr[0], arr[1], arr[2]);
        }

        public static (string, string, string, string) GetStringTuple4()
        {
            var arr = GetStringList();
            return (arr[0], arr[1], arr[2], arr[3]);
        }

        public static int[] GetIntList()
            => GetLine().Split(' ').Where(x => x.Length > 0).Select(x => x.ToInt()).ToArray();

        public static (int, int) GetIntTuple2()
        {
            var arr = GetLine().Split(' ');
            return (arr[0].ToInt(), arr[1].ToInt());
        }

        public static (int, int, int) GetIntTuple3()
        {
            var arr = GetLine().Split(' ');
            return (arr[0].ToInt(), arr[1].ToInt(), arr[2].ToInt());
        }

        public static (int, int, int, int) GetIntTuple4()
        {
            var arr = GetLine().Split(' ');
            return (arr[0].ToInt(), arr[1].ToInt(), arr[2].ToInt(), arr[3].ToInt());
        }

        public static int GetInt()
            => GetLine().ToInt();

        public static long[] GetLongList()
            => GetLine().Split(' ').Where(x => x.Length > 0).Select(x => x.ToLong()).ToArray();

        public static (long, long) GetLongTuple2()
        {
            var arr = GetLine().Split(' ');
            return (arr[0].ToLong(), arr[1].ToLong());
        }

        public static (long, long, long) GetLongTuple3()
        {
            var arr = GetLine().Split(' ');
            return (arr[0].ToLong(), arr[1].ToLong(), arr[2].ToLong());
        }

        public static (long, long, long, long) GetLongTuple4()
        {
            var arr = GetLine().Split(' ');
            return (arr[0].ToLong(), arr[1].ToLong(), arr[2].ToLong(), arr[3].ToLong());
        }

        public static long GetLong()
            => GetLine().ToLong();

        public static T Dump<T>(this T obj, string format = "")
        {
            var text = string.IsNullOrEmpty(format) ? $"{obj}" : string.Format(format, obj);
#if DEBUG // delete
            Console.WriteLine(text);
            _output += Environment.NewLine + text;
#endif
#if !DEBUG
            _outputBuffer.AppendLine(text);
#endif
            return obj;
        }

        public static List<T> Dump<T>(this List<T> list)
        {
#if DEBUG // delete
            Console.WriteLine(list.StringJoin(" "));
#endif
#if !DEBUG
            _outputBuffer.AppendLine(list.StringJoin(" "));
#endif
            return list;
        }

#if !DEBUG
        public static void Dispose()
        {
            _inputReader.Close();
            using var streamWriter = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
            streamWriter.Write(_outputBuffer.ToString());
        }
#endif
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

    public static partial class Ex
    {
        public static int Ccw(Point a, Point b, Point c)
        {
            // 출처: https://jason9319.tistory.com/358 [ACM-ICPC 상 탈 사람]
            var op = a.X * b.Y + b.X * c.Y + c.X * a.Y;
            op -= (a.Y * b.X + b.Y * c.X + c.Y * a.X);
            if (op > 0) return 1;
            else if (op == 0) return 0;
            else return -1;
        }

        public static bool IsIntersect(Line line1, Line line2)
        {
            // 출처: https://jason9319.tistory.com/358 [ACM-ICPC 상 탈 사람]
            var a = line1.P1;
            var b = line1.P2;
            var c = line2.P1;
            var d = line2.P2;
            int ab = Ccw(a, b, c) * Ccw(a, b, d);
            int cd = Ccw(c, d, a) * Ccw(c, d, b);
            if (ab == 0 && cd == 0)
            {
                if (a > b) (a, b) = Swap(a, b);
                if (c > d) (c, d) = Swap(c, d);
                return c <= b && a <= d;
            }
            return ab <= 0 && cd <= 0;
        }

        public static (T b, T a) Swap<T>(T a, T b)
        {
            return (b, a);
        }

        public static (bool found, long x, long y) FindDiophantusEquation(long a, long b, long c)
        {
            // https://m.blog.naver.com/PostView.naver?isHttpsRedirect=true&blogId=beneys&logNo=221122957338

            var initA = a;
            var initB = b;

            var list = new List<(long A, long B, long M, long R)>();
            do
            {
                var m = a / b;
                var r = a % b;
                list.Add((a, b, m, r));
                a = b;
                b = r;
            } while (a % b != 0);

            var gdc = list.Last().R;
            if (c % gdc != 0)
            {
                return (false, 0, 0);
            }

            //list.Dump();
            list.Reverse();

            var first = list.First();
            var list2 = new List<(long A, long X, long B, long Y)>
            {
                (first.A, 1, first.B, -first.M)
            };
            foreach (var (A, B, M, R) in list.Skip(1))
            {
                var prev = list2.Last();
                if (R == prev.A)
                {
                    var nextA = A;
                    var nextX = prev.X;
                    var nextB = B;
                    var nextY = prev.Y + (-M) * prev.X;
                    list2.Add((nextA, nextX, nextB, nextY));
                }
                else // if (R == prev.B)
                {
                    var nextA = B;
                    var nextX = prev.X + (-M) * prev.Y;
                    var nextB = A;
                    var nextY = prev.Y;
                    list2.Add((nextA, nextX, nextB, nextY));
                }
            }
            //list2.Dump();

            var mm = c / gdc;
            var last = list2.Last();
            var x = (initA == last.A ? last.X : last.Y) * mm;
            var y = (initA == last.A ? last.Y : last.X) * mm;

            //((initA * x + initB * y)).Dump("C: " + c);

            return (true, x, y);
        }

        public static long ChineseRemainderTheorem(List<(long A, long M)> arr)
        {
            // https://j1w2k3.tistory.com/1340
            var M = arr.Select(x => x.M).Aggregate((a, b) => a * b);
            var nList = arr.Select(x => M / x.M).ToList();

            var xxxList = arr
                .Zip(nList, (condition, N) => new
                {
                    condition.A,
                    N,
                    Dio = FindDiophantusEquation(N, condition.M, 1), // 특수해
                })
                .ToList();

            long x = 0;
            foreach (var xxx in xxxList)
            {
                x += (xxx.A * xxx.N * xxx.Dio.x) % M;
                x %= M;
            }

            return x;
        }
    }

    public class Matrix
    {
        private List<List<long>> Value;

        public int Row => Value.Count;
        public int Column => Value.First().Count;

        public Matrix(int row, int column)
        {
            Value = row.MakeList(_ => column.MakeList(_ => 0L));
        }

        public Matrix(int row, int column, params int[] values)
            : this(row, column)
        {
            var index = 0;
            row.For(r => column.For(c => Value[r][c] = values[index++]));
        }

        public Matrix(List<List<long>> value)
        {
            Value = value;
        }

        public List<long> this[int row] => Value[row];

        public static Matrix operator * (Matrix m1, Matrix m2)
        {
            var result = m1.Row.MakeList(r =>
            {
                return m2.Column.MakeList(c =>
                {
                    long sum = 0;
                    m1.Column.For(k =>
                    {
                        sum += m1[r][k] * m2[k][c];
                    });
                    return sum;
                });
            });

            return new Matrix(result);
        }

        public Matrix Pow(int N)
        {
            var 항등원 = new Matrix(Row.MakeList(r => Column.MakeList(c => r == c ? 1L : 0L)));

            var result = MathEx.Pow(this, N, 항등원, (m1, m2) => m1 * m2);

            return result;
        }

        public Matrix Pow(int N, Func<Matrix, Matrix, Matrix> fnMultifly)
        {
            var 항등원 = new Matrix(Row.MakeList(r => Column.MakeList(c => r == c ? 1L : 0L)));

            var result = MathEx.Pow(this, N, 항등원, fnMultifly);

            return result;
        }

    }

    public static class MathEx
    {

        /// <summary>
        /// pow1의 exp제곱을 구한다. \n
        /// 2^10 = 2.Pow(10, 1, (a, b) => a * b);
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="base">밑</param>
        /// <param name="exp">지수</param>
        /// <param name="pow0">밑의 0제곱</param>
        /// <param name="fnMultifly">곱셈연산</param>
        /// <returns></returns>
        public static T Pow<T>(this T @base, int exp, T pow0, Func<T, T, T> fnMultifly)
        {
            // Addition-Chain exponentiation

            var basee = @base;
            var res = pow0;

            while (exp > 0)
            {
                if ((exp & 1) != 0)
                    res = fnMultifly(res, basee);
                exp >>= 1;
                basee = fnMultifly(basee, basee);
            }

            return res;
        }

        public static int Pow(this int @base, int exp)
        {
            return @base.Pow(exp, 1, (a, b) => a * b);
        }

        public static long Pow(this long @base, int exp)
        {
            return @base.Pow(exp, 1, (a, b) => a * b);
        }
        public static long Gcd(long a, long b)
        {
            if (a == b) { return a; }
            else if (a > b && a % b == 0) { return b; }
            else if (b > a && b % a == 0) { return a; }

            long _gcd = 0;
            while (b != 0)
            {
                _gcd = b;
                b = a % b;
                a = _gcd;
            }
            return _gcd;
        }

        public static long Lcm(long a, long b)
        {
            var gcd = Gcd(a, b);
            var lcm = (a / gcd) * b;
            return lcm;
        }

    }

    public class Line
    {
        public Point P1;
        public Point P2;

        public long Length => Math.Abs(P1.X - P2.X) + Math.Abs(P1.Y - P2.Y);
    }

    public record Point
    {
        public long X;
        public long Y;
        public Point() { }
        public Point(long x, long y)
        {
            X = x;
            Y = y;
        }

        public static bool operator <(Point a, Point b)
        {
            if (a.X < b.X)
                return true;
            else if (a.X == b.X && a.Y < b.Y)
                return true;
            return false;
        }

        public static bool operator <=(Point a, Point b)
        {
            if (a.X < b.X)
                return true;
            else if (a.X == b.X && a.Y <= b.Y)
                return true;
            return false;
        }

        public static bool operator >(Point a, Point b)
        {
            return !(a <= b);
        }

        public static bool operator >=(Point a, Point b)
        {
            return !(a < b);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
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
                preData = preData.Replace("&quot;", "\"");

                // preHeader.Dump();
                // preData.Dump();
                if (!Regex.IsMatch(preHeader, sampleDataPattern))
                {
                    startIndex = a + 1;
                    continue;
                }

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
}
