using UnityEngine;
using System.Collections;
using System;

public class FridgeNote : IInteractable
{
    public GameObject Book;
    public BookHandler Handler;

    protected override SpringAction[] GetActionList()
    {
        return new SpringAction[]{
            new SpringAction(ActionType.START_READING_FRIDGE_NOTE, "Read", icons[0])
        };
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        // Start reading
        if (newState.AnnanaHouse.IsReadingFridgeNote && (oldState == null || !oldState.AnnanaHouse.IsReadingFridgeNote))
        {
            Book.SetActive(true);
            Handler.OpenBook();
            GameController.Instance.isUI = true;
        }
        // Stop reading
        else if (!newState.AnnanaHouse.IsReadingFridgeNote && (oldState == null || oldState.AnnanaHouse.IsReadingFridgeNote))
        {
            Book.SetActive(false);
            GameController.Instance.isUI = false;
        }
    }
}
