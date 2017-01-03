using UnityEngine;
using System.Collections;

public class TurtleBusFirstStop : IInteractable
{
    private SpringAction[] Actions;
    public TortoiseBusDayAnimator bus;

    protected override void Awake()
    {
        base.Awake();

        Actions = new SpringAction[]
        {
            new SpringAction(ActionType.LOOK, "Look at this old bus", icons[0]),
            new SpringAction(ActionType.GET_TICKET, "Ask driver for a ticket", icons[1])
        };
    }

    protected override SpringAction[] GetActionList()
    {
        return Actions;
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        //if (newState.HubaForest.IsHubaBlessed && (oldState == null || !oldState.HubaForest.IsHubaBlessed))
        //{
        //    gameObject.GetComponentInChildren<ShrineAnimator>().MoneyIn();
        //    Actions = new SpringAction[]
        //    {
        //        new SpringAction(ActionType.LOOK, "Your path has been revealed", icons[0])
        //    };
        //}
    }

    public void setInteractibleActive() {
        GetComponent<Collider2D>().enabled = true;
    }

    public void StartBus() {
        bus.BusArrives();
    }

}
