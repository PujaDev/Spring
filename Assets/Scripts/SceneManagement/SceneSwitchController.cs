using UnityEngine;
using System.Collections;
using System;
using Fungus;

public class SceneSwitchControler : IChangable
{
    public static SceneSwitchControler Instance { get; private set; }
    
    protected Flowchart Flowchart;
    
    override public void OnStateChanged(GameState newState, GameState oldState)
    {
    }

    override protected void Start()
    {
        if (Instance == null)
        {
            Instance = this;

            Flowchart = GameObject.FindGameObjectWithTag("Scenarios").GetComponent<Flowchart>();

            GameState gameState = StateManager.Instance.Subscribe(this);
            OnStateChanged(gameState, null);
            base.Start();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
