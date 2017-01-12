using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using Item = AnnanaTeaPartyInventory.ItemIds;

public class AnnanaTeaPartySceneReducer : Reducer
{
    public override GameState Reduce(GameState state, SpringAction action, IInteractable source = null)
    {
        HashSet<int> newItems;
        switch (action.Type)
        {
            case ActionType.GO_OUTSIDE:
                {
                    GameState s = state.Set(state.AnnanaTeaParty.SetIsOutside(true));
                    return s.Set(s.AnnanaTeaParty.SetIsInside(false));
                }
            case ActionType.GO_INSIDE:
                {
                    GameState s = state.Set(state.AnnanaTeaParty.SetIsInside(true));
                    return s.Set(s.AnnanaTeaParty.SetIsOutside(false));
                }

            case ActionType.TAKE:
                int itemId = (int)action.Data;
                var oldItems = state.AnnanaTeaParty.PickedUpItems;
                if (Enum.IsDefined(typeof(Item), itemId))
                {
                    newItems = new HashSet<int>(oldItems);
                    newItems.Add(itemId);
                    if (newItems.Equals(oldItems))
                        return state;

                    switch ((Item)itemId)
                    {
                        case Item.PotEmpty:
                        case Item.PotColdWater:
                        case Item.PotHotWater:
                            state = state.Set(state.AnnanaTeaParty.SetTeapotOnTheTable(false).SetTeapotOnTheStove(-1));
                            break;
                    }

                    InventoryAnimator.Instance.Animate();
                    return state.Set(state.AnnanaTeaParty.SetPickedUpItems(newItems));
                }
                break;

            case ActionType.FILL_TEAPOT:
                {
                    InventoryAnimator.Instance.Animate();
                    return state.Set(state.AnnanaTeaParty.SetPickedUpItems(
                                            swap(state,
                                                Item.PotColdWater,
                                                Item.PotEmpty
                                        )));
                }

            case ActionType.PUT_TEAPON_ON_THE_STOVE:
                var inventory = state.AnnanaTeaParty.PickedUpItems;
                var pots = new Item[]
                {
                    Item.PotEmpty,
                    Item.PotColdWater,
                    Item.PotHotWater
                };
                var heldPod = Item.None;
                int potId = -1;
                foreach(var pot in pots)
                {
                    potId++;
                    if (inventory.Contains((int)pot))
                    {
                        heldPod = pot;
                        break;
                    }
                }
                return state.Set(
                    state.AnnanaTeaParty.SetPickedUpItems(
                        remove(state, heldPod)
                        ).SetTeapotOnTheStove(potId));

            case ActionType.WATER_BOILED:
                return state.Set(state.AnnanaTeaParty.SetTeapotOnTheStove(2));

            case ActionType.FILL_CUP:
                return state.Set(
                    state.AnnanaTeaParty.SetPickedUpItems(
                        remove(state, (Item)action.Data
                        )).SetWaterInTheCup((int)action.Data));

            case ActionType.USE_TEA_BAG:
                return state.Set(
                    state.AnnanaTeaParty.SetPickedUpItems(
                        remove(state, Item.TeaBag
                        )).SetTeaBagInTheCup(true));

            case ActionType.STEEP_TEA:
                return state.Set(state.AnnanaTeaParty.SetWaterInTheCup(0));

            case ActionType.OVERSTEEP_TEA:
                return state.Set(state.AnnanaTeaParty.SetWaterInTheCup(3));

            case ActionType.DRINK_TEA:
                var happy = state.AnnanaTeaParty.WaterInTheCup == 0;
                return state.Set(
                    state.AnnanaTeaParty
                    .SetPickedUpItems(add(state,Item.Cup))
                    .SetWaterInTheCup(-1)
                    .SetDrankTea(true).SetIsHappy(happy)
                    );

            case ActionType.WALKED_OUT:
                return state.Set(state.AnnanaTeaParty.SetInTheKitchen(false));

            case ActionType.TAKE_FINE:
                return state.Set(state.AnnanaTeaParty.SetIsReadingTheFine(true).SetTookTheFine(true));

            case ActionType.FINISH_READING_FINE:
                return state.Set(state.AnnanaTeaParty.SetIsReadingTheFine(false));

            case ActionType.FLY_AWAY:
                return state.Set(state.AnnanaTeaParty.SetOwlFlownAway(true));

            case ActionType.THROW_CUP:
                return state.Set(state.AnnanaTeaParty.SetThrewCup(true));

            case ActionType.UNLOCK_FRIDGE:
                LockManager.Instance.Open();
                break;

            case ActionType.FRIDGE_UNLOCKED:
                return state.Set(state.AnnanaTeaParty.SetIsLockOpen(true));

        }

        return state;
    }

    private HashSet<int> add(GameState state, Item putIn)
    {
        return swap(state, putIn, Item.None);
    }

    private HashSet<int> remove(GameState state, Item throwOut)
    {
        return swap(state, Item.None, throwOut);
    }

    private HashSet<int> swap(GameState state, Item putIn, Item throwOut = Item.None)
    {
        var newItems = new HashSet<int>(state.AnnanaTeaParty.PickedUpItems);
        if (putIn != Item.None)
            newItems.Add((int)putIn);
        if (throwOut != Item.None)
            newItems.Remove((int)throwOut);
        return newItems;
    }

}
