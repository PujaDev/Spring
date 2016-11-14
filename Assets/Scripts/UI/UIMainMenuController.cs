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

    public void menuTransition() {
        playButton.GetComponent<ObjectsSwapper>().SwapObject();
        bigCircleAndRest.GetComponent<ObjectResizer>().ResizeObject();
        smallCreditsCircle.GetComponent<ObjectsSwapper>().SwapObject();
    }

	// Update is called once per frame
	void Update () {

	}
}
