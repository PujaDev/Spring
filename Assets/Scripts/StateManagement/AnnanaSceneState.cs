using System;
using System.Collections.Generic;

[Serializable]
public class AnnanaSceneState : SceneState
{

	public bool AlarmPostponed { get; private set; }
	public bool AlarmTurnedOff { get; private set; }
	public int AngerLevel { get; private set; }
	public HashSet<int> BoilerContents { get; private set; }
	public bool ChangeClothes { get; private set; }
	public Vector3S CharPosition { get; private set; }
	/// <summary>
	/// Id of chosen elixir
	/// </summary>
	public int ElixirId { get; private set; }
	public bool FlyAway { get; private set; }
	public bool IsAddressPickedUp { get; private set; }
	public bool IsAddressUsed { get; private set; }
	public bool IsBerryPickedUp { get; private set; }
	public bool IsBerryUsed { get; private set; }
	public bool IsCrystalBallPickedUp { get; private set; }
	public bool IsElixirUsed { get; private set; }
	public bool IsEmptyVialPickedUp { get; private set; }
	public bool IsEmptyVialUsed { get; private set; }
	public bool IsFlowerPickedUp { get; private set; }
	public bool IsFlowerUsed { get; private set; }
	public bool IsInside { get; private set; }
	public bool IsLeafPickedUp { get; private set; }
	public bool IsLeafUsed { get; private set; }
	public bool IsOutside { get; private set; }
	public bool IsReadingFridgeNote { get; private set; }
	public bool OwlHasAddress { get; private set; }
	public int OwlPackage { get; private set; }
	public bool ReadingVeganBook { get; private set; }

	// empty constructor - for deserialization
	public AnnanaSceneState() {}

	// initial constructor - default values
	public AnnanaSceneState(bool initial) {
		AlarmPostponed = false;
		AlarmTurnedOff = false;
		AngerLevel = 13;
		BoilerContents = new HashSet<int>();
		ChangeClothes = false;
		CharPosition = new Vector3S(10.74f, -0.52f, 0f);
		ElixirId = -1;
		FlyAway = false;
		IsAddressPickedUp = false;
		IsAddressUsed = false;
		IsBerryPickedUp = false;
		IsBerryUsed = false;
		IsCrystalBallPickedUp = false;
		IsElixirUsed = false;
		IsEmptyVialPickedUp = false;
		IsEmptyVialUsed = false;
		IsFlowerPickedUp = false;
		IsFlowerUsed = false;
		IsInside = false;
		IsLeafPickedUp = false;
		IsLeafUsed = false;
		IsOutside = false;
		IsReadingFridgeNote = false;
		OwlHasAddress = false;
		OwlPackage = -1;
		ReadingVeganBook = false;
		SetCharacterPosition();
	}

	// copy constructor
	private AnnanaSceneState(AnnanaSceneState template) {
		AlarmPostponed = template.AlarmPostponed;
		AlarmTurnedOff = template.AlarmTurnedOff;
		AngerLevel = template.AngerLevel;
		BoilerContents = template.BoilerContents;
		ChangeClothes = template.ChangeClothes;
		CharPosition = template.CharPosition;
		ElixirId = template.ElixirId;
		FlyAway = template.FlyAway;
		IsAddressPickedUp = template.IsAddressPickedUp;
		IsAddressUsed = template.IsAddressUsed;
		IsBerryPickedUp = template.IsBerryPickedUp;
		IsBerryUsed = template.IsBerryUsed;
		IsCrystalBallPickedUp = template.IsCrystalBallPickedUp;
		IsElixirUsed = template.IsElixirUsed;
		IsEmptyVialPickedUp = template.IsEmptyVialPickedUp;
		IsEmptyVialUsed = template.IsEmptyVialUsed;
		IsFlowerPickedUp = template.IsFlowerPickedUp;
		IsFlowerUsed = template.IsFlowerUsed;
		IsInside = template.IsInside;
		IsLeafPickedUp = template.IsLeafPickedUp;
		IsLeafUsed = template.IsLeafUsed;
		IsOutside = template.IsOutside;
		IsReadingFridgeNote = template.IsReadingFridgeNote;
		OwlHasAddress = template.OwlHasAddress;
		OwlPackage = template.OwlPackage;
		ReadingVeganBook = template.ReadingVeganBook;
		SetCharacterPosition();
	}

	public AnnanaSceneState SetAlarmPostponed(bool value)
	{
		var copy = new AnnanaSceneState(this);
		copy.AlarmPostponed = value;
		return copy;
	}

	public AnnanaSceneState SetAlarmTurnedOff(bool value)
	{
		var copy = new AnnanaSceneState(this);
		copy.AlarmTurnedOff = value;
		return copy;
	}

	public AnnanaSceneState SetAngerLevel(int value)
	{
		var copy = new AnnanaSceneState(this);
		copy.AngerLevel = value;
		return copy;
	}

	public AnnanaSceneState SetBoilerContents(HashSet<int> value)
	{
		var copy = new AnnanaSceneState(this);
		copy.BoilerContents = value;
		return copy;
	}

