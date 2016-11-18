using UnityEngine;
using System.Collections;
using System;

public class CrystalBall : IInteractable {

    
    protected override Action[] getActionList()
    {
        // if state == taky then nahadz to pola akcie
        return new Action[] {
            new Action(ActionType.TELL_FORTUNE, "Look into",icons[0]),
            new Action(ActionType.TELL_FORTUNE, "Sell",icons[1]),
            new Action(ActionType.TELL_FORTUNE, "Break",icons[2]),
            new Action(ActionType.TELL_FORTUNE, "Lick",icons[3]),
        };
    }
}
