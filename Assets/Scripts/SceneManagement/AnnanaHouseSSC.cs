using UnityEngine;
using System.Collections;

public class AnnanaHouseSSC : SceneSwitchControler
{
    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        if (newState.AnnanaHouse.IsInside && (oldState == null || !oldState.AnnanaHouse.IsInside))
        {
            // Full animation while playing the game
            if (oldState != null)
            {
                Flowchart.SendFungusMessage("GoIn");
            }
            // Load saved game state
            else // oldState == null
            {
                Flowchart.SendFungusMessage("GoInSwitch");
            }
        }
        if (newState.AnnanaHouse.IsOutside && (oldState == null || !oldState.AnnanaHouse.IsOutside))
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
