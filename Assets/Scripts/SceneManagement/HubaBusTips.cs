using UnityEngine;
using System.Collections;
using System;

public class HubaBusTips : TipManager
{
    protected override string GetTipText(GameState gameState)
    {
        var state = gameState.HubaBus;

        if (state.isDelivered && !state.isOpened)
            return "Get the package, it might be the elixir you ordered.";

        if (state.isOpened && !state.isDrunk)
            return "Wow! You can drink the elixir from the inventory. Just put it on yourself.";

        if (state.isDrunk && !state.isBusWaiting && !state.hasBusLeft)
            return "Maybe you should go to the bus stop so you don't miss the bus.";

        if (state.isDrunk && state.isBusWaiting && !state.askedForTicket)
            return "Ask where the bus is going and whether you can get the ticket.";

        if (state.isDrunk && state.askedForTicket && !state.hasBusLeft && !state.isPaid && !state.isInTheBus)
            return "What?! You don't want to pay or what? Just give the nice lizard his money.";

        if (state.isDrunk && state.hasBusLeft && !state.getOnTheBus)
            return "Oh no, you needed to get on that bus.";

        if (!state.isDrunk)
            return "You need to drink your ordered elixir. Let's hope it comes and that it's the right one.";


        return "There's nothing else you need to do";
    }
}
