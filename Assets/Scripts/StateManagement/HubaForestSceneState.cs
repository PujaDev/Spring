using System;
using System.Collections.Generic;

[Serializable]
public class HubaForestSceneState : SceneState
{

	public int AngerLevel { get; private set; }
	public Vector3S CharPosition { get; private set; }
	/// <summary>
	/// Blessed = coin given to the shrine
	/// </summary>
	public bool IsHubaBlessed { get; private set; }
	public HashSet<int> PickedUpItems { get; private set; }
	public HashSet<int> UsedItems { get; private set; }
	public bool IsReadingMap { get; private set; }
	public List<int> CorrectForestWay { get; private set; }
	public List<int> CurrentForestWay { get; private set; }
	public bool IsInForest { get; private set; }
	public bool IsOnSite { get; private set; }

	public HubaForestSceneState() {
		AngerLevel = 13;
		CharPosition = new Vector3S(-21.03f, -3.88f, 0f);
		IsHubaBlessed = false;
		PickedUpItems = new HashSet<int>() {(int)HubaForestInventory.ItemIds.Coin};
		UsedItems = new HashSet<int>();
		IsReadingMap = false;
		CorrectForestWay = ForestSSC.GenerateNewPath(1);
		CurrentForestWay = new List<int>();
		IsInForest = false;
		IsOnSite = false;
	}

	private HubaForestSceneState(HubaForestSceneState template) {
		AngerLevel = template.AngerLevel;
		CharPosition = template.CharPosition;
		IsHubaBlessed = template.IsHubaBlessed;
		PickedUpItems = template.PickedUpItems;
		UsedItems = template.UsedItems;
		IsReadingMap = template.IsReadingMap;
		CorrectForestWay = template.CorrectForestWay;
		CurrentForestWay = template.CurrentForestWay;
		IsInForest = template.IsInForest;
		IsOnSite = template.IsOnSite;
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

	public HubaForestSceneState SetIsHubaBlessed(bool value)
	{
		var copy = new HubaForestSceneState(this);
		copy.IsHubaBlessed = value;
		return copy;
	}

	public HubaForestSceneState SetPickedUpItems(HashSet<int> value)
	{
		var copy = new HubaForestSceneState(this);
		copy.PickedUpItems = value;
		return copy;
	}

	public HubaForestSceneState SetUsedItems(HashSet<int> value)
	{
		var copy = new HubaForestSceneState(this);
		copy.UsedItems = value;
		return copy;
	}

	public HubaForestSceneState SetIsReadingMap(bool value)
	{
		var copy = new HubaForestSceneState(this);
		copy.IsReadingMap = value;
		return copy;
	}

	public HubaForestSceneState SetCorrectForestWay(List<int> value)
	{
		var copy = new HubaForestSceneState(this);
		copy.CorrectForestWay = value;
		return copy;
	}

	public HubaForestSceneState SetCurrentForestWay(List<int> value)
	{
		var copy = new HubaForestSceneState(this);
		copy.CurrentForestWay = value;
		return copy;
	}

	public HubaForestSceneState SetIsInForest(bool value)
	{
		var copy = new HubaForestSceneState(this);
		copy.IsInForest = value;
		return copy;
	}

	public HubaForestSceneState SetIsOnSite(bool value)
	{
		var copy = new HubaForestSceneState(this);
		copy.IsOnSite = value;
		return copy;
	}


	public List<string> CompareChanges(HubaForestSceneState other) {
		var result = new List<string>();

		if(!AngerLevel.Equals(other.AngerLevel))
			result.Add(String.Format("AngerLevel:\t{0}\t>>>\t{1}",other.AngerLevel,AngerLevel));

		if(!CharPosition.Equals(other.CharPosition))
			result.Add(String.Format("CharPosition:\t{0}\t>>>\t{1}",other.CharPosition,CharPosition));

		if(!IsHubaBlessed.Equals(other.IsHubaBlessed))
			result.Add(String.Format("IsHubaBlessed:\t{0}\t>>>\t{1}",other.IsHubaBlessed,IsHubaBlessed));

		if(!PickedUpItems.Equals(other.PickedUpItems))
			result.Add(String.Format("PickedUpItems:\t{0}\t>>>\t{1}",other.PickedUpItems,PickedUpItems));

		if(!UsedItems.Equals(other.UsedItems))
			result.Add(String.Format("UsedItems:\t{0}\t>>>\t{1}",other.UsedItems,UsedItems));

		if(!IsReadingMap.Equals(other.IsReadingMap))
			result.Add(String.Format("IsReadingMap:\t{0}\t>>>\t{1}",other.IsReadingMap,IsReadingMap));

		if(!CorrectForestWay.Equals(other.CorrectForestWay))
			result.Add(String.Format("CorrectForestWay:\t{0}\t>>>\t{1}",other.CorrectForestWay,CorrectForestWay));

		if(!CurrentForestWay.Equals(other.CurrentForestWay))
			result.Add(String.Format("CurrentForestWay:\t{0}\t>>>\t{1}",other.CurrentForestWay,CurrentForestWay));

		if(!IsInForest.Equals(other.IsInForest))
			result.Add(String.Format("IsInForest:\t{0}\t>>>\t{1}",other.IsInForest,IsInForest));

		if(!IsOnSite.Equals(other.IsOnSite))
			result.Add(String.Format("IsOnSite:\t{0}\t>>>\t{1}",other.IsOnSite,IsOnSite));

		return result;
	}
}
