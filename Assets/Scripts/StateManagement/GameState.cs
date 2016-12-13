using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameState
{
    public AnnanaSceneState AnnanaHouse;
    public Vector3S CharacterPosition;

    // empty default constructor to allow serialization
    public GameState() { }

    public GameState(bool initial)
    {
        CharacterPosition = getCharacterPosition();
        AnnanaHouse = new AnnanaSceneState();
    }

    private GameState(GameState template)
    {
        CharacterPosition = getCharacterPosition();
        AnnanaHouse = template.AnnanaHouse;
    }

    public GameState Set(AnnanaSceneState state)
    {
        var copy = new GameState(this);
        copy.AnnanaHouse = state;
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

        return result;
    }

    private Vector3S getCharacterPosition()
    {
        return new Vector3S(GameObject.FindWithTag("Character").transform.position);
    }

}
