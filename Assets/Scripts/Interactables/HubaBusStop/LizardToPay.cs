using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LizardToPay : IChangable, IItemUsable
{

    private HashSet<int> UsableItems;

    public bool CanUseOnSelf(int itemId)
    {
        return UsableItems.Contains(itemId);
    }

    public void UseOnSelf(int itemId)
    {
        if (CanUseOnSelf(itemId))
        {
            StateManager.Instance.DispatchAction(new SpringAction(ActionType.BUY_TICKET));
        }
    }
    protected void Awake()
    {
        UsableItems = new HashSet<int>()
        {
            (int)HubaBusInventory.ItemIds.GoldCoins
        };
    }

    public void setInteractibleActive(bool toggle)
    {
        GetComponent<Collider2D>().enabled = toggle;
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        if (!newState.HubaBus.hasBusLeft && newState.HubaBus.askedForTicket && newState.HubaBus.UsedItems.Contains((int)HubaBusInventory.ItemIds.Antidote))
        {
            setInteractibleActive(true);
        }
        else
            setInteractibleActive(false);
    }
}
