using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DeliveryOwl : IInteractable, IItemUsable
{
    SpringAction[] Actions;
    SpringAction[] ReadyActions;
    HashSet<int> UsableItems;
    
    protected override void Awake()
    {
        base.Awake();

        Actions = new SpringAction[]
        {
            new SpringAction(ActionType.LOOK, "Owl is not ready to fly", icons[0])
        };

        ReadyActions = new SpringAction[]
        {
            new SpringAction(ActionType.FLY_AWAY, "Send owl", icons[1])
        };

        UsableItems = new HashSet<int>()
        {
            (int)AnnanaInventory.ItemIds.Antidote,
            (int)AnnanaInventory.ItemIds.Shrink,
            (int)AnnanaInventory.ItemIds.Invis,
            (int)AnnanaInventory.ItemIds.Soup,
            (int)AnnanaInventory.ItemIds.NoteAddress
        };
    }


    protected override SpringAction[] GetActionList()
    {
        return Actions;
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        // Just got address but already has package
        if (newState.AnnanaHouse.OwlHasAddress && (oldState == null || !oldState.AnnanaHouse.OwlHasAddress))
        {
            if (newState.AnnanaHouse.OwlPackage != -1)
            {
                Actions = ReadyActions;
            }
        }

        // Just got package and already has address
        if (newState.AnnanaHouse.OwlPackage != -1 && (oldState == null || oldState.AnnanaHouse.OwlPackage == -1))
        {
            gameObject.GetComponentInChildren<AnnanasOwlAnimator>().PackageAppear();
            if (newState.AnnanaHouse.OwlHasAddress)
            {
                Actions = ReadyActions;
            }
        }

        // Owl is away
        if (newState.AnnanaHouse.FlyAway && (oldState == null || !oldState.AnnanaHouse.FlyAway))
        {
            // Disallow interaction
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            
            // Loading state - owl is already away
            if (oldState == null)
                gameObject.SetActive(false);
            else // Playing game - start fly animation
                gameObject.GetComponentInChildren<AnnanasOwlAnimator>().Fly();
        }
    }

    public bool CanUseOnSelf(int itemId)
    {
        if (UsableItems.Contains(itemId))
        {
            // If we want to append package (anything else than address) we need to check if there already isn't one
            if (itemId != (int)AnnanaInventory.ItemIds.NoteAddress
                && StateManager.Instance.State.AnnanaHouse.OwlPackage != -1)
            {
                return false;
            }

            return true;
        }
        return false;
    }

    public void UseOnSelf(int itemId)
    {
        if(CanUseOnSelf(itemId))
        {
            if(itemId == (int)AnnanaInventory.ItemIds.NoteAddress)
            {
                ComeCloser(new SpringAction(ActionType.GIVE_ADDRESS_TO_OWL, "", null));
            }
            else
            {
                ComeCloser(new SpringAction(ActionType.GIVE_PACKAGE_TO_OWL, "", null, itemId));
            }
        }
    }
}
