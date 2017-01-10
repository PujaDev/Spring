using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SwitchSprites : MonoBehaviour {

    public Sprite[] sprites;
    private Image image;
    private int index = 0;
    public bool forSound = false;

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
        if (forSound)
        {
            if (GameController.Instance.IsSoundOn)
            {
                index = 0;
            }
            else
            {
                index = 1;
            }
        }
        image.sprite = sprites[index];
	}

    public void next()
    {
        index++;
        if (index >= sprites.Length)
        {
            index = 0;
        }
        image.sprite = sprites[index];
    }
}
