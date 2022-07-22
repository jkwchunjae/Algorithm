using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestProject1;

public class InteractionTest_ItemBox
{
    [Fact]
    public void Weapon상자를열면착용해야한다()
    {
        IPlayer player = new Player();
        IItemBox itemBox = new ItemBox("W", "10");

        itemBox.Interact(player);

        Assert.Equal(10, player.Weapon.AttackValue);
    }

    [Fact]
    public void Weapon상자를열면착용하고있던무기를버리고새무기를장창해야한다()
    {
        IPlayer player = new Player();
        IItemBox itemBox1 = new ItemBox("W", "10");
        IItemBox itemBox2 = new ItemBox("W", "1");

        itemBox1.Interact(player);
        itemBox2.Interact(player);

        Assert.Equal(1, player.Weapon.AttackValue);
    }

    [Fact]
    public void Armor상자를열면착용해야한다()
    {
        IPlayer player = new Player();
        IItemBox itemBox = new ItemBox("A", "10");

        itemBox.Interact(player);

        Assert.Equal(10, player.Armor.DefenseValue);
    }

    [Fact]
    public void Armor상자를열면착용하고있던갑옷를버리고새갑옷을장창해야한다()
    {
        IPlayer player = new Player();
        IItemBox itemBox1 = new ItemBox("A", "10");
        IItemBox itemBox2 = new ItemBox("A", "1");

        itemBox1.Interact(player);
        itemBox2.Interact(player);

        Assert.Equal(1, player.Armor.DefenseValue);
    }

    [Fact]
    public void 장신구를착용할수있다()
    {
        IPlayer player = new Player();
        IItemBox itemBox = new ItemBox("O", "CO");

        itemBox.Interact(player);

        Assert.Contains(player.Ornaments, x => x?.GetType() == typeof(OrnamentCourage));
    }


    [Fact]
    public void 중복된장신구창용은불가능하다()
    {
        IPlayer player = new Player();
        IItemBox itemBox1 = new ItemBox("O", "CO");
        IItemBox itemBox2 = new ItemBox("O", "CO");

        itemBox1.Interact(player);
        itemBox2.Interact(player);

        Assert.Equal(1, player.Ornaments.Count(o => o is OrnamentCourage));
    }

    [Fact]
    public void 장신구는최대4개까지창용할수있다()
    {
        IPlayer player = new Player();
        IItemBox itemBox1 = new ItemBox("O", "CO");
        IItemBox itemBox2 = new ItemBox("O", "HR");
        IItemBox itemBox3 = new ItemBox("O", "RE");
        IItemBox itemBox4 = new ItemBox("O", "EX");
        IItemBox itemBox5 = new ItemBox("O", "HU");

        itemBox1.Interact(player);
        itemBox2.Interact(player);
        itemBox3.Interact(player);
        itemBox4.Interact(player);
        itemBox5.Interact(player);

        Assert.Equal(4, player.Ornaments.Count());
        Assert.Contains(player.Ornaments, x => x is OrnamentCourage);
        Assert.Contains(player.Ornaments, x => x is OrnamentHpRegeneration);
        Assert.Contains(player.Ornaments, x => x is OrnamentReincarnation);
        Assert.Contains(player.Ornaments, x => x is OrnamentExperience);
    }
}
