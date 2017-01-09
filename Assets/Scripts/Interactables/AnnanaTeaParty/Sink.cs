using UnityEngine;
using System.Collections;
using System;

public class Sink : IInteractable
{
    SpringAction[] Actions;

    protected override SpringAction[] GetActionList()
    {
        return Actions;
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        gameObject.SetActive(!newState.AnnanaTeaParty.DrankTea);

        var hasTeapot = newState.AnnanaTeaParty.PickedUpItems.Contains((int)AnnanaTeaPartyInventory.ItemIds.PotEmpty);

        if (hasTeapot)
        {
            Actions = new SpringAction[] {
                new SpringAction(ActionType.FILL_TEAPOT, "Fill teapot with water",icons[0])
            };
        }
        else
        {
            Actions = new SpringAction[] {
                new SpringAction(ActionType.LOOK, "You have nothing to fill with water", icons[1])
            };
        }
    }
}
