using UnityEngine;
using System.Collections;

public class AnnanaHouseSSC : SceneSwitchControler
{
    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        if (newState.AnnanaHouse.IsInside && (oldState == null || !oldState.AnnanaHouse.IsInside))
        {
            var chart = GameObject.FindGameObjectWithTag("Scenarios").GetComponent<Fungus.Flowchart>();

            // Full animation while playing the game
            if (oldState != null)
            {
                chart.SendFungusMessage("GoIn");
            }
            // Load saved game state
            else // oldState == null
            {
                chart.SendFungusMessage("GoInSwitch");
            }
        }
        if (newState.AnnanaHouse.IsOutside && (oldState == null || !oldState.AnnanaHouse.IsOutside))
        {
            var chart = GameObject.FindGameObjectWithTag("Scenarios").GetComponent<Fungus.Flowchart>();

            // Full animation while playing the game
            if (oldState != null)
            {
                chart.SendFungusMessage("GoOut");
            }
            // Load saved game state
            else // oldState == null
            {
                chart.SendFungusMessage("GoOutSwitch");
            }

        }
    }
}
