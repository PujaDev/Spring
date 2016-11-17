using UnityEngine;
using System.Collections;
using System;

public class Crown : IInteractable
{
    protected override Action[] getActionList()
    {
        return new Action[] {
            new Action(ActionType.SELL),
            new Action(ActionType.PUT_ON),
        };
    }
}
