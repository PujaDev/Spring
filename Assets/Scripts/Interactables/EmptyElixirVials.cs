using UnityEngine;
using System.Collections;
using System;

public class EmptyElixirVials : IInteractable {

    private SpringAction[] Actions;

    protected override void Awake()
    {
        base.Awake();

        Actions = new SpringAction[]
        {
            new SpringAction(ActionType.TAKE, "Take empty vial", icons[0], AnnanaInventory.ItemIds.EmptyVial)
        };
    }


    protected override SpringAction[] GetActionList()
    {
        return Actions;
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        if (newState.AnnanaHouse.IsEmptyVialPickedUp && (oldState == null || !oldState.AnnanaHouse.IsEmptyVialPickedUp))
        {
            Actions = new SpringAction[]
            {
                new SpringAction(ActionType.LOOK, "No more vials", icons[1])
            };
        }
    }
}
