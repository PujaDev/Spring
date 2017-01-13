using System;
using System.Collections.Generic;

[Serializable]
public class AnnanaTeaPartySceneState : SceneState
{

	public bool DrankTea { get; private set; }
	public bool InTheKitchen { get; private set; }
	public bool IsHappy { get; private set; }
	public bool IsInside { get; private set; }
	public bool IsLockOpen { get; private set; }
	public bool IsOutside { get; private set; }
	public bool IsReadingTheFine { get; private set; }
	public bool OwlFlownAway { get; private set; }
	public HashSet<int> PickedUpItems { get; private set; }
	public bool TeaBagInTheCup { get; private set; }
	/// <summary>
	/// -1 - none, 0 - empty, 1 - cold water, 2 - hot water
	/// </summary>
	public int TeapotOnTheStove { get; private set; }
	public bool TeapotOnTheTable { get; private set; }
	public bool ThrewCup { get; private set; }
	public bool TookTheFine { get; private set; }
	public HashSet<int> UsedItems { get; private set; }
	/// <summary>
	/// -1 - none, 0 - tea, 1- cold, 2 - hot, 3 - nasty
	/// </summary>
	public int WaterInTheCup { get; private set; }

	// empty constructor - for deserialization
	public AnnanaTeaPartySceneState() {}

	// initial constructor - default values
	public AnnanaTeaPartySceneState(bool initial) {
		DrankTea = false;
		InTheKitchen = true;
		IsHappy = false;
		IsInside = false;
		IsLockOpen = false;
		IsOutside = false;
		IsReadingTheFine = false;
		OwlFlownAway = false;
		PickedUpItems = new HashSet<int>(){(int)AnnanaTeaPartyInventory.ItemIds.TeaBag};
		TeaBagInTheCup = false;
		TeapotOnTheStove = -1;
		TeapotOnTheTable = true;
		ThrewCup = false;
		TookTheFine = false;
		UsedItems = new HashSet<int>();
		WaterInTheCup = -1;
	}

	// copy constructor
	private AnnanaTeaPartySceneState(AnnanaTeaPartySceneState template) {
		DrankTea = template.DrankTea;
		InTheKitchen = template.InTheKitchen;
		IsHappy = template.IsHappy;
		IsInside = template.IsInside;
		IsLockOpen = template.IsLockOpen;
		IsOutside = template.IsOutside;
		IsReadingTheFine = template.IsReadingTheFine;
		OwlFlownAway = template.OwlFlownAway;
		PickedUpItems = template.PickedUpItems;
		TeaBagInTheCup = template.TeaBagInTheCup;
		TeapotOnTheStove = template.TeapotOnTheStove;
		TeapotOnTheTable = template.TeapotOnTheTable;
		ThrewCup = template.ThrewCup;
		TookTheFine = template.TookTheFine;
		UsedItems = template.UsedItems;
		WaterInTheCup = template.WaterInTheCup;
		SetCharacterPosition();
	}

	public AnnanaTeaPartySceneState SetDrankTea(bool value)
	{
		var copy = new AnnanaTeaPartySceneState(this);
		copy.DrankTea = value;
		return copy;
	}

	public AnnanaTeaPartySceneState SetInTheKitchen(bool value)
	{
		var copy = new AnnanaTeaPartySceneState(this);
		copy.InTheKitchen = value;
		return copy;
	}

	public AnnanaTeaPartySceneState SetIsHappy(bool value)
	{
		var copy = new AnnanaTeaPartySceneState(this);
		copy.IsHappy = value;
		return copy;
	}

	public AnnanaTeaPartySceneState SetIsInside(bool value)
	{
		var copy = new AnnanaTeaPartySceneState(this);
		copy.IsInside = value;
		return copy;
	}

	public AnnanaTeaPartySceneState SetIsLockOpen(bool value)
	{
		var copy = new AnnanaTeaPartySceneState(this);
		copy.IsLockOpen = value;
		return copy;
	}

	public AnnanaTeaPartySceneState SetIsOutside(bool value)
	{
		var copy = new AnnanaTeaPartySceneState(this);
		copy.IsOutside = value;
		return copy;
	}

	public AnnanaTeaPartySceneState SetIsReadingTheFine(bool value)
	{
		var copy = new AnnanaTeaPartySceneState(this);
		copy.IsReadingTheFine = value;
		return copy;
	}

	public AnnanaTeaPartySceneState SetOwlFlownAway(bool value)
	{
		var copy = new AnnanaTeaPartySceneState(this);
		copy.OwlFlownAway = value;
		return copy;
	}

	public AnnanaTeaPartySceneState SetPickedUpItems(HashSet<int> value)
	{
		var copy = new AnnanaTeaPartySceneState(this);
		copy.PickedUpItems = value;
		return copy;
	}

