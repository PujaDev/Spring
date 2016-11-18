using UnityEngine;
using System.Collections;
using System;

public class RadioAlarm : IInteractable {
    protected override Action[] getActionList()
    {
        return new Action[] {
            new Action(ActionType.TURN_ALARM_OFF, "Turn alarm off",icons[0]),
            new Action(ActionType.POSTPONE_ALARM, "Postpone alarm",icons[1]),
        };
    }
    
}
