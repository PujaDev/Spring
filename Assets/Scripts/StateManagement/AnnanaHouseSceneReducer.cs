using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

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
                {
                    string dress = (string)action.Data;
                    return state.Set(state.AnnanaHouse.SetAnnanaDress(dress));
                }
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
            case ActionType.TAKE:
                {
                    int itemId = (int)action.Data;

                    switch ((AnnanaInventory.ItemIds)itemId)
                    {
                        case AnnanaInventory.ItemIds.Flower:
                            return state.Set(state.AnnanaHouse.SetIsFlowerPickedUp(true));
                        case AnnanaInventory.ItemIds.CrystalBall:
                            return state.Set(state.AnnanaHouse.SetIsCrystalBallPickedUp(true));
                        case AnnanaInventory.ItemIds.EmptyVial:
                            return state.Set(state.AnnanaHouse.SetIsEmptyVialPickedUp(true));
                        case AnnanaInventory.ItemIds.Berry:
                            return state.Set(state.AnnanaHouse.SetIsBerryPickedUp(true));
                        case AnnanaInventory.ItemIds.Leaf:
                            return state.Set(state.AnnanaHouse.SetIsLeafPickedUp(true));
                        case AnnanaInventory.ItemIds.NoteAddress:
                            return state.Set(state.AnnanaHouse.SetIsAddressPickedUp(true));
                    }
                    break;
                }
            case ActionType.THROW_TO_BOILER:
                {
                    int itemId = (int)action.Data;
                    
                    // Throw in new item
                    HashSet<int> contents = new HashSet<int>(state.AnnanaHouse.BoilerContents);
                    contents.Add(itemId);

                    GameState s = null;

                    switch ((AnnanaInventory.ItemIds)itemId)
                    {
                        case AnnanaInventory.ItemIds.Flower:
                            s = state.Set(state.AnnanaHouse.SetIsFlowerUsed(true));
                            break;
                        case AnnanaInventory.ItemIds.EmptyVial:
                            s = state.Set(state.AnnanaHouse.SetIsEmptyVialUsed(true));
                            break;
                        case AnnanaInventory.ItemIds.Berry:
                            s = state.Set(state.AnnanaHouse.SetIsBerryUsed(true));
                            break;
                        case AnnanaInventory.ItemIds.Leaf:
                            s = state.Set(state.AnnanaHouse.SetIsLeafUsed(true));
                            break;
                        default:
                            break;
                    }

                    return s.Set(s.AnnanaHouse.SetBoilerContents(contents));
                }
            case ActionType.EMPTY_BOILER:
                return state.Set(state.AnnanaHouse.SetBoilerContents(new HashSet<int>()));
            case ActionType.FILL_ELIXIR:
                {
                    GameState s = state.Set(state.AnnanaHouse.SetIsEmptyVialUsed(true));

                    HashSet<int> ingredients = state.AnnanaHouse.BoilerContents;

                    var inv = Inventory.Instance as AnnanaInventory;

                    var e = inv.Elixirs
                        .Where(x => x.Value.Ingredients.SetEquals(ingredients))
                        .FirstOrDefault();
                    
                    if (e.Value == null) // Unknown elixir = soup
                    {
                        return s.Set(s.AnnanaHouse.SetElixirId((int)AnnanaInventory.ElixirTypes.Soup));
                    }

                    // Known elixir set it
                    return s.Set(s.AnnanaHouse.SetElixirId(e.Key));
                }
            case ActionType.GIVE_ADDRESS_TO_OWL:
                {
                    GameState s = state.Set(state.AnnanaHouse.SetIsAddressUsed(true));
                    return s.Set(s.AnnanaHouse.SetOwlHasAddress(true));
                }
            case ActionType.GIVE_PACKAGE_TO_OWL:
                {
                    // Assume only elixirs can be sent
                    GameState s = state.Set(state.AnnanaHouse.SetIsElixirUsed(true));
                    return s.Set(s.AnnanaHouse.SetOwlPackage((int)action.Data));
                }
            case ActionType.START_READING_FRIDGE_NOTE:
                return state.Set(state.AnnanaHouse.SetIsReadingFridgeNote(true));
            case ActionType.STOP_READING_FRIDGE_NOTE:
                return state.Set(state.AnnanaHouse.SetIsReadingFridgeNote(false));
        }

        return state;
    }
}
