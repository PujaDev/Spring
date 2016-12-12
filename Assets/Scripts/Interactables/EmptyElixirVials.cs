using UnityEngine;
using System.Collections;
using System;

public class EmptyElixirVials : IInteractable {
    protected override SpringAction[] GetActionList()
    {
        return new SpringAction[] {
            new SpringAction(ActionType.LOOK, "Look at",icons[0])
        };
    }
}
