using UnityEngine;
using System.Collections;
using System;

public class SceneSwitchControler : MonoBehaviour, IChangable
{
    public static SceneSwitchControler Instance { get; private set; }

    public virtual void OnStateChanged(GameState newState, GameState oldState)
    {
    }

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            GameState gameState = StateManager.Instance.Subscribe(this);
            OnStateChanged(gameState, null);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
