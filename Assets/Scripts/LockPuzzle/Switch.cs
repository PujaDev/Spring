using UnityEngine;
using System.Collections;
using System;

public class Switch : MonoBehaviour
{
    public bool IsOn { get; private set; }
    public int Value { get; private set; }
    private Action<bool, int> OnClickCallback;

    public void Reset()
    {
        IsOn = false;
        var r = GetComponent<SpriteRenderer>();
        r.color = Color.white;
    }

    void OnMouseDown()
    {
        IsOn = !IsOn;
        var r = GetComponent<SpriteRenderer>();
        if(IsOn)
        {
            r.color = Color.red;
        }
        else
        {
            r.color = Color.white;
        }

        OnClickCallback(IsOn, Value);
    }


    #region Factory
    static bool Loaded;
    static UnityEngine.Object prefab;

    public static GameObject Create(int value, Action<bool, int> onClickCallback, Vector3 position)
    {
        if (!Loaded)
        {
            prefab = Resources.Load("Prefabs/LockPuzzle/Switch");
            Loaded = true;
        }

        GameObject swch = Instantiate(prefab) as GameObject;
        var s = swch.GetComponent<Switch>();

        s.Value = value;
        s.OnClickCallback = onClickCallback;
        swch.transform.position = position;

        return swch;
    }
    #endregion
}
