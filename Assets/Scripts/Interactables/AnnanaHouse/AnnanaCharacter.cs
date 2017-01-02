using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class AnnanaCharacter : MonoBehaviour, IItemUsable, IChangable
{
    private HashSet<int> UsableItems;

    void Awake()
    {
        UsableItems = new HashSet<int>()
        {
            (int)AnnanaInventory.ItemIds.NoteAddress
        };
    }

    void Start()
    {
        StateManager.Instance.Subscribe(this);
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

    public void OnStateChanged(GameState newState, GameState oldState)
    {
        // React to state change
    }
}
