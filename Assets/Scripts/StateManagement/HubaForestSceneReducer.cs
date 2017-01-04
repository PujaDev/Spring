using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

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
            case ActionType.START_READING_MAP:
                return state.Set(state.HubaForest.SetIsReadingMap(true));
            case ActionType.STOP_READING_MAP:
                return state.Set(state.HubaForest.SetIsReadingMap(false));
            case ActionType.GO_TO_FOREST:
                return state.Set(state.HubaForest.SetIsInForest(true));
            case ActionType.GO_FOREST_LEFT:
                return GoForestCrossroads(state, ForestSSC.Direction.Left);
            case ActionType.GO_FOREST_RIGHT:
                return GoForestCrossroads(state, ForestSSC.Direction.Right);

        }

        return state;
    }

    private GameState GoForestCrossroads(GameState state, ForestSSC.Direction direction)
    {
        var path = new List<int>(state.HubaForest.CurrentForestWay);
        path.Add((int)direction);

        GameState s = state;
        if (path.Count == state.HubaForest.CorrectForestWay.Count
            && path.SequenceEqual(state.HubaForest.CorrectForestWay))
        {
            s = state.Set(state.HubaForest.SetIsOnSite(true));
        }

        return s.Set(s.HubaForest.SetCurrentForestWay(path));
    }
}
