using UnityEngine;
using System.Collections;
using System;

public class VeganMagicBook : IInteractable {
    protected override Action[] getActionList()
    {
        return new Action[] {
            new Action(ActionType.TAKE, "Take vial",icons[0])
        };
    }
    
}
