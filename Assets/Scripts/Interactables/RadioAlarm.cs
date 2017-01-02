using UnityEngine;
using System.Collections;
using System;

public class RadioAlarm : IInteractable {

    Vector3 defaultScale;

    protected override void Awake()
    {
        base.Awake();

        defaultScale = gameObject.transform.localScale;
    }

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
        gameObject.transform.localScale = newState.AnnanaHouse.AlarmPostponed ? new Vector3(3,3,1) : defaultScale;
        gameObject.SetActive(!newState.AnnanaHouse.AlarmTurnedOff);
    }

}
