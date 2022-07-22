using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestProject1
{
    public class UnitTest_Map
    {
        [Fact]
        void input_correctly_mapped_to_cells_1()
        {
            var map = IMap.Create(_height1, _width1, _input1);
            
            for (int row = 0; row < _height1; row++)
            {
                for (int col = 0; col < _width1; col++)
                {
                    var cell = map.GetCell(new Position(row, col));
                    switch (_input1[row][col])
                    {
                        case '@':
                        case '.':
                            Assert.IsType<Blank>(cell.Interactable);
                            break;
                        case 'B':
                            Assert.IsType<ItemBox>(cell.Interactable);
                            break;
                        case '&':
                        case 'M':
                            Assert.IsType<Monster>(cell.Interactable);
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

            Assert.Equal(_height1, map.Size.Height);
            Assert.Equal(_width1, map.Size.Width);
        }

        [Fact]
        void input_correctly_mapped_to_cells_2()
        {
            var map = IMap.Create(_height2, _width2, _input2);

            for (int row = 0; row < _height2; row++)
            {
                for (int col = 0; col < _width2; col++)
                {
                    var cell = map.GetCell(new Position(row, col));
                    switch (_input2[row][col])
                    {
                        case '@':
                        case '.':
                            Assert.IsType<Blank>(cell.Interactable);
                            break;
                        case 'B':
                            Assert.IsType<ItemBox>(cell.Interactable);
                            break;
                        case '&':
                        case 'M':
                            Assert.IsType<Monster>(cell.Interactable);
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

            Assert.Equal(_height2, map.Size.Height);
            Assert.Equal(_width2, map.Size.Width);
        }

        [Fact]
        void tostring_correctly_change_input_to_output()
        {
            var map = IMap.Create(_height1, _width1, _input1);
            var player = new Player();
            player.Position = _input1.Select((line, index) =>
            {
                if (line.Contains('@'))
                {
                    return new Position(index, line.IndexOf('@'));
                }
                else return new Position(-1, -1);
            })
            .Where(x => x.Row >= 0)
            .FirstOrDefault();

            var result = map.ToString(player);

            Assert.Equal(_input1.StringJoin(Environment.NewLine), result);
        }

        [Theory]
        [InlineData(-1, 0, false)]
        [InlineData(0, -1, false)]
        [InlineData(-1, -1, false)]
        [InlineData(3 /*_height3*/, 0, false)]
        [InlineData(0, 3 /*_width3*/, false)]
        [InlineData(3 /*_height3*/, 3 /*_width3*/, false)]
        void movable_returns_correctly(int positionRow, int positionColumn, bool result)
        {
            var map = IMap.Create(_height3, _width3, _input3);

            for (int row = 0; row < _height3; row++)
            {
                for (int col = 0; col < _width3; col++)
                {
                    var isMovable = map.Movable(new Position(row, col));
                    if (_input3[row][col] == '#')
                    {
                        Assert.False(isMovable);
                    }
                    else
                    {
                        Assert.True(isMovable);
                    }
                }
            }

            if (result)
            {
                Assert.True(map.Movable(new Position(positionRow, positionColumn)));
            }
            else
            {
                Assert.False(map.Movable(new Position(positionRow, positionColumn)));
            }
        }

        #region Input
        private readonly List<string> _input1 = new List<string>
        {
            ".&....&.",
            "..B.&..&",
            "B...&...",
            ".B@.B#..",
            ".&....M.",
            ".B...B..",
            "..B^^&..",
        };
        private readonly int _height1 = 7;
        private readonly int _width1 = 8;

        private readonly List<string> _input2 = new List<string>
        {
            ".@#.",
            ".B.B",
            "BB.&",
            "B&.^",
            "&M.&",
        };
        private readonly int _height2 = 5;
        private readonly int _width2 = 4;

        private readonly List<string> _input3 = new List<string>
        {
            ".#@",
            "M&^",
            "B.."
        };
        private readonly int _height3 = 3;
        private readonly int _width3 = 3;

        #endregion Input
    }
}
