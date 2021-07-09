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
                    board.Cells[row][column].Block = reversed[row][column] == ' ' ? 0 : (reversed[row][column] - '0');
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
                " 111 ",
                "     ",
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
                " 111 ",
                "     ",
                "   11",
                "     ",
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
                "111  ",
                "11   ",
                "     ",
                "   11",
                "     ",
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
                "111  ",
                "11   ",
                "     ",
                "     ",
                "   11",
                "     ",
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
        public void Test_Active조각을_바닥으로내린다1()
        {
            var input = new string[]
            {
                "    ",
                "    ",
                "11  ",
                "    ",
            };
            var expected = new string[]
            {
                "    ",
                "    ",
                "    ",
                "11  ",
            };

            var board = MakeBoard(input);
            board.DropLevitationBlock();

            var output = board.ToStr();

            Assert.Equal(expected, output);
        }

        [Fact]
        public void Test_Active조각을_바닥으로내린다2()
        {
            var input = new string[]
            {
                " 23 ",
                " 23 ",
                "11  ",
                "    ",
            };
            var expected = new string[]
            {
                "    ",
                " 2  ",
                " 23 ",
                "113 ",
            };

            var board = MakeBoard(input);
            board.DropLevitationBlock();

            var output = board.ToStr();

            Assert.Equal(expected, output);
        }

        //[Fact]
        //public void Test_Active조각을_바닥으로내린다3()
        //{
        //    var input = new string[]
        //    {
        //        "  2 ",
        //        "  2 ",
        //        " 11 ",
        //        "11  ",
        //        " 11 ",
        //        "11  ",
        //    };
        //    var expected = new string[]
        //    {
        //        "  1 ",
        //        "  1 ",
        //        " 11 ",
        //        "11  ",
        //        " 11 ",
        //        "11  ",
        //    };

        //    var board = MakeBoard(input);
        //    board.DropActivePiece();

        //    var output = board.ToStr();

        //    Assert.Equal(expected, output);
        //}

        [Fact]
        public void Test_Simulation1()
        {
            var board = new TetrisBoard(4, 6);
            var score = 0;
            var blockNumber = 1;

            var expected = new string[]
            {
                "    ",
                "    ",
                "    ",
                "    ",
                "    ",
                " 1  ",
            };
            score += board.SetPiece(CellType.A, 1, blockNumber++);
            Assert.Equal(expected, board.ToStr());
            Assert.Equal(0, score);

            expected = new string[]
            {
                "    ",
                "    ",
                "    ",
                "    ",
                "22  ",
                " 1  ",
            };
            score += board.SetPiece(CellType.B, 0, blockNumber++);
            Assert.Equal(expected, board.ToStr());
            Assert.Equal(0, score);

            expected = new string[]
            {
                "    ",
                "    ",
                "    ",
                "    ",
                "223 ",
                " 13 ",
            };
            score += board.SetPiece(CellType.C, 2, blockNumber++);
            Assert.Equal(expected, board.ToStr());
            Assert.Equal(0, score);

            expected = new string[]
            {
                "    ",
                "    ",
                "    ",
                "    ",
                "    ",
                " 134",
            };
            score += board.SetPiece(CellType.C, 3, blockNumber++);
            Assert.Equal(expected, board.ToStr());
            Assert.Equal(1, score);

            expected = new string[]
            {
                "    ",
                "    ",
                "    ",
                "   5",
                "   5",
                " 134",
            };
            score += board.SetPiece(CellType.C, 3, blockNumber++);
            Assert.Equal(expected, board.ToStr());
            Assert.Equal(1, score);

            expected = new string[]
            {
                "    ",
                "    ",
                "    ",
                "   5",
                "66 5",
                " 134",
            };
            score += board.SetPiece(CellType.B, 0, blockNumber++);
            Assert.Equal(expected, board.ToStr());
            Assert.Equal(1, score);

            expected = new string[]
            {
                "    ",
                "    ",
                "7   ",
                "7  5",
                "66 5",
                " 134",
            };
            score += board.SetPiece(CellType.C, 0, blockNumber++);
            Assert.Equal(expected, board.ToStr());
            Assert.Equal(1, score);

            expected = new string[]
            {
                "    ",
                "    ",
                "    ",
                "    ",
                "    ",
                "7 85",
            };
            score += board.SetPiece(CellType.C, 2, blockNumber++);
            Assert.Equal(expected, board.ToStr());
            Assert.Equal(3, score);
        }

        [Fact]
        public void Test_Simulation2()
        {
            var board = new TetrisBoard(4, 6);
            var score = 0;
            var blockNumber = 1;

            var expected = new string[]
            {
                "    ",
                "    ",
                "    ",
                "    ",
                "    ",
                "  1 ",
            };
            score += board.SetPiece(CellType.A, 2, blockNumber++);
            Assert.Equal(expected, board.ToStr());
            Assert.Equal(0, score);

            expected = new string[]
            {
                "    ",
                "    ",
                "    ",
                "    ",
                "2   ",
                "2 1 ",
            };
            score += board.SetPiece(CellType.C, 0, blockNumber++);
            Assert.Equal(expected, board.ToStr());
            Assert.Equal(0, score);

            expected = new string[]
            {
                "    ",
                "    ",
                "    ",
                "33  ",
                "2   ",
                "2 1 ",
            };
            score += board.SetPiece(CellType.B, 0, blockNumber++);
            Assert.Equal(expected, board.ToStr());
            Assert.Equal(0, score);

            expected = new string[]
            {
                "    ",
                "    ",
                "44  ",
                "33  ",
                "2   ",
                "2 1 ",
            };
            score += board.SetPiece(CellType.B, 0, blockNumber++);
            Assert.Equal(expected, board.ToStr());
            Assert.Equal(0, score);

            expected = new string[]
            {
                "    ",
                "    ",
                " 55 ",
                "44  ",
                "33  ",
                "2   ",
            };
            score += board.SetPiece(CellType.B, 1, blockNumber++);
            Assert.Equal(expected, board.ToStr());
            Assert.Equal(0, score);

            expected = new string[]
            {
                "    ",
                "    ",
                " 55 ",
                "44  ",
                "33 6",
                "2  6",
            };
            score += board.SetPiece(CellType.C, 3, blockNumber++);
            Assert.Equal(expected, board.ToStr());
            Assert.Equal(0, score);

            expected = new string[]
            {
                "    ",
                "    ",
                "77  ",
                " 55 ",
                "44  ",
                "33 6",
            };
            score += board.SetPiece(CellType.B, 0, blockNumber++);
            Assert.Equal(expected, board.ToStr());
            Assert.Equal(0, score);

            expected = new string[]
            {
                "    ",
                "    ",
                " 88 ",
                "77  ",
                " 55 ",
                "44  ",
            };
            score += board.SetPiece(CellType.B, 1, blockNumber++);
            Assert.Equal(expected, board.ToStr());
            Assert.Equal(0, score);

            expected = new string[]
            {
                "    ",
                "    ",
                "  9 ",
                "  9 ",
                " 88 ",
                "77  ",
            };
            score += board.SetPiece(CellType.C, 2, blockNumber++);
            Assert.Equal(expected, board.ToStr());
            Assert.Equal(0, score);
        }
    }
}
