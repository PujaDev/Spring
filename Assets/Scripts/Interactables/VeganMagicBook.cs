using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class VeganMagicBook : IInteractable
{
    public GameObject Book;
    public PageTurner Turner;

    protected override Action[] GetActionList()
    {
        return new Action[]{
            new Action(ActionType.START_READING_VEGAN_BOOK, "Read", icons[0])
        };
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        if (newState.Test.ReadingVeganBook)
        {
            Book.SetActive(true);
            Turner.OpenBook();
            GameController.controller.isUI = true;
        }
        else
        {
            Book.SetActive(false);
            GameController.controller.isUI = false;
        }
    }
}
