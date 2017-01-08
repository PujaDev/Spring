using UnityEngine;
using System.Collections;
using System;

public class RadioAlarm : IInteractable {

    public GameObject ParticleSystemObject;
    public int MaxRotation = 45;
    public float RotationSpeed = 3f;
    public int SnoozeTime = 5;
    ParticleSystem ParticleSystem;

    protected override void Awake()
    {
        base.Awake();
        ParticleSystem = ParticleSystemObject.GetComponent<ParticleSystem>();
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
        StopCoroutine("AlarmRinging");
        ParticleSystem.Stop();

        if (newState.AnnanaHouse.AlarmTurnedOff)
        {
            transform.rotation = Quaternion.identity;
        }
        else if (newState.AnnanaHouse.AlarmPostponed)
        {
            transform.rotation = Quaternion.identity;
            StartCoroutine("PostponeAlarm");
        }
        else
        {
            StartCoroutine("AlarmRinging");
            ParticleSystem.Play();
        }
    }

    IEnumerator AlarmRinging()
    {
        bool toLeft = true;
        var origPos = transform.position;
        for (;;)
        {
            var rotZ = transform.rotation.eulerAngles.z;
            if (toLeft)
            {
                transform.Rotate(0, 0, RotationSpeed);
                if (rotZ < 180 && rotZ > MaxRotation)
                    toLeft = false;
            }
            else
            {
                transform.Rotate(0, 0, -RotationSpeed);
                if (rotZ > 180 && rotZ < 360 - MaxRotation)
                    toLeft = true;
            }
            
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator PostponeAlarm()
    {
        yield return new WaitForSeconds(SnoozeTime);
        StateManager.Instance.DispatchAction(new SpringAction(ActionType.RESET_ALARM));
    }
}
