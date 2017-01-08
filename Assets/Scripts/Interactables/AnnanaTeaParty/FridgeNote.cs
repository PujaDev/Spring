using UnityEngine;
using System.Collections;
using System;

public class FridgeNoteTeaParty : IChangable
{

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        this.gameObject.SetActive(!newState.AnnanaHouse.IsAddressPickedUp);
    }
}
