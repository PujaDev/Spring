using UnityEngine;
using System.Collections;
using System;

public class EmptyElixirVials : IInteractable {
    protected override Action[] GetActionList()
    {
        return new Action[] {
            new Action(ActionType.LOOK, "Look at",icons[0])
        };
    }
}
