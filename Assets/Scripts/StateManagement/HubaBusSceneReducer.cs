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
                    InventoryAnimator.Instance.Animate();

                    // Pickup whatever was delivered
                    var pickedUp = new HashSet<int>(state.HubaBus.PickedUpItems);
                    var elixir = state.AnnanaHouse.OwlPackage;
                    if (elixir == (int)AnnanaInventory.ItemIds.Antidote)
                    {
                        pickedUp.Add((int)HubaBusInventory.ItemIds.Antidote);
                    }
                    else if (elixir == (int)AnnanaInventory.ItemIds.Shrink)
                    {
                        pickedUp.Add((int)HubaBusInventory.ItemIds.Shrink);
                    }
                    else if (elixir == (int)AnnanaInventory.ItemIds.Invis)
                    {
                        pickedUp.Add((int)HubaBusInventory.ItemIds.Invis);
                    }
                    else if (elixir == (int)AnnanaInventory.ItemIds.Soup)
                    {
                        pickedUp.Add((int)HubaBusInventory.ItemIds.Soup);
                    }
                    else Debug.Log("opened weird elixir");

                    source.gameObject.GetComponent<PackagedElixir>().TogglePackage(false);

                    GameState s = state.Set(state.HubaBus.SetPickedUpItems(pickedUp));
                    return s.Set(s.HubaBus.SetisOpened(true));
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
            case ActionType.EXIT_HOUSE:
                {
                    return state.Set(state.HubaBus.SetisOutOfTheHouse(true));
                }
            case ActionType.DRINK:
                {
                    var used = new HashSet<int>(state.HubaBus.UsedItems);
                    var elixir = state.AnnanaHouse.OwlPackage;
                    if (elixir == (int)AnnanaInventory.ItemIds.Antidote)
                    {
                        used.Add((int)HubaBusInventory.ItemIds.Antidote);
                    }
                    else if (elixir == (int)AnnanaInventory.ItemIds.Shrink)
                    {
                        used.Add((int)HubaBusInventory.ItemIds.Shrink);
                    }
                    else if (elixir == (int)AnnanaInventory.ItemIds.Invis)
                    {
                        used.Add((int)HubaBusInventory.ItemIds.Invis);
                    }
                    else if (elixir == (int)AnnanaInventory.ItemIds.Soup)
                    {
                        used.Add((int)HubaBusInventory.ItemIds.Soup);
                    }

                    GameState s = state.Set(state.HubaBus.SetUsedItems(used));
                    return s.Set(s.HubaBus.SetisDrunk(true));
                }
            case ActionType.GET_TICKET:
                {
                    var elixir = state.AnnanaHouse.OwlPackage;
                    if (elixir == (int)AnnanaInventory.ItemIds.Antidote)
                    {
                        DialogManager.Instance.SetDialogue((int)HubaBusDialogManager.DialogueTypes.Edible);
                        DialogManager.Instance.Next();
                    }
                    else {
                        DialogManager.Instance.SetDialogue((int)HubaBusDialogManager.DialogueTypes.Poisonous);
                        DialogManager.Instance.Next();
                    }
                    //source.gameObject.SetActive(false);
                    return state.Set(state.HubaBus.SetaskedForTicket(true));
                }
            case ActionType.BUY_TICKET:
                {
                    var used = new HashSet<int>(state.HubaBus.UsedItems);
                    used.Add((int)HubaBusInventory.ItemIds.GoldCoins);

                    GameState s = state.Set(state.HubaBus.SetUsedItems(used));
                    return s.Set(s.HubaBus.SetgetOnTheBus(true));
                }
            case ActionType.IN_THE_BUS:
                {
                    return state.Set(state.HubaBus.SetisInTheBus(true));
                }
        }

        return state;
    }
}
