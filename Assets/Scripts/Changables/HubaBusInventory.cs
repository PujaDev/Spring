using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class HubaBusInventory : Inventory
{
    public enum ItemIds
    {
        GoldCoins = 0,
        SilverCoin = 1,
        Elixir = 2
    }

    protected override Inventory GetInstance()
    {
        return this;
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        #region AddItems

        // See what's been added from the last time
        HashSet<int> addDiff = null;
        if (oldState == null)
        {
            addDiff = newState.HubaBus.PickedUpItems;
        }
        else if (!newState.HubaBus.PickedUpItems.SetEquals(oldState.HubaBus.PickedUpItems))
        {
            addDiff = new HashSet<int>(newState.HubaBus.PickedUpItems);
            addDiff.ExceptWith(oldState.HubaBus.PickedUpItems);
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
            removeDiff = newState.HubaBus.UsedItems;
        }
        else
        {
            removeDiff = new HashSet<int>(newState.HubaBus.UsedItems);
            removeDiff.ExceptWith(oldState.HubaBus.UsedItems);
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
