﻿using UnityEngine;
using System.Collections;
using System;

public class HubaForestSceneReducer : Reducer
{
    public override GameState Reduce(GameState state, SpringAction action, IInteractable source = null)
    {
        switch (action.Type)
        {
            //case ActionType.TURN_ALARM_OFF:
            //    return state.Set(state.AnnanaHouse.SetAlarmTurnedOff(true));
        }

        return state;
    }
}
