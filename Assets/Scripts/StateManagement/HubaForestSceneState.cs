using System;
using System.Collections.Generic;

[Serializable]
public class HubaForestSceneState : SceneState
{

    public int AngerLevel { get; private set; }
    public Vector3S CharPosition { get; private set; }

    public HubaForestSceneState()
    {
        AngerLevel = 13;
        CharPosition = new Vector3S(-21.03f, -3.88f, 0f);
    }

    private HubaForestSceneState(HubaForestSceneState template)
    {
        AngerLevel = template.AngerLevel;
        CharPosition = template.CharPosition;
    }

    public HubaForestSceneState SetAngerLevel(int value)
    {
        var copy = new HubaForestSceneState(this);
        copy.AngerLevel = value;
        return copy;
    }

    public HubaForestSceneState SetCharPosition(Vector3S value)
    {
        var copy = new HubaForestSceneState(this);
        copy.CharPosition = value;
        return copy;
    }


    public List<string> CompareChanges(HubaForestSceneState other)
    {
        var result = new List<string>();

        if (!AngerLevel.Equals(other.AngerLevel))
            result.Add(String.Format("AngerLevel:\t{0}\t>>>\t{1}", other.AngerLevel, AngerLevel));

        if (!CharPosition.Equals(other.CharPosition))
            result.Add(String.Format("CharPosition:\t{0}\t>>>\t{1}", other.CharPosition, CharPosition));

        return result;
    }
}
