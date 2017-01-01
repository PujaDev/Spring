using System;
using System.Collections.Generic;
using System.Linq;

public class AnnanaInventory : Inventory
{
    protected override Inventory GetInstance()
    {
        return this;
    }

    // This has to accord to indices in the Unity inspector for inventory
    public enum ItemIds
    {
        Berry = 0,
        Flower = 1,
        Leaf = 2,
        CrystalBall = 5,
        EmptyVial = 6,
        Antidote = 7,
        Shrink = 8,
        Invis = 9,
        Soup = 10
    }

    #region Elixirs
    public enum ElixirTypes
    {
        Antidote = ItemIds.Antidote,
        Shrink = ItemIds.Shrink,
        Invis = ItemIds.Invis,
        Soup = ItemIds.Soup
    }
    public class Elixir
    {
        public string Name { get; private set; }
        public HashSet<int> Ingredients { get; private set; }
        public Elixir(string name, HashSet<int> ingredients)
        {
            Name = name;
            Ingredients = ingredients;
        }
    }

    // <int, Elixir> instead of <ElixirTypes, Elixir> because of some problems with lookup
    public Dictionary<int, Elixir> Elixirs { get; private set; }

    public AnnanaInventory()
    {
        Elixirs = new Dictionary<int, Elixir>();
        Elixirs.Add((int)ElixirTypes.Antidote, new Elixir("Antidote", new HashSet<int>()
        {
            (int)ItemIds.Berry,
            (int)ItemIds.Flower
        }));
        Elixirs.Add((int)ElixirTypes.Shrink, new Elixir("Shrinking elixir", new HashSet<int>()
        {
            (int)ItemIds.Flower
        }));
        Elixirs.Add((int)ElixirTypes.Invis, new Elixir("Elixir of invisibility", new HashSet<int>()
        {
            (int)ItemIds.Berry,
            (int)ItemIds.Flower,
            (int)ItemIds.Leaf
        }));
        Elixirs.Add((int)ElixirTypes.Soup, new Elixir("Soup", new HashSet<int>()
        {
            (int)ItemIds.Berry,
            (int)ItemIds.Leaf
        }));
    }
    #endregion

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        // This order matters so we do not end up in state where we should not have certain item but we have it
        // First check states that add items
        #region AddItems
        // Add crystal ball
        if (newState.AnnanaHouse.IsCrystalBallPickedUp && (oldState == null || !oldState.AnnanaHouse.IsCrystalBallPickedUp))
        {
            AddItem((int)ItemIds.CrystalBall);
        }

        // Add vial
        if (newState.AnnanaHouse.IsEmptyVialPickedUp && (oldState == null || !oldState.AnnanaHouse.IsEmptyVialPickedUp))
        {
            AddItem((int)ItemIds.EmptyVial);
        }

        // Add flower
        if (newState.AnnanaHouse.IsFlowerPickedUp && (oldState == null || !oldState.AnnanaHouse.IsFlowerPickedUp))
        {
            AddItem((int)ItemIds.Flower);
        }

        // Add berry
        if (newState.AnnanaHouse.IsBerryPickedUp && (oldState == null || !oldState.AnnanaHouse.IsBerryPickedUp))
        {
            AddItem((int)ItemIds.Berry);
        }

        // Add leaf
        if (newState.AnnanaHouse.IsLeafPickedUp && (oldState == null || !oldState.AnnanaHouse.IsLeafPickedUp))
        {
            AddItem((int)ItemIds.Leaf);
        }

        // Add elixir
        if (newState.AnnanaHouse.ElixirName != "" && (oldState == null || oldState.AnnanaHouse.ElixirName == ""))
        {
            int type = Elixirs
                .Where(x => x.Value.Name == newState.AnnanaHouse.ElixirName)
                .First()
                .Key;

            AddItem(type);
        }
        #endregion


        // Then check states that remove items
        #region RemoveItems
        // Remove vial
        if (newState.AnnanaHouse.IsEmptyVialUsed && (oldState == null || !oldState.AnnanaHouse.IsEmptyVialUsed))
        {
            RemoveItem((int)ItemIds.EmptyVial);
        }

        // Remove flower
        if (newState.AnnanaHouse.IsFlowerUsed && (oldState == null || !oldState.AnnanaHouse.IsFlowerUsed))
        {
            RemoveItem((int)ItemIds.Flower);
        }

        // Remove berry
        if (newState.AnnanaHouse.IsBerryUsed && (oldState == null || !oldState.AnnanaHouse.IsBerryUsed))
        {
            RemoveItem((int)ItemIds.Berry);
        }

        // Remove leaf
        if (newState.AnnanaHouse.IsLeafUsed && (oldState == null || !oldState.AnnanaHouse.IsLeafUsed))
        {
            RemoveItem((int)ItemIds.Leaf);
        }

        // Remove elixir
        if (newState.AnnanaHouse.IsElixirUsed && (oldState == null || !oldState.AnnanaHouse.IsElixirUsed))
        {
            int type = Elixirs
                .Where(x => x.Value.Name == newState.AnnanaHouse.ElixirName)
                .First()
                .Key;

            RemoveItem(type);
        }
        
        #endregion
    }
}
