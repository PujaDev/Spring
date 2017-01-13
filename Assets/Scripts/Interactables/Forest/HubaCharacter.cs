using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class HubaCharacter : IChangable, IItemUsable
{
    public GameObject Map;
    public BookHandler Handler;

    HashSet<int> UsableItems;

    void Awake()
    {
        UsableItems = new HashSet<int>()
        {
            (int)HubaForestInventory.ItemIds.Map
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
            if (itemId == (int)HubaForestInventory.ItemIds.Map)
            {
                StateManager.Instance.DispatchAction(new SpringAction(ActionType.START_READING_MAP, "", null), null);
            }
        }
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        // Start reading
        if (newState.HubaForest.IsReadingMap && (oldState == null || !oldState.HubaForest.IsReadingMap))
        {
            Map.SetActive(true);
            Handler.OpenBook();
            GameController.Instance.isUI = true;
        }
        // Stop reading
        else if (!newState.HubaForest.IsReadingMap && (oldState == null || oldState.HubaForest.IsReadingMap))
        {
            Map.SetActive(false);
            GameController.Instance.isUI = false;
        }
    }
}