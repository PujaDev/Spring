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
    public bool IsCrystalBallPickedUp { get; private set; }
    public bool IsInside { get; private set; }
    public bool IsOutside { get; private set; }
    public Vector3S CharPosition { get; private set; }
    public int EmptyVialPickedUpCount { get; private set; }

    public AnnanaSceneState()
    {
        AlarmTurnedOff = false;
        AlarmPostponed = false;
        ChangeClothes = false;
        ReadingVeganBook = false;
        FlyAway = false;
        ElixirName = "";
        AngerLevel = 13;
        IsCrystalBallPickedUp = false;
        IsInside = false;
        IsOutside = false;
        CharPosition = new Vector3S(10.74f, -0.52f, 0f);
        EmptyVialPickedUpCount = 0;
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
        IsCrystalBallPickedUp = template.IsCrystalBallPickedUp;
        IsInside = template.IsInside;
        IsOutside = template.IsOutside;
        CharPosition = template.CharPosition;
        EmptyVialPickedUpCount = template.EmptyVialPickedUpCount;
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

    public AnnanaSceneState SetIsCrystalBallPickedUp(bool value)
    {
        var copy = new AnnanaSceneState(this);
        copy.IsCrystalBallPickedUp = value;
        return copy;
    }

    public AnnanaSceneState SetIsInside(bool value)
    {
        var copy = new AnnanaSceneState(this);
        copy.IsInside = value;
        return copy;
    }

    public AnnanaSceneState SetIsOutside(bool value)
    {
        var copy = new AnnanaSceneState(this);
        copy.IsOutside = value;
        return copy;
    }

    public AnnanaSceneState SetCharPosition(Vector3S value)
    {
        var copy = new AnnanaSceneState(this);
        copy.CharPosition = value;
        return copy;
    }


    public AnnanaSceneState SetEmptyVialPickedUpCount(int value)
    {
        var copy = new AnnanaSceneState(this);
        copy.EmptyVialPickedUpCount = value;
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

        if (!IsCrystalBallPickedUp.Equals(other.IsCrystalBallPickedUp))
            result.Add(String.Format("IsCrystalBallPickedUp:\t{0}\t>>>\t{1}", other.IsCrystalBallPickedUp, IsCrystalBallPickedUp));

        if (!IsInside.Equals(other.IsInside))
            result.Add(String.Format("IsInside:\t{0}\t>>>\t{1}", other.IsInside, IsInside));

        if (!IsOutside.Equals(other.IsOutside))
            result.Add(String.Format("IsOutside:\t{0}\t>>>\t{1}", other.IsOutside, IsOutside));

        if (!CharPosition.Equals(other.CharPosition))
            result.Add(String.Format("CharPosition:\t{0}\t>>>\t{1}", other.CharPosition, CharPosition));

        if (!EmptyVialPickedUpCount.Equals(other.EmptyVialPickedUpCount))
            result.Add(String.Format("EmptyVialPickedUpCount:\t{0}\t>>>\t{1}", other.EmptyVialPickedUpCount, EmptyVialPickedUpCount));

        return result;
    }
}
