using System;
using System.Collections.Generic;

[Serializable]
public class HubaForestSceneState : SceneState
{

	public int AngerLevel { get; private set; }
	public Vector3S CharPosition { get; private set; }
	public bool IsCoinPickedUp { get; private set; }
	public bool IsCoinUsed { get; private set; }
	/// <summary>
	/// Blessd = coin given to the shrine
	/// </summary>
	public bool IsHubaBlessed { get; private set; }

	public HubaForestSceneState() {
		AngerLevel = 13;
		CharPosition = new Vector3S(-21.03f, -3.88f, 0f);
		IsCoinPickedUp = true;
		IsCoinUsed = false;
		IsHubaBlessed = false;
	}

	private HubaForestSceneState(HubaForestSceneState template) {
		AngerLevel = template.AngerLevel;
		CharPosition = template.CharPosition;
		IsCoinPickedUp = template.IsCoinPickedUp;
		IsCoinUsed = template.IsCoinUsed;
		IsHubaBlessed = template.IsHubaBlessed;
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

	public HubaForestSceneState SetIsCoinPickedUp(bool value)
	{
		var copy = new HubaForestSceneState(this);
		copy.IsCoinPickedUp = value;
		return copy;
	}

	public HubaForestSceneState SetIsCoinUsed(bool value)
	{
		var copy = new HubaForestSceneState(this);
		copy.IsCoinUsed = value;
		return copy;
	}

	public HubaForestSceneState SetIsHubaBlessed(bool value)
	{
		var copy = new HubaForestSceneState(this);
		copy.IsHubaBlessed = value;
		return copy;
	}


	public List<string> CompareChanges(HubaForestSceneState other) {
		var result = new List<string>();

		if(!AngerLevel.Equals(other.AngerLevel))
			result.Add(String.Format("AngerLevel:\t{0}\t>>>\t{1}",other.AngerLevel,AngerLevel));

		if(!CharPosition.Equals(other.CharPosition))
			result.Add(String.Format("CharPosition:\t{0}\t>>>\t{1}",other.CharPosition,CharPosition));

		if(!IsCoinPickedUp.Equals(other.IsCoinPickedUp))
			result.Add(String.Format("IsCoinPickedUp:\t{0}\t>>>\t{1}",other.IsCoinPickedUp,IsCoinPickedUp));

		if(!IsCoinUsed.Equals(other.IsCoinUsed))
			result.Add(String.Format("IsCoinUsed:\t{0}\t>>>\t{1}",other.IsCoinUsed,IsCoinUsed));

		if(!IsHubaBlessed.Equals(other.IsHubaBlessed))
			result.Add(String.Format("IsHubaBlessed:\t{0}\t>>>\t{1}",other.IsHubaBlessed,IsHubaBlessed));

		return result;
	}
}