	public AnnanaTeaPartySceneState SetTeaBagInTheCup(bool value)
	{
		var copy = new AnnanaTeaPartySceneState(this);
		copy.TeaBagInTheCup = value;
		return copy;
	}

	public AnnanaTeaPartySceneState SetTeapotOnTheStove(int value)
	{
		var copy = new AnnanaTeaPartySceneState(this);
		copy.TeapotOnTheStove = value;
		return copy;
	}

	public AnnanaTeaPartySceneState SetTeapotOnTheTable(bool value)
	{
		var copy = new AnnanaTeaPartySceneState(this);
		copy.TeapotOnTheTable = value;
		return copy;
	}

	public AnnanaTeaPartySceneState SetThrewCup(bool value)
	{
		var copy = new AnnanaTeaPartySceneState(this);
		copy.ThrewCup = value;
		return copy;
	}

	public AnnanaTeaPartySceneState SetTookTheFine(bool value)
	{
		var copy = new AnnanaTeaPartySceneState(this);
		copy.TookTheFine = value;
		return copy;
	}

	public AnnanaTeaPartySceneState SetUsedItems(HashSet<int> value)
	{
		var copy = new AnnanaTeaPartySceneState(this);
		copy.UsedItems = value;
		return copy;
	}

	public AnnanaTeaPartySceneState SetWaterInTheCup(int value)
	{
		var copy = new AnnanaTeaPartySceneState(this);
		copy.WaterInTheCup = value;
		return copy;
	}


	// compare method
	public List<string> CompareChanges(AnnanaTeaPartySceneState other) {
		var result = new List<string>();

		if(!DrankTea.Equals(other.DrankTea))
			result.Add(String.Format("DrankTea:\t{0}\t>>>\t{1}",other.DrankTea,DrankTea));

		if(!InTheKitchen.Equals(other.InTheKitchen))
			result.Add(String.Format("InTheKitchen:\t{0}\t>>>\t{1}",other.InTheKitchen,InTheKitchen));

		if(!IsHappy.Equals(other.IsHappy))
			result.Add(String.Format("IsHappy:\t{0}\t>>>\t{1}",other.IsHappy,IsHappy));

		if(!IsInside.Equals(other.IsInside))
			result.Add(String.Format("IsInside:\t{0}\t>>>\t{1}",other.IsInside,IsInside));

		if(!IsLockOpen.Equals(other.IsLockOpen))
			result.Add(String.Format("IsLockOpen:\t{0}\t>>>\t{1}",other.IsLockOpen,IsLockOpen));

		if(!IsOutside.Equals(other.IsOutside))
			result.Add(String.Format("IsOutside:\t{0}\t>>>\t{1}",other.IsOutside,IsOutside));

		if(!IsReadingTheFine.Equals(other.IsReadingTheFine))
			result.Add(String.Format("IsReadingTheFine:\t{0}\t>>>\t{1}",other.IsReadingTheFine,IsReadingTheFine));

		if(!OwlFlownAway.Equals(other.OwlFlownAway))
			result.Add(String.Format("OwlFlownAway:\t{0}\t>>>\t{1}",other.OwlFlownAway,OwlFlownAway));

		if(!PickedUpItems.Equals(other.PickedUpItems))
			result.Add(String.Format("PickedUpItems:\t{0}\t>>>\t{1}",other.PickedUpItems,PickedUpItems));

		if(!TeaBagInTheCup.Equals(other.TeaBagInTheCup))
			result.Add(String.Format("TeaBagInTheCup:\t{0}\t>>>\t{1}",other.TeaBagInTheCup,TeaBagInTheCup));

		if(!TeapotOnTheStove.Equals(other.TeapotOnTheStove))
			result.Add(String.Format("TeapotOnTheStove:\t{0}\t>>>\t{1}",other.TeapotOnTheStove,TeapotOnTheStove));

		if(!TeapotOnTheTable.Equals(other.TeapotOnTheTable))
			result.Add(String.Format("TeapotOnTheTable:\t{0}\t>>>\t{1}",other.TeapotOnTheTable,TeapotOnTheTable));

		if(!ThrewCup.Equals(other.ThrewCup))
			result.Add(String.Format("ThrewCup:\t{0}\t>>>\t{1}",other.ThrewCup,ThrewCup));

		if(!TookTheFine.Equals(other.TookTheFine))
			result.Add(String.Format("TookTheFine:\t{0}\t>>>\t{1}",other.TookTheFine,TookTheFine));

		if(!UsedItems.Equals(other.UsedItems))
			result.Add(String.Format("UsedItems:\t{0}\t>>>\t{1}",other.UsedItems,UsedItems));

		if(!WaterInTheCup.Equals(other.WaterInTheCup))
			result.Add(String.Format("WaterInTheCup:\t{0}\t>>>\t{1}",other.WaterInTheCup,WaterInTheCup));

		return result;
	}
}
