﻿public class AnnanaInventory : Inventory
{
    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        // This order matters so we do not end up in state where we should not have certain item but we have it
        // First check states that add items
        if (newState.AnnanaHouse.CrystalBallPickedUp && (oldState == null || !oldState.AnnanaHouse.CrystalBallPickedUp))
        {
            AddItem(5);
        }

        // Then check states that remove items

    }
}
