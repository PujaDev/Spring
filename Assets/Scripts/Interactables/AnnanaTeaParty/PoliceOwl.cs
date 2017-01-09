using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PoliceOwl : IInteractable
{
    SpringAction[] Actions;
    SpringAction[] ActionsBefore;
    SpringAction[] ActionsAfter;

    protected override void Awake()
    {
        base.Awake();

        Actions = ActionsBefore = new SpringAction[]
        {
            new SpringAction(ActionType.TAKE_FINE, "Read the mail", icons[0])
        };

        ActionsAfter = new SpringAction[]
        {
            new SpringAction(ActionType.FLY_AWAY, "Send the owl away", icons[0])
        };
        
    }


    protected override SpringAction[] GetActionList()
    {
        return Actions;
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        var state = newState.AnnanaTeaParty;

        if (state.TookTheFine )
        {
            if (oldState == null || !oldState.AnnanaTeaParty.TookTheFine)
            {
                gameObject.GetComponentInChildren<AnnanasOwlAnimator>().PackageDisppear();
                Actions = ActionsAfter;
                if (state.IsReadingTheFine)
                {
                    DialogManager.Instance.SetDialogue(state.IsHappy ? 4 : 3);
                    DialogManager.Instance.Next();
                }
            }
        }
        else
        {
            if (oldState == null)
                gameObject.GetComponentInChildren<AnnanasOwlAnimator>().PackageAppear();
        }

        if (state.OwlFlownAway)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            if (oldState == null)
            {
                gameObject.SetActive(false);
            }
            else if(!oldState.AnnanaTeaParty.OwlFlownAway)
            {
                gameObject.GetComponentInChildren<AnnanasOwlAnimator>().Fly();
            }

        }
    }
    
}
