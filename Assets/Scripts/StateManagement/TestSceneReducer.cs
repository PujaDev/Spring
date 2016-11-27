using UnityEngine;
using System.Collections;
using System;

public class TestSceneReducer : Reducer
{
    public override GameState Reduce(GameState state, Action action, IInteractable source = null)
    {
        switch (action.Type)
        {
            case ActionType.TURN_ALARM_OFF:
                state.Test.AlarmTurnedOff = true;
                break;
            case ActionType.POSTPONE_ALARM:
                state.Test.AlarmPostponed = true;
                break;
            case ActionType.CHANGE_CLOTHES:
                state.Test.ChangeClothes = true;
                break;
            case ActionType.START_READING_VEGAN_BOOK:
                state.Test.ReadingVeganBook = true;
                break;
            case ActionType.STOP_READING_VEGAN_BOOK:
                state.Test.ReadingVeganBook = false;
                break;
        }

        return state;
    }
}
