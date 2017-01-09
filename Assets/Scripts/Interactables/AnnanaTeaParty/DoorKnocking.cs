using UnityEngine;
using System.Collections;
using System;

public class DoorKnocking : IChangable {

    ParticleSystem ParticleSystem;

    void Awake()
    {
        ParticleSystem = gameObject.GetComponent<ParticleSystem>();
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        if (newState.AnnanaTeaParty.TookTheFine)
            ParticleSystem.Stop();
        else
            ParticleSystem.Play();
    }
}
