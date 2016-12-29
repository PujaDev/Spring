using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIMainMenuController : MonoBehaviour {
    public GameObject playButton;
    public GameObject bigCircleAndRest;
    public GameObject smallCreditsCircle;

    // Use this for initialization
    void Start () {
    }

    public void menuTransition(int id = 1) {
        playButton.GetComponent<ObjectsSwapper>().SwapObject(id);
        bigCircleAndRest.GetComponent<ObjectResizer>().ResizeObject();
        smallCreditsCircle.GetComponent<ObjectsSwapper>().SwapObject();
    }

	// Update is called once per frame
	void Update () {

	}
}
