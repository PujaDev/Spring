using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameState
{
    public AnnanaSceneState AnnanaHouse;
    public HubaBusSceneState HubaBus;

    // empty default constructor to allow serialization
    public GameState() { }

    public GameState(bool initial)
    {
        AnnanaHouse = new AnnanaSceneState();
        HubaBus = new HubaBusSceneState();
    }

    private GameState(GameState template)
    {
        AnnanaHouse = template.AnnanaHouse;
        HubaBus = template.HubaBus;
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

        return result;
    }
}
