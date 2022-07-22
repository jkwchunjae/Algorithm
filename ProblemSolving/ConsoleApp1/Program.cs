using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
#if DEBUG // delete
using System.Net.Http;
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
            var problemNumber = "17081";
            var inputOutputList = BojUtils.MakeInputOutput(problemNumber, useLocalInput: false);
            var checkAll = true;
            foreach (var inputOutput in inputOutputList)
            {
                IO.SetInputOutput(inputOutput);
#endif
                var result = Solve();
                result.Dump();
#if DEBUG // delete
                var correct = IO.IsCorrect().Dump();
                checkAll = checkAll && correct;
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

        public static string Solve()
        {
            var map = CreateMapFromInput();

            return string.Empty;
        }

        public static IMap CreateMapFromInput()
        {
            var (height, width) = IO.GetIntTuple2();
            var lines = height.MakeList(_ => IO.GetLine());
            var map = IMap.Create(height, width, lines);

            var moves = IO.GetLine().Select(chr => Ex.ToMoveType(chr)).ToList();

            var monsters = map.MonsterCount.MakeList(_ =>
            {
                var arr = IO.GetStringList();
                var row = arr[0].ToInt() - 1;
                var column = arr[1].ToInt() - 1;
                var name = arr[2];
                var w = arr[3].ToInt();
                var a = arr[4].ToInt();
                var h = arr[5].ToInt();
                var e = arr[6].ToInt();

                var position = new Position(row, column);
                var monster = new Monster($"{name} {w} {a} {h} {e}");

                return (position, monster);
            });
            var items = map.ItemCount.MakeList(_ =>
            {
                var arr = IO.GetStringList();
                var row = arr[0].ToInt() - 1;
                var column = arr[1].ToInt() - 1;

                var t = arr[2];
                var s = arr[3];

                var position = new Position(row, column);
                var item = new ItemBox($"{t} {s}");

                return (position, item);
            });

            map.UpdateMonsters(monsters);
            map.UpdateItems(items);

            return map;
        }
    }

    public enum MoveType
    {
        Left, Right, Up, Down
    }

    public record Position
    {
        public int Row { get; init; }
        public int Column { get; init; }
        public Position(int row, int column)
            => (Row, Column) = (row, column);
        public void Deconstruct(out int row, out int column)
            => (row, column) = (Row, Column);
    }

    #region Map
    public interface IMap
    {
        (int Height, int Width) Size { get; }
        // IEnumerable<IEnumerable<ICell>> Cells { get; }

        bool Movable(Position position);
        ICell GetCell(Position position);

        int MonsterCount { get; }
        int ItemCount { get; }

        void UpdateMonsters(List<(Position, Monster)> monsters);
        void UpdateItems(List<(Position, ItemBox)> items);

        string ToString();

        string ToString(IPlayer player);

        static IMap Create(int height, int width, List<string> input)
        {
            return new Map(height, width, input);
        }
    }

    public class Map : IMap
    {
        public Map(int height, int width, List<string> input)
        {
            Size = (height, width);
            _cells = input
                .Select((line, row) => line.Select((chr, column) => ICell.Create(row, column, chr)).ToList())
                .ToList();
        }

        public (int Height, int Width) Size { get; set; }

        public int MonsterCount => _cells
                .Sum(cells => cells.Count(cell => cell.Interactable is IMonster));

        public int ItemCount => _cells
                .Sum(cells => cells.Count(cell => cell.Interactable is IItemBox));

        private List<List<ICell>> _cells;

        public ICell GetCell(Position position)
        {
            return _cells[position.Row][position.Column];
        }

        public bool Movable(Position position)
        {
            if (position.Row < 0 || position.Column < 0)
                return false;
            if (position.Row >= Size.Height || position.Column >= Size.Width)
                return false;

            var cell = _cells[position.Row][position.Column];
            if (cell.Interactable is Wall)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public override string ToString()
        {
            return ToString(null);
        }

        public string ToString(IPlayer player)
        {
            return _cells
                .Select(row => row.Select(cell =>
                {
                    if (cell.Position == player?.Position)
                    {
                        return "@";
                    }
                    else
                    {
                        return cell.ToText();
                    }
                }).StringJoin(""))
                .StringJoin(Environment.NewLine);
        }

        public void UpdateMonsters(List<(Position, Monster)> monsters)
        {
            monsters.ForEach(data =>
            {
                var (position, monster) = data;
                _cells[position.Row][position.Column].Interactable = monster;
            });
        }

        public void UpdateItems(List<(Position, ItemBox)> items)
        {
            items.ForEach(data =>
            {
                var (position, itembox) = data;
                _cells[position.Row][position.Column].Interactable = itembox;
            });
        }
    }

    public interface ICell
    {
        Position Position { get; init; }

        IInteractable Interactable { get; set; }

        bool Interact(IPlayer player);

        static ICell Create(int row, int column, char chr)
        {
            return new Cell(new Position(row, column), chr);
        }
    }

    public class Cell : ICell
    {
        public Position Position { get; init; }
        public IInteractable Interactable { get; set; }

        public bool Interact(IPlayer player)
        {
            throw new NotImplementedException();
        }

        public Cell(Position position, char chr)
        {
            Position = position;
            Interactable = IInteractable.Create(chr);
        }
    }
    #endregion

    #region Interatable
    public interface IInteractable
    {
        InteractResult Interact(IPlayer player);
        static IInteractable Create(char chr)
        {
            return chr switch
            {
                '.' => new Blank(),
                '#' => new Wall(),
                'B' => new ItemBox(),
                '&' => new Monster(chr),
                'M' => new Monster(chr),
                '^' => new Trap(),
                _ => new Blank(),
            };
        }
    }

    public interface IBlank : IInteractable
    {

    }

    public class Blank : IBlank
    {
        public InteractResult Interact(IPlayer player)
        {
            return new InteractResult(false, false);
        }
    }

    public interface IWall : IInteractable
    {
    }

    public class Wall : IWall
    {
        public InteractResult Interact(IPlayer player)
        {
            return new InteractResult(false, false);
        }
    }

    public interface IItemBox : IInteractable
    {
    }

    public class ItemBox : IItemBox
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input">1 3 O DX 중 "O DX" 가 들어온다</param>
        public ItemBox(string input)
        {
        }

        public ItemBox() { }

        public InteractResult Interact(IPlayer player)
        {
            throw new NotImplementedException();
        }
    }

    public interface IMonster : IInteractable
    {
        bool IsBoss { get; }
    }

    public class Monster : IMonster
    {
        public bool IsBoss { get; init; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input">One 4 2 10 3 이 들어온다</param>
        public Monster(string input)
        {
        }
        public Monster(char chr)
        {
            if (chr == 'M')
            {
                IsBoss = true;
            }
        }
        public Monster() { }

        public InteractResult Interact(IPlayer player)
        {
            throw new NotImplementedException();
        }
    }

    public interface ITrap : IInteractable
    {
        public int Damage { get; init; }
    }

    public class Trap : ITrap
    {
        public int Damage { get; init; }
        
        public InteractResult Interact(IPlayer player)
        {

            var dead = player.DeadAfterTrap(this);

            return new InteractResult(dead, false);
        }
    }

    public class InteractResult
    {
        public InteractResult(bool dead, bool changeToBlank)
        {
            Dead = dead;
            ChangeToBlank = changeToBlank;
        }

        bool Dead { get; } // 부활까지 포함해서 최종적으로 죽었는지
        bool ChangeToBlank { get; }
    }
    #endregion

    #region Player
    public interface IPlayer
    {
        Position Position { get; set; }

        int Experience { get; set; }
        int Level { get; set; }

        int MaxHP { get; set; }
        int CurrentHP { get; set; }

        int AttackValue { get; set; }
        int DefenseValue { get; set; }

        IWeapon Weapon { get; set; }
        IArmor Armor { get; set; }
        IOrnament[] Ornaments { get; set; }

        bool DeadAfterTrap(ITrap trap);
        bool DeadAfterMonster(IMonster monster);

    }

    public class Player : IPlayer
    {
        public Position Position { get; set; }
        public int Experience { get; set; }
        public int Level { get; set; }
        public int MaxHP { get; set; }
        public int CurrentHP { get; set; }
        public int AttackValue { get; set; }
        public int DefenseValue { get; set; }
        public IWeapon Weapon { get; set; }
        public IArmor Armor { get; set; }
        public IOrnament[] Ornaments { get; set; }

        public bool DeadAfterMonster(IMonster monster)
        {
            throw new NotImplementedException();
        }

        public bool DeadAfterTrap(ITrap trap)
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    #region Item
    public interface IItem
    {
    }

    public interface IWeapon : IItem
    {
    }

    public class Weapon : IWeapon
    {
    }

    public interface IArmor : IItem
    {
    }

    public class Armor : IArmor
    {
    }

    public interface IOrnament : IItem
    {
    }

    public class OrnamentHpRegeneration : IOrnament
    {
    }

    public class OrnamentReincarnation : IOrnament
    {
    }

    public class OrnamentCourage : IOrnament
    {
    }

    public class OrnamentExperience : IOrnament
    {
    }

    public class OrnamentDexterity : IOrnament
    {
    }

    public class OrnamentHunter : IOrnament
    {
    }

    public class OrnamentCursed : IOrnament
    {
    }
    #endregion

    #region Ex
    public static partial class Ex
    {
        public static string ToText(this ICell cell)
        {
            switch (cell.Interactable)
            {
                case IBlank: return ".";
                case IWall: return "#";
                case IItemBox: return "B";
                case IMonster monster: return monster.IsBoss ? "M" : "&";
                case ITrap: return "^";
                default: return ".";
            }
        }

        public static MoveType ToMoveType(char chr)
        {
            return chr switch
            {
                'L' => MoveType.Left,
                'R' => MoveType.Right,
                'U' => MoveType.Up,
                'D' => MoveType.Down,
                _ => throw new Exception($"LRUD말고 다른 값이 들어왔음: {chr}"),
            };
        }
    }
    #endregion

    #region IO
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
    #endregion

    #region Extensions
    public enum LoopResult
    {
        Void,
        Break,
        Continue,
    }

    public static class Extensions
    {
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

        public static IEnumerable<(TItem Item1, TItem Item2)> AllPairs<TItem>(this List<TItem> source, bool includeDuplicate = false)
        {
            for (var i = 0; i < source.Count(); i++)
            {
                var item1 = source[i];
                for (var j = i + (includeDuplicate ? 0 : 1); j < source.Count(); j++)
                {
                    var item2 = source[j];
                    yield return (item1, item2);
                }
            }
        }

        public static IEnumerable<(TItem Item1, TItem Item2)> AllPairs<TItem>(this TItem[] source, bool includeDuplicate = false)
        {
            for (var i = 0; i < source.Count(); i++)
            {
                var item1 = source[i];
                for (var j = i + (includeDuplicate ? 0 : 1); j < source.Count(); j++)
                {
                    var item2 = source[j];
                    yield return (item1, item2);
                }
            }
        }

        public static bool Empty<TSource>(this IEnumerable<TSource> source)
        {
            return !source.Any();
        }

        public static bool Empty<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return !source.Any(predicate);
        }
    }
    #endregion

    #region Utils
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
    #endregion
}
