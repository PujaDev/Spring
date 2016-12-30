using UnityEngine;
using System.Collections;
using System;

public class AnnanaHouseSceneReducer : Reducer
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
                return state.Set(state.AnnanaHouse.SetReadingVeganBook(false));
            case ActionType.GO_OUTSIDE:
                {
                    GameState s = state.Set(state.AnnanaHouse.SetIsOutside(true));
                    return s.Set(s.AnnanaHouse.SetIsInside(false));
                }
            case ActionType.GO_INSIDE:
                {
                    GameState s = state.Set(state.AnnanaHouse.SetIsInside(true));
                    return s.Set(s.AnnanaHouse.SetIsOutside(false));
                }
            case ActionType.FLY_AWAY:
                return state.Set(state.AnnanaHouse.SetFlyAway(true));
            case ActionType.PICK_UP_CRYSTAL_BALL:
                return state.Set(state.AnnanaHouse.SetIsCrystalBallPickedUp(true));
            case ActionType.TAKE_EMPTY_VIAL:
                {
                    // Since we cannot stack do not take new vial if you already have one
                    if (Inventory.Instance.Contains((int)AnnanaInventory.ItemIds.EmptyVial))
                        return state;
                    return state.Set(state.AnnanaHouse.SetEmptyVialPickedUpCount(state.AnnanaHouse.EmptyVialPickedUpCount + 1));
                }

        }

        return state;
    }
}
