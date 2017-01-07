using UnityEngine;
using System.Collections;
using System;

public class AnnanaHouseTips : TipManager
{
    protected override string GetTipText(GameState gameState)
    {
        var state = gameState.AnnanaHouse;

        if (!state.AlarmTurnedOff)
            return "Turn the alarm off";

        if (!state.DidReadFridgeNote)
            return "Read the note from the fridge";

        if (!state.IsAddressPickedUp)
            return "Take the note from the fridge";

        if (!state.ReadVeganBook)
            return "Read the cookbook book and remember the correct recipe";


        return "There's nothing else you need to do";
    }
}
