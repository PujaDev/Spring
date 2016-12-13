﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

/// <summary>
/// Stores game state and handles its changes
/// </summary>
public class StateManager : MonoBehaviour
{
    public bool DebugLog = true;
    public bool PressToLoadState = false;
    public int StateNum = -1;
    private string SaveDirectory;
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
            // Ensures that OnSceneStart is called whenever
            // a scene is loaded
            SceneManager.sceneLoaded += OnSceneStart;
            SceneManager.sceneUnloaded += OnSceneEnd;
            Instance = this;
            SaveDirectory = Application.persistentDataPath + "/states/";
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
    public void DispatchAction(SpringAction action, IInteractable actionSource)
    {
        var newState = State;
        foreach (var reducer in reducers)
        {
            newState = reducer.Reduce(newState, action, actionSource);
        }

        var differences = newState.CompareChanges(State);
        var changed = differences.Count != 0;
        if (DebugLog)
        {
            Debug.Log("Action " + action.Type);
            if (changed)
            {
                foreach (var d in differences)
                {
                    Debug.Log(d.Value.Count + " changes in " + d.Key);
                    foreach (var change in d.Value)
                    {
                        Debug.Log(change);
                    }
                }
            }
            else
            {
                Debug.Log("No changes.");
            }

        }

        if (!changed)
            return;

        foreach (var interactable in interactables)
        {
            interactable.OnStateChanged(newState, State);
        }

        State = newState;

        StateNum++;
        saveStateToFile();

    }

    public void LoadState(int stateNum)
    {
        if (DebugLog)
            Debug.Log("Loading state " + stateNumToFile(stateNum));

        var loadedState = loadStateFromFile(stateNum);

        GameObject.FindWithTag("Character").transform.position = loadedState.CharacterPosition.GetVector3();

        foreach (var interactable in interactables)
        {
            interactable.OnStateChanged(loadedState, State);
        }
        State = loadedState;
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

        try
        {
            string[] fileEntries = Directory.GetFiles(SaveDirectory);
            if (fileEntries.Length > 0)
            {
                if(StateNum<0)
                    StateNum = Int32.Parse(Path.GetFileName(fileEntries[fileEntries.Length - 1]));
                LoadState(StateNum);
            }
            else
            {
                State = new GameState(true);
                StateNum = 0;
                saveStateToFile();
            }
        }
        catch (DirectoryNotFoundException)
        {
            Directory.CreateDirectory(SaveDirectory);
            State = new GameState(true);
            StateNum = 0;
            saveStateToFile();
        }
    }

    private void OnSceneEnd(Scene scene)
    {
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
    /// Stops interactable from receiving notificatios about state changes
    /// </summary>
    /// <param name="interactable">Interactable to register</param>
    /// <returns>Current game state</returns>
    public void Unsubscribe(IInteractable interactable)
    {
        interactables.Remove(interactable);
    }

    public void Update()
    {
        if (PressToLoadState)
        {
            PressToLoadState = false;
            LoadState(StateNum);
        }
    }

    private GameState loadStateFromFile(int stateNum = -1)
    {
        if (stateNum < 0)
            stateNum = StateNum;
        using (Stream stream = File.Open(SaveDirectory + stateNumToFile(stateNum), FileMode.Open))
        {
            //var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            var formatter = new XmlSerializer(typeof(GameState));
            return (GameState)formatter.Deserialize(stream);
        }
    }

    private void saveStateToFile(int stateNum = -1)
    {
        if (stateNum < 0)
            stateNum = StateNum;
        //var formatter = new BinaryFormatter();
        var formatter = new XmlSerializer(typeof(GameState));

        FileStream file = File.Create(SaveDirectory + stateNumToFile());
        formatter.Serialize(file, State);
        file.Close();
        if (DebugLog)
            Debug.Log("State saved to " + stateNumToFile());
    }

    private string stateNumToFile(int stateNum = -1)
    {
        if (stateNum < 0)
            stateNum = StateNum;
        return stateNum.ToString().PadLeft(5, '0');
    }
}
