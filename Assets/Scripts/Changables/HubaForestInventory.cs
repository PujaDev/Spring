using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class HubaForestInventory : Inventory
{
    public enum ItemIds
    {
        Coin = 0,
        Vases = 1,
        Spices = 2,
        Map = 3,
        Mask = 4,
        Potion = 5,
        DreamCatcher = 6,
        Braclets = 7
    }

    protected override Inventory GetInstance()
    {
        return this;
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        // This order matters so we do not end up in state where we should not have certain item but we have it
        // First check states that add items
        #region AddItems
        
        // See what's been added from the last time
        HashSet<int> addDiff = null;
        if (oldState == null )
        {
            addDiff = newState.HubaForest.PickedUpItems;
        }
        else if (!newState.HubaForest.PickedUpItems.SetEquals(oldState.HubaForest.PickedUpItems))
        {
            addDiff = new HashSet<int>(newState.HubaForest.PickedUpItems);
            addDiff.ExceptWith(oldState.HubaForest.PickedUpItems);
        }

        // If there are differences
        if (addDiff != null)
        {
            // Add new items
            foreach (var item in addDiff)
            {
                AddItem(item);
            }
        }

        #endregion

        // Then check states that remove items
        #region RemoveItems

        // See what's been used from the last time
        HashSet<int> removeDiff = null;
        if (oldState == null)
        {
            removeDiff = newState.HubaForest.UsedItems;
        }
        else
        {
            removeDiff = new HashSet<int>(newState.HubaForest.UsedItems);
            removeDiff.ExceptWith(oldState.HubaForest.UsedItems);
        }

        // If there are any differences
        if (removeDiff != null)
        {
            // Remove used items
            foreach (var item in removeDiff)
            {
                RemoveItem(item);
            }
        }

        #endregion
    }
}
