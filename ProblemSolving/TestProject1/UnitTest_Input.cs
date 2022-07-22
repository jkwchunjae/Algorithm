using ConsoleApp1;
using System.Linq;
using Xunit;

namespace TestProject1;

public class UnitTest_Input
{
    readonly string _input1 = @"7 8
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

    [Fact]
    public void InputTest_MapInstance()
    {
        var input = _input1;
        IO.SetInputOutput(new InputOutput { Input = input });

        var map = Program.CreateMapFromInput(out var moves, out var player);
        var height1 = input.Split('\n').First().Split(' ')[0].ToInt();
        var width1 = input.Split('\n').First().Split(' ')[1].ToInt();
        var input1 = input.Split('\n').Skip(1).Take(map.Size.Height).ToList();

        Assert.NotNull(map);
        Assert.Equal((height1, width1), map.Size);
    }

    [Fact]
    public void InputTest_PlayerPosition()
    {
        var input = _input1;
        IO.SetInputOutput(new InputOutput { Input = input });

        var map = Program.CreateMapFromInput(out var moves, out var player);

        Assert.Equal(new Position(3, 2), player.Position);
    }

    [Fact]
    public void InputTest_CellInteractableType()
    {
        var input = _input1;
        IO.SetInputOutput(new InputOutput { Input = input });

        var map = Program.CreateMapFromInput(out var moves, out var player);
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
    }

    [Fact]
    public void InputTest_Items()
    {
        var input = _input1;
        IO.SetInputOutput(new InputOutput { Input = input });

        var map = Program.CreateMapFromInput(out var moves, out var player);

        Assert.IsType<ItemBox>(map.GetCell(new Position(3, 4)).Interactable);
        if (map.GetCell(new Position(3, 4)).Interactable is ItemBox itembox1)
            Assert.IsType<Weapon>(itembox1.Item);

        Assert.IsType<ItemBox>(map.GetCell(new Position(1, 2)).Interactable);
        if (map.GetCell(new Position(1, 2)).Interactable is ItemBox itembox2)
            Assert.IsType<OrnamentCourage>(itembox2.Item);

        Assert.IsType<ItemBox>(map.GetCell(new Position(2, 0)).Interactable);
        if (map.GetCell(new Position(2, 0)).Interactable is ItemBox itembox3)
            Assert.IsType<Armor>(itembox3.Item);

        Assert.IsType<ItemBox>(map.GetCell(new Position(3, 1)).Interactable);
        if (map.GetCell(new Position(3, 1)).Interactable is ItemBox itembox4)
            Assert.IsType<Armor>(itembox4.Item);

        Assert.IsType<ItemBox>(map.GetCell(new Position(5, 1)).Interactable);
        if (map.GetCell(new Position(5, 1)).Interactable is ItemBox itembox5)
            Assert.IsType<OrnamentDexterity>(itembox5.Item);

        Assert.IsType<ItemBox>(map.GetCell(new Position(6, 2)).Interactable);
        if (map.GetCell(new Position(6, 2)).Interactable is ItemBox itembox6)
            Assert.IsType<OrnamentHunter>(itembox6.Item);

        Assert.IsType<ItemBox>(map.GetCell(new Position(5, 5)).Interactable);
        if (map.GetCell(new Position(5, 5)).Interactable is ItemBox itembox7)
            Assert.IsType<Weapon>(itembox7.Item);
    }

    [Fact]
    public void InputTest_Monsters()
    {
        var input = _input1;
        IO.SetInputOutput(new InputOutput { Input = input });

        var map = Program.CreateMapFromInput(out var moves, out var player);

        Assert.IsType<Monster>(map.GetCell(new Position(2, 4)).Interactable);
        if (map.GetCell(new Position(2, 4)).Interactable is Monster monster1)
        {
            Assert.Equal("One", monster1.Name);
            Assert.False(monster1.IsBoss);
        }

        Assert.IsType<Monster>(map.GetCell(new Position(1, 4)).Interactable);
        if (map.GetCell(new Position(1, 4)).Interactable is Monster monster2)
        {
            Assert.Equal("Two", monster2.Name);
            Assert.False(monster2.IsBoss);
        }

        Assert.IsType<Monster>(map.GetCell(new Position(0, 1)).Interactable);
        if (map.GetCell(new Position(0, 1)).Interactable is Monster monster3)
        {
            Assert.Equal("Three", monster3.Name);
            Assert.False(monster3.IsBoss);
        }

        Assert.IsType<Monster>(map.GetCell(new Position(4, 1)).Interactable);
        if (map.GetCell(new Position(4, 1)).Interactable is Monster monster4)
        {
            Assert.Equal("Four", monster4.Name);
            Assert.False(monster4.IsBoss);
        }

        Assert.IsType<Monster>(map.GetCell(new Position(6, 5)).Interactable);
        if (map.GetCell(new Position(6, 5)).Interactable is Monster monster5)
        {
            Assert.Equal("Five", monster5.Name);
            Assert.False(monster5.IsBoss);
        }

        Assert.IsType<Monster>(map.GetCell(new Position(4, 6)).Interactable);
        if (map.GetCell(new Position(4, 6)).Interactable is Monster monster6)
        {
            Assert.Equal("Boss", monster6.Name);
            Assert.True(monster6.IsBoss);
        }

        Assert.IsType<Monster>(map.GetCell(new Position(0, 6)).Interactable);
        if (map.GetCell(new Position(0, 6)).Interactable is Monster monster7)
        {
            Assert.Equal("EO", monster7.Name);
            Assert.False(monster7.IsBoss);
        }

        Assert.IsType<Monster>(map.GetCell(new Position(1, 7)).Interactable);
        if (map.GetCell(new Position(1, 7)).Interactable is Monster monster8)
        {
            Assert.Equal("ET", monster8.Name);
            Assert.False(monster8.IsBoss);
        }
    }
}
