using UnityEngine;
using System.Collections;
using System;

public class EmptyElixirVials : IInteractable {

    public int VialCount;

    protected override SpringAction[] GetActionList()
    {
        return new SpringAction[] {
            new SpringAction(ActionType.TAKE_EMPTY_VIAL, "Take empty vial", icons[0])
        };
    }
}
