using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Boiler : IInteractable, IItemUsable
{
    HashSet<int> UsableItems;

    void Awake()
    {
        UsableItems = new HashSet<int>()
        {
            (int)AnnanaInventory.ItemIds.EmptyVial,
            (int)AnnanaInventory.ItemIds.Flower,
            (int)AnnanaInventory.ItemIds.Berry,
            (int)AnnanaInventory.ItemIds.Leaf
        };
    }

    protected override SpringAction[] GetActionList()
    {
        return new SpringAction[]
        {
            new SpringAction(ActionType.EMPTY_BOILER, "Empty boiler", icons[0])
        };
    }

    public bool CanUseOnSelf(int itemId)
    {
        // Check if we can use it and disallow duplicates
        if (UsableItems.Contains(itemId) &&
            !StateManager.Instance.State.AnnanaHouse.BoilerContents.Contains(itemId))
            return true;
        return false;
    }

    public void UseOnSelf(int itemId)
    {
        if (CanUseOnSelf(itemId))
        {
            if (itemId == (int)AnnanaInventory.ItemIds.EmptyVial) // Fill current elixir
            {
                ComeCloser(new SpringAction(ActionType.FILL_ELIXIR, "", null));
            }
            else // Add ingredient
            {
                ComeCloser(new SpringAction(ActionType.THROW_TO_BOILER, "", null, itemId));
            }
        }
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        if ((oldState == null && newState.AnnanaHouse.BoilerContents.Count != 0)
            || (oldState != null && oldState.AnnanaHouse.BoilerContents != newState.AnnanaHouse.BoilerContents))
        {
            // Change water color based on current elixir
        }
    }


}
