using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class GameState
{
    public AnnanaSceneState AnnanaHouse = null;
    public HubaBusSceneState HubaBus = null;
    public HubaForestSceneState HubaForest = null;
    // empty default constructor to allow deserialization
    private GameState() {
    }

    public GameState(bool initial)
    {
        AnnanaHouse = new AnnanaSceneState(initial);
        HubaBus = new HubaBusSceneState(initial);
        HubaForest = new HubaForestSceneState(initial);
    }

    private GameState(GameState template)
    {
        AnnanaHouse = template.AnnanaHouse;
        HubaBus = template.HubaBus;
        HubaForest = template.HubaForest;
    }

    public GameState Set(SceneState state)
    {
        throw new Exception();
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
            case "Scena_1_AnnanaHouse":
                return AnnanaHouse;
            case "Scena_2_HubaForest":
                return HubaBus;
            case "Scena_4_SilentForest":
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
        return new SceneState[] {AnnanaHouse, HubaBus, HubaForest };
    }
    
    public GameState Reset(string SceneName)
    {
        switch (SceneName)
        {
            case "Scena_1_AnnanaHouse":
                return Set(new AnnanaSceneState(true));
            case "Scena_2_HubaForest":
                return Set(new HubaBusSceneState(true));
            case "Scena_4_SilentForest":
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
           result["AnnanaHouse"] = diff;

        var diff2 = HubaBus.CompareChanges(other.HubaBus);
        if (diff2.Count > 0)
            result["HubaBus"] = diff2;

        var diff3 = HubaForest.CompareChanges(other.HubaForest);
        if (diff3.Count > 0)
            result["HubaForest"] = diff3;

        return result;
    }
}
