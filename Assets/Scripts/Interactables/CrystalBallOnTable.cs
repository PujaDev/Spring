using UnityEngine;
using System.Collections;
using System;

public class CrystalBallOnTable : IInteractable {
    protected override Action[] getActionList()
    {
        return new Action[] {
            new Action(ActionType.CALL_MOM, "Call mom",icons[0]),
            new Action(ActionType.TELL_FORTUNE, "Tell fortune",icons[1])
        };
    }
    
}
