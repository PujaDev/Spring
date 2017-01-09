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
            new SpringAction(ActionType.GET_TICKET, "Ask driver for a ticket", icons[0])
        };
    }

    public void BusComesToTheBusstop()
    {
        if (StateManager.Instance.State.HubaBus.isDrunk && StateManager.Instance.State.AnnanaHouse.OwlPackage == (int)AnnanaInventory.ItemIds.Invis) {
            tortoise.BusGoesOn();
        }
        else
        {
            tortoise.BusArrives();
        }
    }

    public void setInteractibleActive(bool toggle)
    {
        GetComponent<Collider2D>().enabled = toggle;
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        if (newState.HubaBus.isBusWaiting && !newState.HubaBus.hasBusLeft)
        {
            tortoise.Arrived();
            if (!newState.HubaBus.askedForTicket)
            {
                setInteractibleActive(true);
            }
            else
            {
                setInteractibleActive(false);
                if (!newState.HubaBus.getOnTheBus && DialogManager.Instance.currentDialogue != (int)HubaBusDialogManager.DialogueTypes.Edible && DialogManager.Instance.currentLine == 0)
                    tortoise.WaitToGetPaid();

                if (newState.HubaBus.getOnTheBus && !newState.HubaBus.isInTheBus)
                    tortoise.GetPaid();
            }
        }
    }
}
