using UnityEngine;
using System.Collections;
using System;

public class RadioAlarm : IInteractable {

    protected override SpringAction[] GetActionList()
    {
        return new SpringAction[] {
            new SpringAction(ActionType.TURN_ALARM_OFF, "Turn alarm off",icons[0]),
            new SpringAction(ActionType.POSTPONE_ALARM, "Postpone alarm",icons[1]),
            new SpringAction(ActionType.TURN_ALARM_OFF, "Turn alarm off",icons[0]),
            new SpringAction(ActionType.POSTPONE_ALARM, "Postpone alarm",icons[1]),
            new SpringAction(ActionType.TURN_ALARM_OFF, "Turn alarm off",icons[0]),
        };
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        if (newState.Test.AlarmPostponed)
            gameObject.transform.localScale = new Vector3(3,3,1);
        if (newState.Test.AlarmTurnedOff)
            gameObject.SetActive(false);
    }

}
