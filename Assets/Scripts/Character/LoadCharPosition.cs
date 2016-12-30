using UnityEngine;
using System.Collections;

public class LoadCharPosition : MonoBehaviour {
    public int sceneID;

	// Use this for initialization
	public void RelocateCharacter (GameState loadedState) {
        switch (sceneID) {
            case 1:
                transform.position = loadedState.AnnanaHouse.CharPosition.GetVector3();
                break;
            case 2:
                transform.position = loadedState.HubaBus.CharPosition.GetVector3();
                break;
            //case 3:
            //    break;
            //case 4:
            //    break;
        }
        
    }

    public Vector3S GetCharPosition() {
        return new Vector3S(transform.position);
    }
}
