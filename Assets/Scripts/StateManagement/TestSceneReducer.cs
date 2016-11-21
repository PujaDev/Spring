﻿using UnityEngine;
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
        }

        return state;
    }
}