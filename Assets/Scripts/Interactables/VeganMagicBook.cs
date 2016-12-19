using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class VeganMagicBook : IInteractable
{
    public GameObject Book;
    public BookHandler Handler;

    protected override SpringAction[] GetActionList()
    {
        return new SpringAction[]{
            new SpringAction(ActionType.START_READING_VEGAN_BOOK, "Read", icons[0])
        };
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        // Start reading
        if (newState.AnnanaHouse.ReadingVeganBook && (oldState == null || !oldState.AnnanaHouse.ReadingVeganBook))
        {
            Book.SetActive(true);
            Handler.OpenBook();
            GameController.controller.isUI = true;
        }
        // Stop reading
        else if (!newState.AnnanaHouse.ReadingVeganBook && (oldState == null || oldState.AnnanaHouse.ReadingVeganBook))
        {
            Book.SetActive(false);
            GameController.controller.isUI = false;
        }
    }
}
