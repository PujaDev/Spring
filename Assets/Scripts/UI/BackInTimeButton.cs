using UnityEngine;
using System.Collections;

public class BackInTimeButton : MonoBehaviour {
    public GameObject panel;

    public void PlayYes()
    {
        LevelSelector.selector.GoBackInTime();
        //Destroy(panel);
    }

    public void PlayNo()
    {
        Destroy(panel);
    }
}
