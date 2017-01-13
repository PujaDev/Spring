using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Shrine : IInteractable, IItemUsable
{
    private SpringAction[] Actions;
    private HashSet<int> UsableItems;

    protected override void Awake()
    {
        base.Awake();

        Actions = new SpringAction[]
        {
            new SpringAction(ActionType.LOOK, "Shrine with a bowl for coins", icons[0])
        };

        UsableItems = new HashSet<int>()
        {
            (int)HubaForestInventory.ItemIds.Coin
        };
    }

    protected override SpringAction[] GetActionList()
    {
        return Actions;
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        if (newState.HubaForest.IsHubaBlessed && (oldState == null || !oldState.HubaForest.IsHubaBlessed))
        {
            gameObject.GetComponentInChildren<ShrineAnimator>().MoneyIn();
            Actions = new SpringAction[]
            {
                new SpringAction(ActionType.LOOK, "Your path has been revealed", icons[0])
            };
        }
    }

    public bool CanUseOnSelf(int itemId)
    {
        return UsableItems.Contains(itemId);
    }

    public void UseOnSelf(int itemId)
    {
        if (CanUseOnSelf(itemId))
        {
            if (itemId == (int)HubaForestInventory.ItemIds.Coin)
            {
                ComeCloser(new SpringAction(ActionType.GIVE_MONEY_TO_SHRINE, "", null));
            }
        }
    }
}
