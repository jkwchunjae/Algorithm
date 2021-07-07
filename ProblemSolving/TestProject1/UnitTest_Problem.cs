using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestProject1
{
    public class UnitTest_Problem
    {
        private TetrisBoard MakeBoard(string[] info)
        {
            var reversed = info.Reverse().ToArray();
            //  : empty, 1: fill, 2: active
            var board = new TetrisBoard(info[0].Length, info.Length);

            board.Rows.For(row =>
            {
                board.Columns.For(column =>
                {
                    board.Cells[row][column].Status = reversed[row][column] == '2' ? CellStatus.Active : reversed[row][column] == '1' ? CellStatus.Fill : CellStatus.Empty;
                });
            });

            return board;
        }

        [Fact]
        public void Test_한줄_잘지우는지()
        {
            var input = new string[]
            {
                "     ",
                " 111 ",
                "11111",
                "   11",
                " 111 ",
            };

            var expected = new string[]
            {
                "     ",
                "     ",
                " 111 ",
                "   11",
                " 111 ",
            };

            var board = MakeBoard(input);
            board.RemoveFullFillLine();

            var output = board.ToStr();

            Assert.Equal(expected, output);
        }

        [Fact]
        public void Test_각각한줄_잘지우는지()
        {
            var input = new string[]
            {
                "     ",
                " 111 ",
                "11111",
                "   11",
                "11111",
            };

            var expected = new string[]
            {
                "     ",
                "     ",
                "     ",
                " 111 ",
                "   11",
            };

            var board = MakeBoard(input);
            board.RemoveFullFillLine();

            var output = board.ToStr();

            Assert.Equal(expected, output);
        }

        [Fact]
        public void Test_지울때_떨어진상태는_일단_그대로둔다()
        {
            var input = new string[]
            {
                "111  ",
                "11   ",
                "11111",
                "   11",
                "11111",
            };

            var expected = new string[]
            {
                "     ",
                "     ",
                "111  ",
                "11   ",
                "   11",
            };

            var board = MakeBoard(input);
            board.RemoveFullFillLine();

            var output = board.ToStr();

            Assert.Equal(expected, output);
        }

        [Fact]
        public void Test_두줄도잘지운다()
        {
            var input = new string[]
            {
                "111  ",
                "11   ",
                "11111",
                "11111",
                "   11",
                "11111",
            };

            var expected = new string[]
            {
                "     ",
                "     ",
                "     ",
                "111  ",
                "11   ",
                "   11",
            };

            var board = MakeBoard(input);
            board.RemoveFullFillLine();

            var output = board.ToStr();

            Assert.Equal(expected, output);
        }

        [Fact]
        public void Test_인접한셀찾기1()
        {
            var input = new string[]
            {
                "     ",
                "111  ",
                "11   ",
                "   11",
            };

            var board = MakeBoard(input);
            var cell = board.Cells.First().First(x => x.IsFill);
            var cells = board.FindAdjacencyCells(cell);

            Assert.Equal(2, cells.Count);
        }

        [Fact]
        public void Test_공중에_떠있는조각_찾는다1()
        {
            var input = new string[]
            {
                "      11",
                "111     ",
                "11      ",
                "   11 11",
            };
            var expected = new string[]
            {
                "      22",
                "222     ",
                "22      ",
                "   11 11",
            };

            var board = MakeBoard(input);
            board.ChangeLevitationCellState();

            var output = board.ToStr();

            Assert.Equal(expected, output);
        }

        [Fact]
        public void Test_공중에_떠있는조각_찾는다2()
        {
            var input = new string[]
            {
                "     11",
                "111  11",
                "11   11",
                "   11  ",
            };
            var expected = new string[]
            {
                "     22",
                "222  22",
                "22   22",
                "   11  ",
            };

            var board = MakeBoard(input);
            board.ChangeLevitationCellState();

            var output = board.ToStr();

            Assert.Equal(expected, output);
        }

        [Fact]
        public void Test_공중에_떠있는조각_찾는다3()
        {
            var input = new string[]
            {
                "     11",
                "111  11",
                "11  111",
                "   11  ",
            };
            var expected = new string[]
            {
                "     11",
                "222  11",
                "22  111",
                "   11  ",
            };

            var board = MakeBoard(input);
            board.ChangeLevitationCellState();

            var output = board.ToStr();

            Assert.Equal(expected, output);
        }

        [Fact]
        public void Test_Active조각을_바닥으로내린다1()
        {
            var input = new string[]
            {
                "     11",
                "222  11",
                "22  111",
                "   11  ",
            };
            var expected = new string[]
            {
                "     11",
                "     11",
                "111 111",
                "11 11  ",
            };

            var board = MakeBoard(input);
            board.DropActivePiece();

            var output = board.ToStr();

            Assert.Equal(expected, output);
        }

        [Fact]
        public void Test_Active조각을_바닥으로내린다2()
        {
            var input = new string[]
            {
                "      22",
                "222     ",
                "22      ",
                "   11 11",
            };
            var expected = new string[]
            {
                "        ",
                "        ",
                "111   11",
                "11 11 11",
            };

            var board = MakeBoard(input);
            board.DropActivePiece();

            var output = board.ToStr();

            Assert.Equal(expected, output);
        }

        [Fact]
        public void Test_Simulation1()
        {
            var board = new TetrisBoard(4, 6);
            var score = 0;

            var expected = new string[]
            {
                "    ",
                "    ",
                "    ",
                "    ",
                "    ",
                " 1  ",
            };
            score += board.SetPiece(CellType.A, 1);
            Assert.Equal(expected, board.ToStr());
            Assert.Equal(0, score);

            expected = new string[]
            {
                "    ",
                "    ",
                "    ",
                "    ",
                "11  ",
                " 1  ",
            };
            score += board.SetPiece(CellType.B, 0);
            Assert.Equal(expected, board.ToStr());
            Assert.Equal(0, score);

            expected = new string[]
            {
                "    ",
                "    ",
                "    ",
                "    ",
                "111 ",
                " 11 ",
            };
            score += board.SetPiece(CellType.C, 2);
            Assert.Equal(expected, board.ToStr());
            Assert.Equal(0, score);

            expected = new string[]
            {
                "    ",
                "    ",
                "    ",
                "    ",
                "    ",
                " 111",
            };
            score += board.SetPiece(CellType.C, 3);
            Assert.Equal(expected, board.ToStr());
            Assert.Equal(1, score);

            expected = new string[]
            {
                "    ",
                "    ",
                "    ",
                "   1",
                "   1",
                " 111",
            };
            score += board.SetPiece(CellType.C, 3);
            Assert.Equal(expected, board.ToStr());
            Assert.Equal(1, score);

            expected = new string[]
            {
                "    ",
                "    ",
                "    ",
                "   1",
                "11 1",
                " 111",
            };
            score += board.SetPiece(CellType.B, 0);
            Assert.Equal(expected, board.ToStr());
            Assert.Equal(1, score);

            expected = new string[]
            {
                "    ",
                "    ",
                "1   ",
                "1  1",
                "11 1",
                " 111",
            };
            score += board.SetPiece(CellType.C, 0);
            Assert.Equal(expected, board.ToStr());
            Assert.Equal(1, score);

            expected = new string[]
            {
                "    ",
                "    ",
                "    ",
                "    ",
                "    ",
                "1 11",
            };
            score += board.SetPiece(CellType.C, 2);
            Assert.Equal(expected, board.ToStr());
            Assert.Equal(3, score);
        }
        [Fact]
        public void Test_Simulation2()
        {
            var board = new TetrisBoard(4, 6);
            var score = 0;

            var expected = new string[]
            {
                "    ",
                "    ",
                "    ",
                "    ",
                "    ",
                "  1 ",
            };
            score += board.SetPiece(CellType.A, 2);
            Assert.Equal(expected, board.ToStr());
            Assert.Equal(0, score);

            expected = new string[]
            {
                "    ",
                "    ",
                "    ",
                "    ",
                "1   ",
                "1 1 ",
            };
            score += board.SetPiece(CellType.C, 0);
            Assert.Equal(expected, board.ToStr());
            Assert.Equal(0, score);

            expected = new string[]
            {
                "    ",
                "    ",
                "    ",
                "11  ",
                "1   ",
                "1 1 ",
            };
            score += board.SetPiece(CellType.B, 0);
            Assert.Equal(expected, board.ToStr());
            Assert.Equal(0, score);

            expected = new string[]
            {
                "    ",
                "    ",
                "11  ",
                "11  ",
                "1   ",
                "1 1 ",
            };
            score += board.SetPiece(CellType.B, 0);
            Assert.Equal(expected, board.ToStr());
            Assert.Equal(0, score);

            expected = new string[]
            {
                "    ",
                "    ",
                " 11 ",
                "11  ",
                "11  ",
                "1   ",
            };
            score += board.SetPiece(CellType.B, 1);
            Assert.Equal(expected, board.ToStr());
            Assert.Equal(0, score);

            expected = new string[]
            {
                "    ",
                "    ",
                " 11 ",
                "11  ",
                "11 1",
                "1  1",
            };
            score += board.SetPiece(CellType.C, 3);
            Assert.Equal(expected, board.ToStr());
            Assert.Equal(0, score);

            expected = new string[]
            {
                "    ",
                "    ",
                "11  ",
                " 11 ",
                "11  ",
                "11 1",
            };
            score += board.SetPiece(CellType.B, 0);
            Assert.Equal(expected, board.ToStr());
            Assert.Equal(0, score);

            expected = new string[]
            {
                "    ",
                "    ",
                " 11 ",
                "11  ",
                " 11 ",
                "11  ",
            };
            score += board.SetPiece(CellType.B, 1);
            Assert.Equal(expected, board.ToStr());
            Assert.Equal(0, score);
        }
    }
}
