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
        IPlayer player = new Player() { MaxHP = 20, CurrentHP = 20 };
        var initHp = player.CurrentHP;

        ITrap trap = new Trap();

        var result = trap.Interact(player);

        Assert.Equal(initHp - 5, player.CurrentHP);
        Assert.False(result.Dead);
    }

    [Fact]
    public void Trap을밟아서죽을수있다()
    {
        IPlayer player = new Player() { MaxHP = 20, CurrentHP = 20 };
        var initHp = player.CurrentHP;

        ITrap trap = new Trap();

        trap.Interact(player);
        trap.Interact(player);
        trap.Interact(player);
        var result = trap.Interact(player);

        Assert.True(result.Dead);
        Assert.Equal("SPIKE TRAP", result.DeadBy);
        Assert.Equal(0, player.CurrentHP);
    }

    [Fact]
    public void Dexterity를가지고있을때는체력이1만줄어든다()
    {
        IPlayer player = new Player() { MaxHP = 5, CurrentHP = 5 };
        player.Ornaments = new IOrnament[] { new OrnamentDexterity() };
        var initHp = player.CurrentHP;

        ITrap trap = new Trap();

        var result = trap.Interact(player);

        Assert.Equal(initHp - 1, player.CurrentHP);
        Assert.False(result.Dead);
    }

    [Fact]
    public void Dexterity를가지고있을때는체력이1만줄어들고죽는다()
    {
        IPlayer player = new Player() { MaxHP = 5, CurrentHP = 5 };
        player.Ornaments = new IOrnament[] { new OrnamentDexterity() };
        var initHp = player.CurrentHP;

        ITrap trap = new Trap();

        trap.Interact(player);
        trap.Interact(player);
        trap.Interact(player);
        trap.Interact(player);
        var result = trap.Interact(player);

        Assert.True(result.Dead);
        Assert.Equal("SPIKE TRAP", result.DeadBy);
        Assert.Equal(0, player.CurrentHP);
    }
}
