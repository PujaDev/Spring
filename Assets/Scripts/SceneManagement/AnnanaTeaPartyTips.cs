using UnityEngine;
using System.Collections;
using System;

public class AnnanaTeaPartyTips : TipManager
{
    protected override string GetTipText(GameState gameState)
    {
        var state = gameState.AnnanaTeaParty;

        if (state.WaterInTheCup == 1)
        {
            return "You can't really steep the tea in cold water. You can however reset this scene!";
        }

        if (state.WaterInTheCup == 3)
        {
            return "Uh-oh. It looks like you left the tea bag in way too long. Way longer than it's recommended";
        }
        
        if (!state.DrankTea)
        {
            return "Oh no! Annana is thirsty! She needs her special tea (get it? special-tea?)! Let's hope you know how to make it or else she won't be happy.";
        }


        if (!state.TookTheFine)
        {
            if (!state.IsOutside)
                return "Can you hear it? It looks like someone is calling you outside. Better check it out!";
            else if (state.IsHappy)
                return "Uh-oh. Letter from the police! What could they possibly want from Annana?";
            else
                return "First terrible 'tea' and now she got letter from police? This day isn't going well for Annana!";
        }

        if (state.IsReadingTheFine)
            return "That's harsh :(";

        if (!state.OwlFlownAway)
            return "You can send the owl back now. Then you should start to think about where to get money to pay the fine";



        return ALL_DONE;
    }
}
