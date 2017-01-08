using UnityEngine;
using System.Collections;
using System;

public class OwlTrigger : IChangable
{
    public HubaOwlAnimator owl;

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        if (newState.HubaBus.isDelivered)
        {
            GetComponent<Collider2D>().isTrigger = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        owl.Fly();
        GetComponent<Collider2D>().isTrigger = false;
    }
}
