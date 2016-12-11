using UnityEngine;
using System.Collections;
using System;

public class CrystalBallOnTable : IInteractable {
    protected override SpringAction[] GetActionList()
    {
        return new SpringAction[] {
            new SpringAction(ActionType.CALL_MOM, "Call mom",icons[0]),
            new SpringAction(ActionType.TELL_FORTUNE, "Tell fortune",icons[1])
        };
    }
    
}
