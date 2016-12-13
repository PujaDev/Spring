using UnityEngine;
using System.Collections;
using System;

public class TestSceneReducer : Reducer
{
    public override GameState Reduce(GameState state, SpringAction action, IInteractable source = null)
    {
        switch (action.Type)
        {
            case ActionType.TURN_ALARM_OFF:
                return state.Set(state.AnnanaHouse.SetAlarmTurnedOff(true));
            case ActionType.POSTPONE_ALARM:
                return state.Set(state.AnnanaHouse.SetAlarmPostponed(true));
            case ActionType.CHANGE_CLOTHES:
                return state.Set(state.AnnanaHouse.SetChangeClothes(true));
            case ActionType.START_READING_VEGAN_BOOK:
                return state.Set(state.AnnanaHouse.SetReadingVeganBook(true));
            case ActionType.STOP_READING_VEGAN_BOOK:
                return state.Set(state.AnnanaHouse.SetReadingVeganBook(true));
            case ActionType.GO_OUTSIDE:
                var chart = GameObject.FindGameObjectWithTag("Scenarios").GetComponent<Fungus.Flowchart>();
                chart.SendFungusMessage("GoOut");
                break;
            case ActionType.GO_INSIDE:
                var chart1 = GameObject.FindGameObjectWithTag("Scenarios").GetComponent<Fungus.Flowchart>();
                chart1.SendFungusMessage("GoIn");
                break;
            case ActionType.FLY_AWAY:
                return state.Set(state.AnnanaHouse.SetFlyAway(true));
        }

        return state;
    }
}
