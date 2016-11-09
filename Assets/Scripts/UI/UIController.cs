using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public bool soundOn;
    public GameObject playButton;
    public GameObject bigCircleAndRest;
    public GameObject smallCreditsCircle;

    // Use this for initialization
    void Start () {
        soundOn = true;
    }

    public void ToggleSound() {
        soundOn = !soundOn;
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
