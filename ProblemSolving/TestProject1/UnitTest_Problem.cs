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
        [Fact]
        void input_correctly_mapped_to_cells()
        {
            var input1 = new List<string>
            {
                ".&....&.",
                "..B.&..&",
                "B...&...",
                ".B@.B#..",
                ".&....M.",
                ".B...B..",
                "..B^^&..",
            };

            var map1 = IMap.Create(7, 8, input1);
            
            for (int row = 0; row < 7; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    var cell = map1.GetCell(new Position(row, col));
                    switch (input1[row][col])
                    {
                        case '@':
                        case '.':
                            Assert.IsType<Blank>(cell.Interatable);
                            break;
                        case 'B':
                            Assert.IsType<ItemBox>(cell.Interatable);
                            break;
                        case '&':
                        case 'M':
                            Assert.IsType<Monster>(cell.Interatable);
                            break;
                        case '^':
                            Assert.IsType<Trap>(cell.Interatable);
                            break;
                        case '#':
                            Assert.IsType<Wall>(cell.Interatable);
                            break;
                        default:
                            Assert.True(false);
                            break;
                    }
                }
            }

            var input2 = new List<string>
            {
                ".@#.",
                ".B.B",
                "BB.&",
                "B&.^",
                "&M.&"
            };

            var map2 = IMap.Create(5, 4, input2);

            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    var cell = map2.GetCell(new Position(row, col));
                    switch (input2[row][col])
                    {
                        case '@':
                        case '.':
                            Assert.IsType<Blank>(cell.Interatable);
                            break;
                        case 'B':
                            Assert.IsType<ItemBox>(cell.Interatable);
                            break;
                        case '&':
                        case 'M':
                            Assert.IsType<Monster>(cell.Interatable);
                            break;
                        case '^':
                            Assert.IsType<Trap>(cell.Interatable);
                            break;
                        case '#':
                            Assert.IsType<Wall>(cell.Interatable);
                            break;
                        default:
                            Assert.True(false);
                            break;
                    }
                }
            }
        }

        [Fact]
        void tostring_correctly_change_input_to_output()
        {
            var input = new List<string>
            {
                ".&....&.",
                "..B.&..&",
                "B...&...",
                ".B@.B#..",
                ".&....M.",
                ".B...B..",
                "..B^^&..",
            };

            var map = IMap.Create(7, 8, input);

            var result = map.ToString();

            Assert.Equal(input.Select(x => x.Replace("@", ".")).StringJoin(Environment.NewLine), result);
        }
    }
}
