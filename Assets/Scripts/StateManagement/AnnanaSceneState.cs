using System;
using System.Collections.Generic;

[Serializable]
public class AnnanaSceneState : SceneState
{

    public bool AlarmTurnedOff { get; private set; }
    public bool AlarmPostponed { get; private set; }
    public bool ChangeClothes { get; private set; }
    public bool ReadingVeganBook { get; private set; }
    public bool FlyAway { get; private set; }
    /// <summary>
    /// Name of the chosen elixir
    /// </summary>
    public string ElixirName { get; private set; }
    public int AngerLevel { get; private set; }

    public AnnanaSceneState()
    {
        AlarmTurnedOff = false;
        AlarmPostponed = false;
        ChangeClothes = false;
        ReadingVeganBook = false;
        FlyAway = false;
        ElixirName = "";
        AngerLevel = 13;
    }

    private AnnanaSceneState(AnnanaSceneState template)
    {
        AlarmTurnedOff = template.AlarmTurnedOff;
        AlarmPostponed = template.AlarmPostponed;
        ChangeClothes = template.ChangeClothes;
        ReadingVeganBook = template.ReadingVeganBook;
        FlyAway = template.FlyAway;
        ElixirName = template.ElixirName;
        AngerLevel = template.AngerLevel;
    }

    public AnnanaSceneState SetAlarmTurnedOff(bool value)
    {
        var copy = new AnnanaSceneState(this);
        copy.AlarmTurnedOff = value;
        return copy;
    }

    public AnnanaSceneState SetAlarmPostponed(bool value)
    {
        var copy = new AnnanaSceneState(this);
        copy.AlarmPostponed = value;
        return copy;
    }

    public AnnanaSceneState SetChangeClothes(bool value)
    {
        var copy = new AnnanaSceneState(this);
        copy.ChangeClothes = value;
        return copy;
    }

    public AnnanaSceneState SetReadingVeganBook(bool value)
    {
        var copy = new AnnanaSceneState(this);
        copy.ReadingVeganBook = value;
        return copy;
    }

    public AnnanaSceneState SetFlyAway(bool value)
    {
        var copy = new AnnanaSceneState(this);
        copy.FlyAway = value;
        return copy;
    }

    public AnnanaSceneState SetElixirName(string value)
    {
        var copy = new AnnanaSceneState(this);
        copy.ElixirName = value;
        return copy;
    }

    public AnnanaSceneState SetAngerLevel(int value)
    {
        var copy = new AnnanaSceneState(this);
        copy.AngerLevel = value;
        return copy;
    }


    public List<string> CompareChanges(AnnanaSceneState other)
    {
        var result = new List<string>();

        if (!AlarmTurnedOff.Equals(other.AlarmTurnedOff))
            result.Add(String.Format("AlarmTurnedOff:\t{0}\t>>>\t{1}", other.AlarmTurnedOff, AlarmTurnedOff));

        if (!AlarmPostponed.Equals(other.AlarmPostponed))
            result.Add(String.Format("AlarmPostponed:\t{0}\t>>>\t{1}", other.AlarmPostponed, AlarmPostponed));

        if (!ChangeClothes.Equals(other.ChangeClothes))
            result.Add(String.Format("ChangeClothes:\t{0}\t>>>\t{1}", other.ChangeClothes, ChangeClothes));

        if (!ReadingVeganBook.Equals(other.ReadingVeganBook))
            result.Add(String.Format("ReadingVeganBook:\t{0}\t>>>\t{1}", other.ReadingVeganBook, ReadingVeganBook));

        if (!FlyAway.Equals(other.FlyAway))
            result.Add(String.Format("FlyAway:\t{0}\t>>>\t{1}", other.FlyAway, FlyAway));

        if (!ElixirName.Equals(other.ElixirName))
            result.Add(String.Format("ElixirName:\t{0}\t>>>\t{1}", other.ElixirName, ElixirName));

        if (!AngerLevel.Equals(other.AngerLevel))
            result.Add(String.Format("AngerLevel:\t{0}\t>>>\t{1}", other.AngerLevel, AngerLevel));

        return result;
    }
}
