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
    private Sprite BrokenSprite;
    private bool IsBroken;

    public void Reset()
    {
        if (!IsBroken)
        {
            IsOn = false;
            var r = GetComponent<SpriteRenderer>();
            r.sprite = OffSprite;
        }
    }

    void OnMouseDown()
    {
        if (!IsBroken)
        {
            IsOn = !IsOn;
            var r = GetComponent<SpriteRenderer>();
            if (IsOn)
            {
                r.sprite = OnSprite;
            }
            else
            {
                r.sprite = OffSprite;
            }

            OnClickCallback(IsOn, Value);
        }
    }

    public void Repair()
    {
        if (IsBroken)
            GetComponent<SpriteRenderer>().sprite = OffSprite;
        IsBroken = false;
    }

    #region Factory
    static bool Loaded;
    static UnityEngine.Object prefab;
    static Sprite spriteOn;
    static Sprite spriteOff;
    static Sprite spriteBroken;

    public static GameObject Create(int value, Action<bool, int> onClickCallback, Vector3 position, bool isBroken)
    {
        if (!Loaded)
        {
            spriteOn = Resources.Load<Sprite>("Sprites/LockPuzzle/switch_on");
            spriteOff = Resources.Load<Sprite>("Sprites/LockPuzzle/switch_off");
            spriteBroken = Resources.Load<Sprite>("Sprites/LockPuzzle/switch_broken");
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
        s.IsBroken = isBroken;

        var r = swch.GetComponent<SpriteRenderer>();
        r.sprite = isBroken ? spriteBroken : spriteOff;

        return swch;
    }
    #endregion
}
