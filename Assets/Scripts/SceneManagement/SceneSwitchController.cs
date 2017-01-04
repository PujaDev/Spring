using UnityEngine;
using System.Collections;
using System;
using Fungus;

public class SceneSwitchControler : IChangable
{
    public static SceneSwitchControler Instance { get; private set; }

<<<<<<< HEAD
    protected Flowchart Flowchart;

    public virtual void OnStateChanged(GameState newState, GameState oldState)
    {
    }

    protected virtual void Start()
=======
    override public void OnStateChanged(GameState newState, GameState oldState)
    {
    }

    override protected void Start()
>>>>>>> feature/Animations
    {
        if (Instance == null)
        {
            Instance = this;
<<<<<<< HEAD

            Flowchart = GameObject.FindGameObjectWithTag("Scenarios").GetComponent<Flowchart>();

            GameState gameState = StateManager.Instance.Subscribe(this);
            OnStateChanged(gameState, null);
=======
            base.Start();
>>>>>>> feature/Animations
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
