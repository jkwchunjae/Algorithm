using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestProject1;

public class InteractionTest_Trap
{
    [Fact]
    public void Trap을밟으면체력이줄어든다()
    {
        IPlayer player = new Player();
        var initHp = player.Hp;

        ITrap trap = new Trap();

        var result = trap.Interact(player);

        Assert.Equal(initHp - 5, player.Hp);
        Assert.False(result.Dead);
    }

    [Fact]
    public void Trap을밟아서죽을수있다()
    {
        IPlayer player = new Player();
        var initHp = player.Hp;

        ITrap trap = new Trap();

        trap.Interact(player);
        trap.Interact(player);
        trap.Interact(player);
        var result = trap.Interact(player);

        Assert.True(result.Dead);
        Assert.Equal("SPIKE TRAP", result.DeadBy);
        Assert.Equal(0, player.Hp);
    }

    [Fact]
    public void Dexterity를가지고있을때는체력이1만줄어든다()
    {
        IPlayer player = new Player();
        new OrnamentDexterity().Interact(player);
        var initHp = player.Hp;

        ITrap trap = new Trap();

        var result = trap.Interact(player);

        Assert.Equal(initHp - 1, player.Hp);
        Assert.False(result.Dead);
    }

    [Fact]
    public void Dexterity를가지고있을때는체력이1만줄어들고죽는다()
    {
        IPlayer player = new Player();
        new OrnamentDexterity().Interact(player);
        var initHp = player.Hp;

        ITrap trap = new Trap();

        (initHp - 1).For(_ => trap.Interact(player));
        var result = trap.Interact(player);

        Assert.True(result.Dead);
        Assert.Equal("SPIKE TRAP", result.DeadBy);
        Assert.Equal(0, player.Hp);
    }
}
