using System;
using System.Collections.Generic;

[Serializable]
public class AnnanaTeaPartySceneState : SceneState
{

	public bool DrankTea { get; private set; }

	// empty constructor - for deserialization
	public AnnanaTeaPartySceneState() {}

	// initial constructor - default values
	public AnnanaTeaPartySceneState(bool initial) {
		DrankTea = false;
		SetCharacterPosition();
	}

	// copy constructor
	private AnnanaTeaPartySceneState(AnnanaTeaPartySceneState template) {
		DrankTea = template.DrankTea;
		SetCharacterPosition();
	}

	public AnnanaTeaPartySceneState SetDrankTea(bool value)
	{
		var copy = new AnnanaTeaPartySceneState(this);
		copy.DrankTea = value;
		return copy;
	}


	// compare method
	public List<string> CompareChanges(AnnanaTeaPartySceneState other) {
		var result = new List<string>();

		if(!DrankTea.Equals(other.DrankTea))
			result.Add(String.Format("DrankTea:\t{0}\t>>>\t{1}",other.DrankTea,DrankTea));

		return result;
	}
}
