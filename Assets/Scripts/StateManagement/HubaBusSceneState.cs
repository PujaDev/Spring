using System;
using System.Collections.Generic;

[Serializable]
public class HubaBusSceneState : SceneState
{

    public int AngerLevel { get; private set; }
    public bool askedForTicket { get; private set; }
    public bool getOnTheBus { get; private set; }
    /// <summary>
    /// Bus already arrived and left
    /// </summary>
    public bool hasBusLeft { get; private set; }
    public Vector3S CharPosition { get; private set; }
    /// <summary>
    /// Bus is waiting at the busstop
    /// </summary>
    public bool isBusWaiting { get; private set; }
    /// <summary>
    /// Package is delivered on the ground
    /// </summary>
    public bool isDelivered { get; private set; }
    /// <summary>
    /// Huba drank the elixir
    /// </summary>
    public bool isDrunk { get; private set; }
    public bool isInTheBus { get; private set; }
    /// <summary>
    /// package is not there anymore
    /// </summary>
    public bool isOpened { get; private set; }
    public bool isOutOfTheHouse { get; private set; }
    public bool isPaid { get; private set; }
    public HashSet<int> PickedUpItems { get; private set; }
    public HashSet<int> UsedItems { get; private set; }

    // empty constructor - for deserialization
    public HubaBusSceneState() { }

    // initial constructor - default values
    public HubaBusSceneState(bool initial)
    {
        AngerLevel = 13;
        askedForTicket = false;
        getOnTheBus = false;
        hasBusLeft = false;
        CharPosition = new Vector3S(-11.5f, -1.4f, 0f);
        isBusWaiting = false;
        isDelivered = false;
        isDrunk = false;
        isInTheBus = false;
        isOpened = false;
        isOutOfTheHouse = false;
        isPaid = false;
        PickedUpItems = new HashSet<int>() { (int)HubaBusInventory.ItemIds.GoldCoins, (int)HubaBusInventory.ItemIds.SilverCoin };
        UsedItems = new HashSet<int>();
    }

    // copy constructor
    private HubaBusSceneState(HubaBusSceneState template)
    {
        AngerLevel = template.AngerLevel;
        askedForTicket = template.askedForTicket;
        getOnTheBus = template.getOnTheBus;
        hasBusLeft = template.hasBusLeft;
        CharPosition = template.CharPosition;
        isBusWaiting = template.isBusWaiting;
        isDelivered = template.isDelivered;
        isDrunk = template.isDrunk;
        isInTheBus = template.isInTheBus;
        isOpened = template.isOpened;
        isOutOfTheHouse = template.isOutOfTheHouse;
        isPaid = template.isPaid;
        PickedUpItems = template.PickedUpItems;
        UsedItems = template.UsedItems;
        SetCharacterPosition();
    }

    public HubaBusSceneState SetAngerLevel(int value)
    {
        var copy = new HubaBusSceneState(this);
        copy.AngerLevel = value;
        return copy;
    }

    public HubaBusSceneState SetaskedForTicket(bool value)
    {
        var copy = new HubaBusSceneState(this);
        copy.askedForTicket = value;
        return copy;
    }

    public HubaBusSceneState SetgetOnTheBus(bool value)
    {
        var copy = new HubaBusSceneState(this);
        copy.getOnTheBus = value;
        return copy;
    }

    public HubaBusSceneState SethasBusLeft(bool value)
    {
        var copy = new HubaBusSceneState(this);
        copy.hasBusLeft = value;
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

    public HubaBusSceneState SetisInTheBus(bool value)
    {
        var copy = new HubaBusSceneState(this);
        copy.isInTheBus = value;
        return copy;
    }

    public HubaBusSceneState SetisOpened(bool value)
    {
        var copy = new HubaBusSceneState(this);
        copy.isOpened = value;
        return copy;
    }

    public HubaBusSceneState SetisOutOfTheHouse(bool value)
    {
        var copy = new HubaBusSceneState(this);
        copy.isOutOfTheHouse = value;
        return copy;
    }

    public HubaBusSceneState SetisPaid(bool value)
    {
        var copy = new HubaBusSceneState(this);
        copy.isPaid = value;
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


    // compare method
    public List<string> CompareChanges(HubaBusSceneState other)
    {
        var result = new List<string>();

        if (!AngerLevel.Equals(other.AngerLevel))
            result.Add(String.Format("AngerLevel:\t{0}\t>>>\t{1}", other.AngerLevel, AngerLevel));

        if (!askedForTicket.Equals(other.askedForTicket))
            result.Add(String.Format("askedForTicket:\t{0}\t>>>\t{1}", other.askedForTicket, askedForTicket));

        if (!getOnTheBus.Equals(other.getOnTheBus))
            result.Add(String.Format("getOnTheBus:\t{0}\t>>>\t{1}", other.getOnTheBus, getOnTheBus));

        if (!hasBusLeft.Equals(other.hasBusLeft))
            result.Add(String.Format("hasBusLeft:\t{0}\t>>>\t{1}", other.hasBusLeft, hasBusLeft));

        if (!CharPosition.Equals(other.CharPosition))
            result.Add(String.Format("CharPosition:\t{0}\t>>>\t{1}", other.CharPosition, CharPosition));

        if (!isBusWaiting.Equals(other.isBusWaiting))
            result.Add(String.Format("isBusWaiting:\t{0}\t>>>\t{1}", other.isBusWaiting, isBusWaiting));

        if (!isDelivered.Equals(other.isDelivered))
            result.Add(String.Format("isDelivered:\t{0}\t>>>\t{1}", other.isDelivered, isDelivered));

        if (!isDrunk.Equals(other.isDrunk))
            result.Add(String.Format("isDrunk:\t{0}\t>>>\t{1}", other.isDrunk, isDrunk));

        if (!isInTheBus.Equals(other.isInTheBus))
            result.Add(String.Format("isInTheBus:\t{0}\t>>>\t{1}", other.isInTheBus, isInTheBus));

        if (!isOpened.Equals(other.isOpened))
            result.Add(String.Format("isOpened:\t{0}\t>>>\t{1}", other.isOpened, isOpened));

        if (!isOutOfTheHouse.Equals(other.isOutOfTheHouse))
            result.Add(String.Format("isOutOfTheHouse:\t{0}\t>>>\t{1}", other.isOutOfTheHouse, isOutOfTheHouse));

        if (!isPaid.Equals(other.isPaid))
            result.Add(String.Format("isPaid:\t{0}\t>>>\t{1}", other.isPaid, isPaid));

        if (!PickedUpItems.Equals(other.PickedUpItems))
            result.Add(String.Format("PickedUpItems:\t{0}\t>>>\t{1}", other.PickedUpItems, PickedUpItems));

        if (!UsedItems.Equals(other.UsedItems))
            result.Add(String.Format("UsedItems:\t{0}\t>>>\t{1}", other.UsedItems, UsedItems));

        return result;
    }
}
