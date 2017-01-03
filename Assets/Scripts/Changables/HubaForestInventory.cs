using UnityEngine;
using System.Collections;
using System;

public class HubaForestInventory : Inventory
{
    public enum ItemIds
    {
        Coin = 0
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
        // Add coin
        if (newState.HubaForest.IsCoinPickedUp && (oldState == null || !oldState.HubaForest.IsCoinPickedUp))
        {
            AddItem((int)ItemIds.Coin);
        }
        #endregion

        // Then check states that remove items
        #region RemoveItems
        // Remove coin
        if (newState.HubaForest.IsCoinUsed && (oldState == null || !oldState.HubaForest.IsCoinUsed))
        {
            RemoveItem((int)ItemIds.Coin);
        }
        #endregion
    }
}
