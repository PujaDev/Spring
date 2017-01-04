﻿using System;
using System.Collections.Generic;

[Serializable]
public class HubaBusSceneState : SceneState
{

    public int AngerLevel { get; private set; }
    public Vector3S CharPosition { get; private set; }
    /// <summary>
    /// Bus is waiting at the busstop
    /// </summary>
    public bool isBusWaiting { get; private set; }
    /// <summary>
    /// Bus already arrived and left
    /// </summary>
    public bool hasBusLeft { get; private set; }
    public HashSet<int> PickedUpItems { get; private set; }
    public HashSet<int> UsedItems { get; private set; }
    /// <summary>
    /// Package is delivered on the ground
    /// </summary>
    public bool isDelivered { get; private set; }
    /// <summary>
    /// Huba drank the elixir
    /// </summary>
    public bool isDrunk { get; private set; }

    public HubaBusSceneState()
    {
        AngerLevel = 13;
        CharPosition = new Vector3S(-11.5f, -1.4f, 0f);
        isBusWaiting = false;
        hasBusLeft = false;
        PickedUpItems = new HashSet<int>() { (int)HubaBusInventory.ItemIds.GoldCoins, (int)HubaBusInventory.ItemIds.SilverCoin };
        UsedItems = new HashSet<int>();
        isDelivered = false;
        isDrunk = false;
    }

    private HubaBusSceneState(HubaBusSceneState template)
    {
        AngerLevel = template.AngerLevel;
        CharPosition = template.CharPosition;
        isBusWaiting = template.isBusWaiting;
        hasBusLeft = template.hasBusLeft;
        PickedUpItems = template.PickedUpItems;
        UsedItems = template.UsedItems;
        isDelivered = template.isDelivered;
        isDrunk = template.isDrunk;
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

    public HubaBusSceneState SetisBusWaiting(bool value)
    {
        var copy = new HubaBusSceneState(this);
        copy.isBusWaiting = value;
        return copy;
    }

    public HubaBusSceneState SethasBusLeft(bool value)
    {
        var copy = new HubaBusSceneState(this);
        copy.hasBusLeft = value;
        return copy;
    }

    public HubaBusSceneState SetPickedUpItems(HashSet<int> value)
    {
        var copy = new HubaBusSceneState(this);
        copy.PickedUpItems = value;
        return copy;
    }

    public HubaBusSceneState SetUsedItems(HashSet<int> value)
    {
        var copy = new HubaBusSceneState(this);
        copy.UsedItems = value;
        return copy;
    }

    public HubaBusSceneState SetisDelivered(bool value)
    {
        var copy = new HubaBusSceneState(this);
        copy.isDelivered = value;
        return copy;
    }

    public HubaBusSceneState SetisDrunk(bool value)
    {
        var copy = new HubaBusSceneState(this);
        copy.isDrunk = value;
        return copy;
    }


    public List<string> CompareChanges(HubaBusSceneState other)
    {
        var result = new List<string>();

        if (!AngerLevel.Equals(other.AngerLevel))
            result.Add(String.Format("AngerLevel:\t{0}\t>>>\t{1}", other.AngerLevel, AngerLevel));

        if (!CharPosition.Equals(other.CharPosition))
            result.Add(String.Format("CharPosition:\t{0}\t>>>\t{1}", other.CharPosition, CharPosition));

        if (!isBusWaiting.Equals(other.isBusWaiting))
            result.Add(String.Format("isBusWaiting:\t{0}\t>>>\t{1}", other.isBusWaiting, isBusWaiting));

        if (!hasBusLeft.Equals(other.hasBusLeft))
            result.Add(String.Format("hasBusLeft:\t{0}\t>>>\t{1}", other.hasBusLeft, hasBusLeft));

        if (!PickedUpItems.Equals(other.PickedUpItems))
            result.Add(String.Format("PickedUpItems:\t{0}\t>>>\t{1}", other.PickedUpItems, PickedUpItems));

        if (!UsedItems.Equals(other.UsedItems))
            result.Add(String.Format("UsedItems:\t{0}\t>>>\t{1}", other.UsedItems, UsedItems));

        if (!isDelivered.Equals(other.isDelivered))
            result.Add(String.Format("isDelivered:\t{0}\t>>>\t{1}", other.isDelivered, isDelivered));

        if (!isDrunk.Equals(other.isDrunk))
            result.Add(String.Format("isDrunk:\t{0}\t>>>\t{1}", other.isDrunk, isDrunk));

        return result;
    }
}
