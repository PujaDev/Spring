using UnityEngine;
using System.Collections;
using System;

public class HubaBusFirstStop : IInteractable

{
    private SpringAction[] Actions;
    public TortoiseBusDayAnimator tortoise;

    override protected void Start()
    {
        base.Start();
    }

    protected override SpringAction[] GetActionList()
    {
        return Actions;
    }
    protected override void Awake()
    {
        base.Awake();
        Actions = new SpringAction[]
        {
            new SpringAction(ActionType.LOOK, "Look at this old bus", icons[0]),
            new SpringAction(ActionType.GET_TICKET, "Ask driver for a ticket", icons[1])
        };
    }

    public void BusComesToTheBusstop()
    {
        tortoise.BusArrives();
    }

    public void setInteractibleActive(bool toggle)
    {
        GetComponent<Collider2D>().enabled = toggle;
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        if (newState.HubaBus.isBusWaiting && !newState.HubaBus.hasBusLeft
            && (oldState == null || !oldState.HubaBus.isBusWaiting))
        {
            tortoise.Arrived();
            setInteractibleActive(true);
        }
    }
}
