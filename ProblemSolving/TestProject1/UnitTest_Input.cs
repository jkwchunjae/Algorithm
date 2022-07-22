﻿using ConsoleApp1;
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
6 6 W 3";
        IO.SetInputOutput(new InputOutput { Input = input });

        var map = Program.CreateMapFromInput();

        Assert.NotNull(map);
    }
}
