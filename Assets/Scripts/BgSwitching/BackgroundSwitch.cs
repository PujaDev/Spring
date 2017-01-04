using UnityEngine;
using System.Collections;

public class BackgroundSwitch : MonoBehaviour
{
    public SpriteRenderer ObjToSwitch;

    bool IsForeground;
    Collider2D MyCollider;

    void Awake()
    {
        MyCollider = gameObject.GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger enter");
        IsForeground = !IsForeground;
        SwitchLayer();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Trigger exit");
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
        if (IsForeground)
            ObjToSwitch.sortingLayerName = "Foreground";
        else
            ObjToSwitch.sortingLayerName = "Background";
    }
}
