using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour {

    public Sprite[] toggleImages;
    public GameObject objectToToggle;
    public bool atStartOn;
    private bool ObjectOn;

    public void Awake()
    {
        ObjectOn = (atStartOn) ? false : true;
        toggle();
    }

    // Use this for initialization
    public void toggle()
    {
        ObjectOn = !ObjectOn;
        if (ObjectOn)
        {
            GetComponent<Image>().sprite = toggleImages[0];
            objectToToggle.SetActive(true);
        }
        else
        {
            GetComponent<Image>().sprite = toggleImages[1];
            objectToToggle.SetActive(false);
        }
    }
}
