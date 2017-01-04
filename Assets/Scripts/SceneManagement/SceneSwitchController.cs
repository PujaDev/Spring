using UnityEngine;
using System.Collections;
using System;

public class SceneSwitchControler : IChangable
{
    public static SceneSwitchControler Instance { get; private set; }

    override public void OnStateChanged(GameState newState, GameState oldState)
    {
    }

    override protected void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            base.Start();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