	public AnnanaSceneState SetChangeClothes(bool value)
	{
		var copy = new AnnanaSceneState(this);
		copy.ChangeClothes = value;
		return copy;
	}

	public AnnanaSceneState SetCharPosition(Vector3S value)
	{
		var copy = new AnnanaSceneState(this);
		copy.CharPosition = value;
		return copy;
	}

	public AnnanaSceneState SetElixirId(int value)
	{
		var copy = new AnnanaSceneState(this);
		copy.ElixirId = value;
		return copy;
	}

	public AnnanaSceneState SetFlyAway(bool value)
	{
		var copy = new AnnanaSceneState(this);
		copy.FlyAway = value;
		return copy;
	}

	public AnnanaSceneState SetIsAddressPickedUp(bool value)
	{
		var copy = new AnnanaSceneState(this);
		copy.IsAddressPickedUp = value;
		return copy;
	}

	public AnnanaSceneState SetIsAddressUsed(bool value)
	{
		var copy = new AnnanaSceneState(this);
		copy.IsAddressUsed = value;
		return copy;
	}

	public AnnanaSceneState SetIsBerryPickedUp(bool value)
	{
		var copy = new AnnanaSceneState(this);
		copy.IsBerryPickedUp = value;
		return copy;
	}

	public AnnanaSceneState SetIsBerryUsed(bool value)
	{
		var copy = new AnnanaSceneState(this);
		copy.IsBerryUsed = value;
		return copy;
	}

	public AnnanaSceneState SetIsCrystalBallPickedUp(bool value)
	{
		var copy = new AnnanaSceneState(this);
		copy.IsCrystalBallPickedUp = value;
		return copy;
	}

	public AnnanaSceneState SetIsElixirUsed(bool value)
	{
		var copy = new AnnanaSceneState(this);
		copy.IsElixirUsed = value;
		return copy;
	}

	public AnnanaSceneState SetIsEmptyVialPickedUp(bool value)
	{
		var copy = new AnnanaSceneState(this);
		copy.IsEmptyVialPickedUp = value;
		return copy;
	}

	public AnnanaSceneState SetIsEmptyVialUsed(bool value)
	{
		var copy = new AnnanaSceneState(this);
		copy.IsEmptyVialUsed = value;
		return copy;
	}

	public AnnanaSceneState SetIsFlowerPickedUp(bool value)
	{
		var copy = new AnnanaSceneState(this);
		copy.IsFlowerPickedUp = value;
		return copy;
	}

	public AnnanaSceneState SetIsFlowerUsed(bool value)
	{
		var copy = new AnnanaSceneState(this);
		copy.IsFlowerUsed = value;
		return copy;
	}

	public AnnanaSceneState SetIsInside(bool value)
	{
		var copy = new AnnanaSceneState(this);
		copy.IsInside = value;
		return copy;
	}

	public AnnanaSceneState SetIsLeafPickedUp(bool value)
	{
		var copy = new AnnanaSceneState(this);
		copy.IsLeafPickedUp = value;
		return copy;
	}

	public AnnanaSceneState SetIsLeafUsed(bool value)
	{
		var copy = new AnnanaSceneState(this);
		copy.IsLeafUsed = value;
		return copy;
	}

	public AnnanaSceneState SetIsOutside(bool value)
	{
		var copy = new AnnanaSceneState(this);
		copy.IsOutside = value;
		return copy;
	}

	public AnnanaSceneState SetIsReadingFridgeNote(bool value)
	{
		var copy = new AnnanaSceneState(this);
		copy.IsReadingFridgeNote = value;
		return copy;
	}

	public AnnanaSceneState SetOwlHasAddress(bool value)
	{
		var copy = new AnnanaSceneState(this);
		copy.OwlHasAddress = value;
		return copy;
	}

	public AnnanaSceneState SetOwlPackage(int value)
	{
		var copy = new AnnanaSceneState(this);
		copy.OwlPackage = value;
		return copy;
	}

	public AnnanaSceneState SetReadingVeganBook(bool value)
	{
		var copy = new AnnanaSceneState(this);
		copy.ReadingVeganBook = value;
		return copy;
	}


