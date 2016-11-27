using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// Stores game state and handles its changes
/// </summary>
public class StateManager : MonoBehaviour
{
    //-- Public --//
    // Singleton instance
    public static StateManager Instance { get; private set; }
    /// <summary>
    /// Current game state
    /// </summary>
    public GameState State { get; private set; }

    //-- Private --//
    /// <summary>
    /// Set of reducers for current state
    /// </summary>
    private HashSet<Reducer> reducers;
    /// <summary>
    /// Set of interactables subscribed to state changes
    /// </summary>
    private HashSet<IInteractable> interactables;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            State = new GameState();
            // Ensures that OnSceneStart is called whenever
            // a scene is loaded
            SceneManager.sceneLoaded += OnSceneStart;
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Changes game state depending on dispatched action.
    /// Notifies subscribed interactables about these changes.
    /// </summary>
    /// <param name="action">Dispatched action</param>
    /// <param name="actionSource">Interactable that dispatched action</param>
    public void DispatchAction(Action action, IInteractable actionSource)
    {
        var newState = State;
        foreach (var reducer in reducers)
        {
            newState = reducer.Reduce(newState, action, actionSource);
        }

        // TODO compare old and new state before notifying about changes
        foreach(var interactable in interactables)
        {
            interactable.OnStateChanged(newState, State);
        }
        State = newState;
    }

    /// <summary>
    /// Clear all subscribed interactables and reducers
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    void OnSceneStart(Scene scene, LoadSceneMode mode)
    {
        interactables = new HashSet<IInteractable>();
        reducers = new HashSet<Reducer>();
    }

    /// <summary>
    /// Adds new reducers to handle dispatched actions
    /// </summary>
    /// <param name="reducer">Reducer to add</param>
    public void AddReducer(Reducer reducer)
    {
        reducers.Add(reducer);
    }

    /// <summary>
    /// Registers new interactable to notify about state changes
    /// </summary>
    /// <param name="interactable">Interactable to register</param>
    /// <returns>Current game state</returns>
    public GameState Subscribe(IInteractable interactable)
    {
        interactables.Add(interactable);
        return State;
    }
    /// <summary>
    /// Stops interactable from receiving notifications about state changes
    /// </summary>
    /// <param name="interactable">Interactable to register</param>
    /// <returns>Current game state</returns>
    public void Unsubscribe(IInteractable interactable)
    {
        interactables.Remove(interactable);
    }
}
