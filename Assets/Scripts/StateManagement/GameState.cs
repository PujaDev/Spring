﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class GameState
{
    public AnnanaSceneState AnnanaHouse = null;
    public HubaBusSceneState HubaBus = null;
    public AnnanaTeaPartySceneState AnnanaTeaParty = null;
    public HubaForestSceneState HubaForest = null;
    // empty default constructor to allow deserialization
    private GameState() {
    }

    // this constructor uses parameter just to differ from empty one
    public GameState(bool initial)
    {
        AnnanaHouse = new AnnanaSceneState(initial);
        AnnanaTeaParty = new AnnanaTeaPartySceneState(initial);
        HubaBus = new HubaBusSceneState(initial);
        HubaForest = new HubaForestSceneState(initial);
    }

    private GameState(GameState template)
    {
        AnnanaHouse = template.AnnanaHouse;
        HubaBus = template.HubaBus;
        AnnanaTeaParty = template.AnnanaTeaParty;
        HubaForest = template.HubaForest;
    }

    public GameState Set(AnnanaSceneState state)
    {
        var copy = new GameState(this);
        copy.AnnanaHouse = state;
        return copy;
    }

    public GameState Set(HubaBusSceneState state)
    {
        var copy = new GameState(this);
        copy.HubaBus = state;
        return copy;
    }

    public GameState Set(AnnanaTeaPartySceneState state)
    {
        var copy = new GameState(this);
        copy.AnnanaTeaParty = state;
        return copy;
    }

    public GameState Set(HubaForestSceneState state)
    {
        var copy = new GameState(this);
        copy.HubaForest = state;
        return copy;
    }

    public SceneState GetSceneState(string SceneName)
    {
        switch (SceneName)
        {
            case SceneState.ANNANA_HOUSE_NAME:
                return AnnanaHouse;
            case SceneState.HUBA_FOREST_NAME:
                return HubaBus;
            case SceneState.ANNANA_TEA_PARTY_NAME:
                return AnnanaTeaParty;
            case SceneState.SILENT_FOREST_NAME:
                return HubaForest;
        }
        return null;
    }

    public SceneState GetCurrentSceneState()
    {
        return GetSceneState(SceneManager.GetActiveScene().name);
    }

    public SceneState[] GetScenes()
    {
        return new SceneState[] {AnnanaHouse, HubaBus, AnnanaTeaParty, HubaForest };
    }
    
    public GameState Reset(string SceneName)
    {
        switch (SceneName)
        {
            case SceneState.ANNANA_HOUSE_NAME:
                return Set(new AnnanaSceneState(true));
            case SceneState.HUBA_FOREST_NAME:
                return Set(new HubaBusSceneState(true));
            case SceneState.ANNANA_TEA_PARTY_NAME:
                return Set(new AnnanaTeaPartySceneState(true));
            case SceneState.SILENT_FOREST_NAME:
                return Set(new HubaForestSceneState(true));
        }
        return null;
    }

    public GameState ReturnTo(string SceneName)
    {
        var presentState = GetSceneState(SceneName);
        var newState = new GameState(this);

        foreach(var otherState in GetScenes())
        {
            if (presentState.TimeRange < otherState.TimeRange)
                newState = newState.Reset(otherState.SceneName);
        }

        return newState;
    }

    public Dictionary<string, List<string>> CompareChanges(GameState other)
    {
        var result = new Dictionary<string, List<string>>();

        if (this == other)
            return result;

        var diff = AnnanaHouse.CompareChanges(other.AnnanaHouse);
        if (diff.Count > 0)
           result["Annana House"] = diff;

        var diff2 = HubaBus.CompareChanges(other.HubaBus);
        if (diff2.Count > 0)
            result["Huba Bus"] = diff2;

        var diff3 = AnnanaTeaParty.CompareChanges(other.AnnanaTeaParty);
        if (diff3.Count > 0)
            result["Annana Tea Party"] = diff3;

        var diff4 = HubaForest.CompareChanges(other.HubaForest);
        if (diff4.Count > 0)
            result["Huba Forest"] = diff4;

        return result;
    }
}
