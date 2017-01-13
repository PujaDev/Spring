using UnityEngine;
using System.Collections;

public class ToggleLight : MonoBehaviour
{
    public Sprite OffSprite;
    public Sprite OnSprite;

    private SpriteRenderer Rend;

    void Awake()
    {
        Rend = GetComponent<SpriteRenderer>();
        Rend.sprite = OffSprite;
    }

    public void Toggle(bool isOn)
    {
        Rend.sprite = isOn ? OnSprite : OffSprite;
    }
}
