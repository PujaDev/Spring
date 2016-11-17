using UnityEngine;
using System.Collections;
using System;

public class CrystalBall : IInteractable {

    protected override Action[] getActionList()
    {
        // if state == taky then nahadz to pola akcie
        return new Action[1] { new Action(ActionType.TELL_FORTUNE) };
    }
}
