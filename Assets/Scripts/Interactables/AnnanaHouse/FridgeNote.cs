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
            new SpringAction(ActionType.START_READING_FRIDGE_NOTE, "Read", icons[0]),
            new SpringAction(ActionType.TAKE, "Take", icons[1], (int)AnnanaInventory.ItemIds.NoteAddress)
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

        // If note is picked up it is no longer on the fridge. We cannot destroy it however since we still need this script
        if (newState.AnnanaHouse.IsAddressPickedUp && (oldState == null || !oldState.AnnanaHouse.IsAddressPickedUp))
        {
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            Destroy(gameObject.GetComponent<SpriteRenderer>());
        }
    }
}
