using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestProject1;

public class CellTest
{
    [Fact]
    public void 아이템박스셀은유저가지나가면빈셀로바뀐다_무기()
    {
        IPlayer player = new Player();
        ICell cell = new Cell(new Position(1, 1), 'B');
        cell.Interactable = new ItemBox("W", "10");

        cell.Interact(player);

        Assert.IsType<Blank>(cell.Interactable);
    }
    [Fact]
    public void 아이템박스셀은유저가지나가면빈셀로바뀐다_방어()
    {
        IPlayer player = new Player();
        ICell cell = new Cell(new Position(1, 1), 'B');
        cell.Interactable = new ItemBox("A", "10");

        cell.Interact(player);

        Assert.IsType<Blank>(cell.Interactable);
    }
    [Fact]
    public void 아이템박스셀은유저가지나가면빈셀로바뀐다_장신구()
    {
        IPlayer player = new Player();
        ICell cell = new Cell(new Position(1, 1), 'B');
        cell.Interactable = new ItemBox("O", "HR");

        cell.Interact(player);

        Assert.IsType<Blank>(cell.Interactable);
    }
    [Fact]
    public void 전투에서유저가이겼다면몬스터는사라진다()
    {
        IPlayer player = new Player();
        ICell cell = new Cell(new Position(1, 1), '&');
        cell.Interactable = new Monster("Name", 1, 1, 1, 1);

        cell.Interact(player);

        Assert.IsType<Blank>(cell.Interactable);
    }
    [Fact]
    public void 전투에서몬스터가이겼다면사라지지않는다()
    {
        IPlayer player = new Player();
        ICell cell = new Cell(new Position(1, 1), '&');
        cell.Interactable = new Monster("Name", 999, 999, 999, 999);

        cell.Interact(player);

        Assert.IsType<Monster>(cell.Interactable);
    }
    [Fact]
    public void 가시는사라지지않는다()
    {
        IPlayer player = new Player();
        ICell cell = new Cell(new Position(1, 1), '^');

        cell.Interact(player);

        Assert.IsType<Trap>(cell.Interactable);
    }
}
