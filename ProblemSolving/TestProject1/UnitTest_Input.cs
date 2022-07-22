using ConsoleApp1;
using System.Linq;
using Xunit;

namespace TestProject1;

public class UnitTest_Input
{
    [Fact]
    public void InputTest1()
    {
        var input = @"7 8
.&....&.
..B.&..&
B...&...
.B@.B#..
.&....M.
.B...B..
..B^^&..
RRRUULLULUDDDLDRDRDRRRURRULUULLU
3 5 One 4 2 10 3
2 5 Two 10 2 8 3
1 2 Three 20 2 14 7
5 2 Four 16 2 16 5
7 6 Five 16 5 16 12
5 7 Boss 2 9 20 2
1 7 EO 20 1 1 4
2 8 ET 10 5 4 10
4 5 W 4
2 3 O CO
3 1 A 10
4 2 A 2
6 2 O DX
7 3 O HU
6 6 W 3".Replace("\r", "");
        IO.SetInputOutput(new InputOutput { Input = input });

        var map = Program.CreateMapFromInput();
        var height1 = input.Split('\n').First().Split(' ')[0].ToInt();
        var width1 = input.Split('\n').First().Split(' ')[1].ToInt();
        var input1 = input.Split('\n').Skip(1).Take(map.Size.Height).ToList();

        for (int row = 0; row < map.Size.Height; row++)
        {
            for (int col = 0; col < map.Size.Width; col++)
            {
                var cell = map.GetCell(new Position(row, col));
                switch (input1[row][col])
                {
                    case '@':
                    case '.':
                        Assert.IsType<Blank>(cell.Interactable);
                        break;
                    case 'B':
                        Assert.IsType<ItemBox>(cell.Interactable);
                        break;
                    case '&':
                        Assert.IsType<Monster>(cell.Interactable);
                        var monster = cell.Interactable as Monster;
                        Assert.False(monster.IsBoss);
                        break;
                    case 'M':
                        Assert.IsType<Monster>(cell.Interactable);
                        var boss = cell.Interactable as Monster;
                        Assert.True(boss.IsBoss);
                        break;
                    case '^':
                        Assert.IsType<Trap>(cell.Interactable);
                        break;
                    case '#':
                        Assert.IsType<Wall>(cell.Interactable);
                        break;
                    default:
                        Assert.True(false);
                        break;
                }
            }
        }

        Assert.Equal(height1, map.Size.Height);
        Assert.Equal(width1, map.Size.Width);

        Assert.NotNull(map);
    }
}
