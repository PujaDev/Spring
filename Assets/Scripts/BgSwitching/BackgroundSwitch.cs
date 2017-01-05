using UnityEngine;
using System.Collections;
using Spine.Unity;

public class BackgroundSwitch : MonoBehaviour
{
    public MeshRenderer[] ObjToSwitch;

    public int BgOrder;
    public int FgOrder;

    bool IsBackground;
    Collider2D MyCollider;

    void Awake()
    {
        MyCollider = gameObject.GetComponent<Collider2D>();
    }

    public void Reset()
    {
        IsBackground = false;
        SwitchLayer();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        IsBackground = !IsBackground;
        SwitchLayer();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Other = character
        bool behind = other.bounds.center.y > MyCollider.bounds.center.y;

        if ((IsBackground && !behind)
            || (!IsBackground && behind))
        {
            IsBackground = !IsBackground;
            SwitchLayer();
        }
    }

    private void SwitchLayer()
    {
        int layerOrder = IsBackground ? BgOrder : FgOrder;
        
        foreach (var item in ObjToSwitch)
        {
            item.sortingOrder = layerOrder;
        }
    }
}
