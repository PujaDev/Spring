using UnityEngine;
using System.Collections;
using System;

public class SilentForestTips : TipManager
{
    protected override string GetTipText(GameState gameState)
    {
        HubaForestSceneState state = gameState.HubaForest;
        // Killed turtle
        if (gameState.AnnanaTeaParty.ThrewCup)
            return "A mug killed the bus. Redo previous scene.";

        // Nothing to do here
        if (!gameState.HubaBus.getOnTheBus)
            return "Huba must to get here by bus. Finish previous scene.";

        if (state.IsOnSite)
            return "Although it may not seem like it, you have finished the game :)";

        // In forest with a coin
        if (!state.UsedItems.Contains((int)HubaForestInventory.ItemIds.Coin)
            && state.IsInForest)
            return "Try your luck or restart scene. You will need help finding way.";

        // Still have a coin
        if (!state.UsedItems.Contains((int)HubaForestInventory.ItemIds.Coin))
            return "Buy a map or donate the coin to find your way through the forest.";

        // No coin but not in forest yet
        if (state.UsedItems.Contains((int)HubaForestInventory.ItemIds.Coin)
            && !state.IsInForest)
            return "Go to the forest";

        // In forest with map
        if (state.PickedUpItems.Contains((int)HubaForestInventory.ItemIds.Map)
            && state.IsInForest)
            return "To see your way through the forest use map at Huba.";

        // Blessed in forest
        if (state.IsHubaBlessed 
            && state.IsInForest)
            return "Watch and follow the spirit.";

        return ALL_DONE;
    }
}
