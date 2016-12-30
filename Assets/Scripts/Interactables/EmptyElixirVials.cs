using UnityEngine;
using System.Collections;
using System;

public class EmptyElixirVials : IInteractable {

    public int VialCount;

    private SpringAction[] Actions;

    void Awake()
    {
        Actions = new SpringAction[]
        {
            new SpringAction(ActionType.TAKE_EMPTY_VIAL, "Take empty vial", icons[0])
        };
    }


    protected override SpringAction[] GetActionList()
    {
        return Actions;
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        if (oldState == null)
        {
            VialCount -= newState.AnnanaHouse.EmptyVialPickedUpCount;
        }
        else if (newState.AnnanaHouse.EmptyVialPickedUpCount > oldState.AnnanaHouse.EmptyVialPickedUpCount)
        {
            VialCount -= newState.AnnanaHouse.EmptyVialPickedUpCount - oldState.AnnanaHouse.EmptyVialPickedUpCount;
        }

        if (VialCount <= 0)
        {
            Actions = new SpringAction[]
            {
                new SpringAction(ActionType.LOOK, "No more vials", icons[0])
            };
        }
    }
}
