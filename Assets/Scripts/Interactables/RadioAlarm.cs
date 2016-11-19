using UnityEngine;
using System.Collections;
using System;

public class RadioAlarm : IInteractable {

    protected override Action[] getActionList()
    {
        return new Action[] {
            new Action(ActionType.TURN_ALARM_OFF, "Turn alarm off",icons[0]),
            new Action(ActionType.POSTPONE_ALARM, "Postpone alarm",icons[1]),
            new Action(ActionType.TURN_ALARM_OFF, "Turn alarm off",icons[0]),
            new Action(ActionType.POSTPONE_ALARM, "Postpone alarm",icons[1]),
            new Action(ActionType.TURN_ALARM_OFF, "Turn alarm off",icons[0]),
        };
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        if (newState.Test.AlarmPostponed)
            gameObject.transform.localScale = new Vector3(3,3,1);
        if(newState.Test.AlarmTurnedOff)
            Destroy(gameObject);
    }

}
