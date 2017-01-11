using UnityEngine;
using System.Collections;
using System;

public class TurnRoadTripOn : IChangable {

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        if (newState.HubaBus.isInTheBus)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}
