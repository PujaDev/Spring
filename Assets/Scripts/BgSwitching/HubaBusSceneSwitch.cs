using UnityEngine;
using System.Collections;

public class HubaBusSceneSwitch : SceneSwitchControler
{

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        // We just came to ritual site
        if (newState.HubaBus.isInTheBus && (oldState == null || !oldState.HubaBus.isInTheBus))
        {
            string mssg = oldState == null ? "GoBusSwitch" : "GoBus";
            Debug.Log(mssg);
            Flowchart.SendFungusMessage(mssg);
        }
    }
}
