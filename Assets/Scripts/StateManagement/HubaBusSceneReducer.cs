using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class HubaBusSceneReducer : Reducer
{
    public override GameState Reduce(GameState state, SpringAction action, IInteractable source = null)
    {
        switch (action.Type)
        {
            case ActionType.GET_ELIXIR:
                {
                    // Pickup whatever was delivered
                    int item = (int)action.Data;
                    var pickedUp = new HashSet<int>(state.HubaBus.PickedUpItems);
                    pickedUp.Add(item);
                    Destroy(source.gameObject);
                    return state.Set(state.HubaBus.SetPickedUpItems(pickedUp));
                }
            case ActionType.DELIVERY:
                {
                    return state.Set(state.HubaBus.SetisDelivered(true));
                }
            case ActionType.ARRIVAL:
                {
                    return state.Set(state.HubaBus.SetisBusWaiting(true));
                }
            case ActionType.DEPARTURE:
                {
                    return state.Set(state.HubaBus.SethasBusLeft(true));
                }
        }

        return state;
    }
}
