using UnityEngine;
using System.Collections;
using System;

public class HubaForestSceneReducer : Reducer
{
    public override GameState Reduce(GameState state, SpringAction action, IInteractable source = null)
    {
        switch (action.Type)
        {
            case ActionType.GIVE_MONEY_TO_SHRINE:
                {
                    GameState s = state.Set(state.HubaForest.SetIsCoinUsed(true));
                    return s.Set(s.HubaForest.SetIsHubaBlessed(true));
                }
        }

        return state;
    }
}
