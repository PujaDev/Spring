using UnityEngine;
using System.Collections;
using System;

public class Seller : IInteractable
{
    private SpringAction[] Actions;
    protected override SpringAction[] GetActionList()
    {
        return Actions;
    }

    protected override void Awake()
    {
        base.Awake();

        Actions = new SpringAction[]
        {
            new SpringAction(ActionType.BUY_IN_FOREST, "Buy a Potion", icons[1], (int)HubaForestInventory.ItemIds.Potion),
            new SpringAction(ActionType.BUY_IN_FOREST, "Buy Vases", icons[2], (int)HubaForestInventory.ItemIds.Vases),
            new SpringAction(ActionType.BUY_IN_FOREST, "Buy Spices", icons[3], (int)HubaForestInventory.ItemIds.Spices),
            new SpringAction(ActionType.BUY_IN_FOREST, "Buy a Map", icons[4], (int)HubaForestInventory.ItemIds.Map),
            new SpringAction(ActionType.BUY_IN_FOREST, "Buy a Mask", icons[5], (int)HubaForestInventory.ItemIds.Mask),
            //new SpringAction(ActionType.BUY_IN_FOREST, "Buy a Dream Catcher", icons[6], (int)HubaForestInventory.ItemIds.DreamCatcher), // AW cannot display more than 6 items
            new SpringAction(ActionType.BUY_IN_FOREST, "Buy Braclets", icons[7], (int)HubaForestInventory.ItemIds.Braclets)
        };
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        // Cannot buy anything without money
        int coin = (int)HubaForestInventory.ItemIds.Coin;
        if (newState.HubaForest.UsedItems.Contains(coin)
            && (oldState == null || !oldState.HubaForest.UsedItems.Contains(coin)))
        {
            Actions = new SpringAction[]
            {
                new SpringAction(ActionType.LOOK, "Cannot buy anything with no money", icons[0])
            };
        }
    }
}
