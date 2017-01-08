using UnityEngine;
using System.Collections;
using System;

public class HubaBusTips : TipManager
{
    protected override string GetTipText(GameState gameState)
    {
        var state = gameState.HubaBus;

        if (!state.isDrunk)
            return "You need to drink your ordered elixir. Let's hope it comes and that it's the right one.";
        

        return "There's nothing else you need to do";
    }
}
