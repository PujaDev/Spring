using UnityEngine;
using System.Collections;

public class ElixirOwl : IInteractable
{
    protected override SpringAction[] GetActionList()
    {
        return new SpringAction[] {
            new SpringAction(ActionType.FLY_AWAY, "Fly away",icons[0]),
        };
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        if (newState.AnnanaHouse.FlyAway)
            gameObject.GetComponent<AnnanasOwlAnimator>().Fly();
    }
}
