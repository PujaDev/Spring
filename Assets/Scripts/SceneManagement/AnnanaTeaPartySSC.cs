using UnityEngine;
using System.Collections;

public class AnnanaTeaPartySSC : SceneSwitchControler
{
    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        if (oldState != null && newState.AnnanaTeaParty.IsInside == oldState.AnnanaTeaParty.IsInside)
            return;

        if (newState.AnnanaTeaParty.IsInside)
        {
            // Full animation while playing the game
            if (oldState != null)
            {
                Flowchart.SendFungusMessage("GoIn");
            }
            // Load saved game state
            else // oldState == null
            {
               // Flowchart.SendFungusMessage("GoInSwitch");
            }
        }
        else
        {
            // Full animation while playing the game
            if (oldState != null)
            {
                Flowchart.SendFungusMessage("GoOut");
            }
            // Load saved game state
            else // oldState == null
            {
                Flowchart.SendFungusMessage("GoOutSwitch");
            }

        }
    }
}
