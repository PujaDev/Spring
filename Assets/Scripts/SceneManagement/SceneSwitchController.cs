using UnityEngine;
using System.Collections;
using System;
using Fungus;

public class SceneSwitchControler : MonoBehaviour, IChangable
{
    public static SceneSwitchControler Instance { get; private set; }

    protected Flowchart Flowchart;

    public virtual void OnStateChanged(GameState newState, GameState oldState)
    {
    }

    protected virtual void Start()
    {
        if (Instance == null)
        {
            Instance = this;

            Flowchart = GameObject.FindGameObjectWithTag("Scenarios").GetComponent<Flowchart>();

            GameState gameState = StateManager.Instance.Subscribe(this);
            OnStateChanged(gameState, null);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
