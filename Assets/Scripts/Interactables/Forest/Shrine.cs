using UnityEngine;
using System.Collections;
using System;

public class Shrine : IInteractable
{
    private SpringAction[] Actions;

    protected override void Awake()
    {
        base.Awake();

        Actions = new SpringAction[]
        {
            new SpringAction(ActionType.LOOK, "Shrine with a bowl for coins", icons[0])
        };
    }

    protected override SpringAction[] GetActionList()
    {
        return Actions;
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        base.OnStateChanged(newState, oldState);
    }
}
