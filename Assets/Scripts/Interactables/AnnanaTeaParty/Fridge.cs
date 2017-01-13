using UnityEngine;
using System.Collections;
using System;

public class Fridge : IInteractable
{
    public LockManager LockManager;

    SpringAction[] Actions;

    protected override void Awake()
    {
        base.Awake();

        Actions = new SpringAction[]
        {
            new SpringAction(ActionType.UNLOCK_FRIDGE, "Unlock fridge", icons[0])
        };
    }

    protected override SpringAction[] GetActionList()
    {
        return Actions;
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        if (newState.AnnanaTeaParty.IsLockOpen && (oldState == null || !oldState.AnnanaTeaParty.IsLockOpen))
        {
            Actions = new SpringAction[]
            {
                new SpringAction(ActionType.OPEN_FRIDGE, "Open fridge", icons[0])
            };
        }
    }
}
