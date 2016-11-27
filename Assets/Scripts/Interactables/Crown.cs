using UnityEngine;
using System.Collections;
using System;

public class Crown : IInteractable
{
    protected override Action[] GetActionList()
    {
        return new Action[] {
            new Action(ActionType.SELL, "Put on head",icons[0]),
            new Action(ActionType.PUT_ON, "Sell",icons[1]),
            new Action(ActionType.SELL, "Examine",icons[2]),
        };
    }
}
