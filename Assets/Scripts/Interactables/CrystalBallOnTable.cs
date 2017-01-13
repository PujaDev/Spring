using UnityEngine;
using System.Collections;
using System;

public class CrystalBallOnTable : IInteractable {

    protected override SpringAction[] GetActionList()
    {
        return new SpringAction[] {
            new SpringAction(ActionType.CALL_MOM, "Call mom", icons[0]),
            new SpringAction(ActionType.TELL_FORTUNE, "Tell fortune", icons[1]),
            new SpringAction(ActionType.TAKE, "Pick up", icons[2], (int)AnnanaInventory.ItemIds.CrystalBall)
        };
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        if (newState.AnnanaHouse.IsCrystalBallPickedUp && (oldState == null || !oldState.AnnanaHouse.IsCrystalBallPickedUp))
        {
            Destroy(gameObject);
        }
    }

}
