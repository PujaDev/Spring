using UnityEngine;
using System.Collections;

public class BackgroundSwitch : MonoBehaviour
{
    public SpriteRenderer[] ObjToSwitch;

    bool IsForeground;
    Collider2D MyCollider;

    void Awake()
    {
        MyCollider = gameObject.GetComponent<Collider2D>();
    }

    public void Reset()
    {
        IsForeground = false;
        SwitchLayer();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        IsForeground = !IsForeground;
        SwitchLayer();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        bool behind = other.bounds.center.y > MyCollider.bounds.center.y;

        if ((IsForeground && !behind)
            || (!IsForeground && behind))
        {
            IsForeground = !IsForeground;
            SwitchLayer();
        }
    }

    private void SwitchLayer()
    {
        string layerName = IsForeground ? "Foreground" : "Background";
        
        foreach (var item in ObjToSwitch)
        {
            item.sortingLayerName = layerName;
        }
    }
}
