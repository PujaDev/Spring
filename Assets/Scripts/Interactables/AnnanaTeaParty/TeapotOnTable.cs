using UnityEngine;
using System.Collections;
using System;

public class TeapotOnTable : IInteractable
{
    protected override SpringAction[] GetActionList()
    {
        return  new SpringAction[] {
                new SpringAction(ActionType.TAKE, "Take teapot",icons[0],AnnanaTeaPartyInventory.ItemIds.PotEmpty)
            };
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        gameObject.SetActive(newState.AnnanaTeaParty.TeapotOnTheTable);
    }
}
