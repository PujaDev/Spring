using UnityEngine;
using System.Collections;
using System;

public class Crown : IInteractable
{
    protected override SpringAction[] GetActionList()
    {
        return new SpringAction[] {
            new SpringAction(ActionType.SELL, "Put on head",icons[0]),
            new SpringAction(ActionType.PUT_ON, "Sell",icons[1]),
            new SpringAction(ActionType.SELL, "Examine",icons[2]),
        };
    }
}
