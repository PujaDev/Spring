﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SwitchSprites : MonoBehaviour {

    public Sprite[] sprites;
    private Image image;
    private int index;

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
        index = 0;
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
