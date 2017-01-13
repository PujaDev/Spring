using UnityEngine;
using System.Collections;

public enum UIBlocker {
    INVENTORY,
    OPTIONS,
    TIPS
}

public class Blocker : MonoBehaviour {
    public static Blocker Instance { get; set; }

    public GameObject Screen16x9;
    public GameObject Screen4x3;
    public GameObject Screen16x10;
    private GameObject currentRatio;

    void Awake()
    {
        Instance = this;

        if (Camera.main.aspect >= 1.7)
        {
            Screen16x9.SetActive(true);
            currentRatio = Screen16x9;
            Screen4x3.SetActive(false);
            Screen16x10.SetActive(false);
        }
        else if (Camera.main.aspect >= 1.5)
        {
            Screen16x10.SetActive(true);
            currentRatio = Screen16x10;
            Screen16x9.SetActive(false);
            Screen4x3.SetActive(false);
        }
        else
        {
            Screen4x3.SetActive(true);
            currentRatio = Screen4x3;
            Screen16x9.SetActive(false);
            Screen16x10.SetActive(false);
        }
            
        float newScale = (transform.localScale.x / 5.2f) * Camera.main.orthographicSize;
        transform.localScale = new Vector3(newScale, newScale, newScale);
        
    }
    
    public void ToggleBlocker(bool toggleOn, UIBlocker blocker)
    {
        Collider2D[] colliders = currentRatio.GetComponentsInChildren<Collider2D>();
        switch (blocker) {
            case UIBlocker.INVENTORY:
                colliders[2].enabled = toggleOn;
                break;
            case UIBlocker.OPTIONS:
                colliders[3].enabled = toggleOn;
                break;
            case UIBlocker.TIPS:
                colliders[4].enabled = toggleOn;
                break;
        }
    }

}
