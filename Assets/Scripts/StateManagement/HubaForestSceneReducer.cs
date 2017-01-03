using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class HubaForestSceneReducer : Reducer
{
    public override GameState Reduce(GameState state, SpringAction action, IInteractable source = null)
    {
        switch (action.Type)
        {
            case ActionType.GIVE_MONEY_TO_SHRINE:
                {
                    var used = new HashSet<int>(state.HubaForest.UsedItems);
                    used.Add((int)HubaForestInventory.ItemIds.Coin);

                    GameState s = state.Set(state.HubaForest.SetUsedItems(used));
                    return s.Set(s.HubaForest.SetIsHubaBlessed(true));
                }
            case ActionType.BUY_IN_FOREST:
                {
                    // Pickup whatever was bought
                    int item = (int)action.Data;
                    var pickedUp = new HashSet<int>(state.HubaForest.PickedUpItems);
                    pickedUp.Add(item);

                    // Pay with coin
                    var used = new HashSet<int>(state.HubaForest.UsedItems);
                    used.Add((int)HubaForestInventory.ItemIds.Coin);

                    GameState s = state.Set(state.HubaForest.SetPickedUpItems(pickedUp));
                    return s.Set(s.HubaForest.SetUsedItems(used));
                }
        }

        return state;
    }
}
