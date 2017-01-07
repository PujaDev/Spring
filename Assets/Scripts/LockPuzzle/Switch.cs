using UnityEngine;
using System.Collections;
using System;

public class Switch : MonoBehaviour
{
    public bool IsOn { get; private set; }
    public int Value { get; private set; }
    private Action<bool, int> OnClickCallback;
    private Sprite OnSprite;
    private Sprite OffSprite;

    public void Reset()
    {
        IsOn = false;
        var r = GetComponent<SpriteRenderer>();
        r.sprite = OffSprite;
    }

    void OnMouseDown()
    {
        IsOn = !IsOn;
        var r = GetComponent<SpriteRenderer>();
        if(IsOn)
        {
            r.sprite = OnSprite;
        }
        else
        {
            r.sprite = OffSprite;
        }

        OnClickCallback(IsOn, Value);
    }


    #region Factory
    static bool Loaded;
    static UnityEngine.Object prefab;
    static Sprite spriteOn;
    static Sprite spriteOff;

    public static GameObject Create(int value, Action<bool, int> onClickCallback, Vector3 position)
    {
        if (!Loaded)
        {
            spriteOn = Resources.Load<Sprite>("Sprites/LockPuzzle/switch_on");
            spriteOff = Resources.Load<Sprite>("Sprites/LockPuzzle/switch_off");
            prefab = Resources.Load("Prefabs/LockPuzzle/Switch");
            Loaded = true;
        }

        GameObject swch = Instantiate(prefab) as GameObject;
        var s = swch.GetComponent<Switch>();

        s.Value = value;
        s.OnClickCallback = onClickCallback;
        s.OnSprite = spriteOn;
        s.OffSprite = spriteOff;
        swch.transform.position = position;

        return swch;
    }
    #endregion
}
