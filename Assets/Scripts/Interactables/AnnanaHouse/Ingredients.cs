using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Ingredients : IInteractable
{
    private List<SpringAction> Actions;
    private SpringAction TakeFlower;
    private SpringAction TakeBerry;
    private SpringAction TakeLeaf;

    protected override void Awake()
    {
        base.Awake();

        TakeFlower = new SpringAction(ActionType.TAKE, "Take flower", icons[1], (int)AnnanaInventory.ItemIds.Flower);
        TakeBerry = new SpringAction(ActionType.TAKE, "Take berry", icons[0], (int)AnnanaInventory.ItemIds.Berry);
        TakeLeaf = new SpringAction(ActionType.TAKE, "Take leaf", icons[2], (int)AnnanaInventory.ItemIds.Leaf);
        Actions = new List<SpringAction>
        {
            TakeFlower,
            TakeBerry,
            TakeLeaf
        };
    }

    protected override SpringAction[] GetActionList()
    {
        return Actions.ToArray();
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        if(newState.AnnanaHouse.IsBerryPickedUp && (oldState == null || !oldState.AnnanaHouse.IsBerryPickedUp))
        {
            Actions.Remove(TakeBerry);
        }
        if (newState.AnnanaHouse.IsFlowerPickedUp && (oldState == null || !oldState.AnnanaHouse.IsFlowerPickedUp))
        {
            Actions.Remove(TakeFlower);
        }
        if (newState.AnnanaHouse.IsLeafPickedUp && (oldState == null || !oldState.AnnanaHouse.IsLeafPickedUp))
        {
            Actions.Remove(TakeLeaf);
        }

        if (Actions.Count == 0)
        {
            Actions.Add(new SpringAction(ActionType.LOOK, "No more ingredients", icons[3]));
        }
    }
}
