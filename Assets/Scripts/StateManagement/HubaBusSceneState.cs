using System;
using System.Collections.Generic;

[Serializable]
public class HubaBusSceneState : SceneState
{

    public int AngerLevel { get; private set; }
    public Vector3S CharPosition { get; private set; }

    public HubaBusSceneState()
    {
        AngerLevel = 13;
        CharPosition = new Vector3S(-11.5f, -1.4f, 0f);
    }

    private HubaBusSceneState(HubaBusSceneState template)
    {
        AngerLevel = template.AngerLevel;
        CharPosition = template.CharPosition;
    }

    public HubaBusSceneState SetAngerLevel(int value)
    {
        var copy = new HubaBusSceneState(this);
        copy.AngerLevel = value;
        return copy;
    }

    public HubaBusSceneState SetCharPosition(Vector3S value)
    {
        var copy = new HubaBusSceneState(this);
        copy.CharPosition = value;
        return copy;
    }


    public List<string> CompareChanges(HubaBusSceneState other)
    {
        var result = new List<string>();

        if (!AngerLevel.Equals(other.AngerLevel))
            result.Add(String.Format("AngerLevel:\t{0}\t>>>\t{1}", other.AngerLevel, AngerLevel));

        if (!CharPosition.Equals(other.CharPosition))
            result.Add(String.Format("CharPosition:\t{0}\t>>>\t{1}", other.CharPosition, CharPosition));

        return result;
    }
}