	// compare method
	public List<string> CompareChanges(AnnanaSceneState other) {
		var result = new List<string>();

		if(!AlarmPostponed.Equals(other.AlarmPostponed))
			result.Add(String.Format("AlarmPostponed:\t{0}\t>>>\t{1}",other.AlarmPostponed,AlarmPostponed));

		if(!AlarmTurnedOff.Equals(other.AlarmTurnedOff))
			result.Add(String.Format("AlarmTurnedOff:\t{0}\t>>>\t{1}",other.AlarmTurnedOff,AlarmTurnedOff));

		if(!AngerLevel.Equals(other.AngerLevel))
			result.Add(String.Format("AngerLevel:\t{0}\t>>>\t{1}",other.AngerLevel,AngerLevel));

		if(!BoilerContents.Equals(other.BoilerContents))
			result.Add(String.Format("BoilerContents:\t{0}\t>>>\t{1}",other.BoilerContents,BoilerContents));

		if(!ChangeClothes.Equals(other.ChangeClothes))
			result.Add(String.Format("ChangeClothes:\t{0}\t>>>\t{1}",other.ChangeClothes,ChangeClothes));

		if(!CharPosition.Equals(other.CharPosition))
			result.Add(String.Format("CharPosition:\t{0}\t>>>\t{1}",other.CharPosition,CharPosition));

		if(!ElixirId.Equals(other.ElixirId))
			result.Add(String.Format("ElixirId:\t{0}\t>>>\t{1}",other.ElixirId,ElixirId));

		if(!FlyAway.Equals(other.FlyAway))
			result.Add(String.Format("FlyAway:\t{0}\t>>>\t{1}",other.FlyAway,FlyAway));

		if(!IsAddressPickedUp.Equals(other.IsAddressPickedUp))
			result.Add(String.Format("IsAddressPickedUp:\t{0}\t>>>\t{1}",other.IsAddressPickedUp,IsAddressPickedUp));

		if(!IsAddressUsed.Equals(other.IsAddressUsed))
			result.Add(String.Format("IsAddressUsed:\t{0}\t>>>\t{1}",other.IsAddressUsed,IsAddressUsed));

		if(!IsBerryPickedUp.Equals(other.IsBerryPickedUp))
			result.Add(String.Format("IsBerryPickedUp:\t{0}\t>>>\t{1}",other.IsBerryPickedUp,IsBerryPickedUp));

		if(!IsBerryUsed.Equals(other.IsBerryUsed))
			result.Add(String.Format("IsBerryUsed:\t{0}\t>>>\t{1}",other.IsBerryUsed,IsBerryUsed));

		if(!IsCrystalBallPickedUp.Equals(other.IsCrystalBallPickedUp))
			result.Add(String.Format("IsCrystalBallPickedUp:\t{0}\t>>>\t{1}",other.IsCrystalBallPickedUp,IsCrystalBallPickedUp));

		if(!IsElixirUsed.Equals(other.IsElixirUsed))
			result.Add(String.Format("IsElixirUsed:\t{0}\t>>>\t{1}",other.IsElixirUsed,IsElixirUsed));

		if(!IsEmptyVialPickedUp.Equals(other.IsEmptyVialPickedUp))
			result.Add(String.Format("IsEmptyVialPickedUp:\t{0}\t>>>\t{1}",other.IsEmptyVialPickedUp,IsEmptyVialPickedUp));

		if(!IsEmptyVialUsed.Equals(other.IsEmptyVialUsed))
			result.Add(String.Format("IsEmptyVialUsed:\t{0}\t>>>\t{1}",other.IsEmptyVialUsed,IsEmptyVialUsed));

		if(!IsFlowerPickedUp.Equals(other.IsFlowerPickedUp))
			result.Add(String.Format("IsFlowerPickedUp:\t{0}\t>>>\t{1}",other.IsFlowerPickedUp,IsFlowerPickedUp));

		if(!IsFlowerUsed.Equals(other.IsFlowerUsed))
			result.Add(String.Format("IsFlowerUsed:\t{0}\t>>>\t{1}",other.IsFlowerUsed,IsFlowerUsed));

		if(!IsInside.Equals(other.IsInside))
			result.Add(String.Format("IsInside:\t{0}\t>>>\t{1}",other.IsInside,IsInside));

		if(!IsLeafPickedUp.Equals(other.IsLeafPickedUp))
			result.Add(String.Format("IsLeafPickedUp:\t{0}\t>>>\t{1}",other.IsLeafPickedUp,IsLeafPickedUp));

		if(!IsLeafUsed.Equals(other.IsLeafUsed))
			result.Add(String.Format("IsLeafUsed:\t{0}\t>>>\t{1}",other.IsLeafUsed,IsLeafUsed));

		if(!IsOutside.Equals(other.IsOutside))
			result.Add(String.Format("IsOutside:\t{0}\t>>>\t{1}",other.IsOutside,IsOutside));

		if(!IsReadingFridgeNote.Equals(other.IsReadingFridgeNote))
			result.Add(String.Format("IsReadingFridgeNote:\t{0}\t>>>\t{1}",other.IsReadingFridgeNote,IsReadingFridgeNote));

		if(!OwlHasAddress.Equals(other.OwlHasAddress))
			result.Add(String.Format("OwlHasAddress:\t{0}\t>>>\t{1}",other.OwlHasAddress,OwlHasAddress));

		if(!OwlPackage.Equals(other.OwlPackage))
			result.Add(String.Format("OwlPackage:\t{0}\t>>>\t{1}",other.OwlPackage,OwlPackage));

		if(!ReadingVeganBook.Equals(other.ReadingVeganBook))
			result.Add(String.Format("ReadingVeganBook:\t{0}\t>>>\t{1}",other.ReadingVeganBook,ReadingVeganBook));

		return result;
	}
}
