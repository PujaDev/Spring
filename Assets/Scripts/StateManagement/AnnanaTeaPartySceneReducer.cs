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
            //case ActionType.GO_OUTSIDE:
            //    {
            //        GameState s = state.Set(state.AnnanaHouse.SetIsOutside(true));
            //        return s.Set(s.AnnanaHouse.SetIsInside(false));
            //    }
            //case ActionType.GO_INSIDE:
            //    {
            //        GameState s = state.Set(state.AnnanaHouse.SetIsInside(true));
            //        return s.Set(s.AnnanaHouse.SetIsOutside(false));
            //    }
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

                    return state.Set(state.AnnanaTeaParty.SetPickedUpItems(newItems));
                }
                break;

            case ActionType.FILL_TEAPOT:
                return state.Set(state.AnnanaTeaParty.SetPickedUpItems(
                        swap(state,
                            Item.PotColdWater,
                            Item.PotEmpty
                    )));

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
                        swap(state,
                            Item.None,
                            heldPod)
                        ).SetTeapotOnTheStove(potId));

            case ActionType.WATER_BOILED:
                return state.Set(state.AnnanaTeaParty.SetTeapotOnTheStove(2));

            case ActionType.FILL_CUP:
                return state.Set(
                    state.AnnanaTeaParty.SetPickedUpItems(
                        swap(state,
                            Item.None,
                            (Item)action.Data
                        )).SetWaterInTheCup((int)action.Data));

            case ActionType.USE_TEA_BAG:
                return state.Set(
                    state.AnnanaTeaParty.SetPickedUpItems(
                        swap(state,
                            Item.None,
                            Item.TeaBag
                        )).SetTeaBagInTheCup(true));

            case ActionType.STEEP_TEA:
                return state.Set(state.AnnanaTeaParty.SetWaterInTheCup(0));

            case ActionType.OVERSTEEP_TEA:
                return state.Set(state.AnnanaTeaParty.SetWaterInTheCup(3));

            case ActionType.DRINK_TEA:
                var happy = state.AnnanaTeaParty.WaterInTheCup == 0;
                return state.Set(
                    state.AnnanaTeaParty
                    .SetWaterInTheCup(-1)
                    .SetDrankTea(true)
                    .SetIsHappy(happy)
                    );

            case ActionType.WALKED_OUT:
                return state.Set(state.AnnanaTeaParty.SetInTheKitchen(false));

        }

        return state;
    }

    private HashSet<int> swap(GameState state, Item putIn, Item throwOut = Item.None)
    {
        var newItems = new HashSet<int>(state.AnnanaTeaParty.PickedUpItems);
        if (putIn != Item.None)
            newItems.Add((int)putIn);
        if(throwOut!= Item.None)
            newItems.Remove((int)throwOut);
        return newItems;
    }
}
