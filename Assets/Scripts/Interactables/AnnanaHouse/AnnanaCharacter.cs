using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class AnnanaCharacter : IChangable, IItemUsable
{
    private HashSet<int> UsableItems;

    void Awake()
    {
        UsableItems = new HashSet<int>()
        {
            (int)AnnanaInventory.ItemIds.NoteAddress
        };
    }
    
    public bool CanUseOnSelf(int itemId)
    {
        return UsableItems.Contains(itemId);
    }

    public void UseOnSelf(int itemId)
    {
        if (CanUseOnSelf(itemId))
        {
            if(itemId == (int)AnnanaInventory.ItemIds.NoteAddress)
            {
                StateManager.Instance.DispatchAction(new SpringAction(ActionType.START_READING_FRIDGE_NOTE, "", null), null);
            }
        }
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        throw new NotImplementedException();
    }
}
