using UnityEngine;
using System.Collections;
using System;

public class CrystalBall : IInteractable {

    
    protected override SpringAction[] GetActionList()
    {
        // if state == taky then nahadz to pola akcie
        return new SpringAction[] {
            new SpringAction(ActionType.TELL_FORTUNE, "Look into",icons[0]),
            new SpringAction(ActionType.TELL_FORTUNE, "Sell",icons[1]),
            new SpringAction(ActionType.TELL_FORTUNE, "Break",icons[2]),
            new SpringAction(ActionType.TELL_FORTUNE, "Lick",icons[3]),
        };
    }
}
