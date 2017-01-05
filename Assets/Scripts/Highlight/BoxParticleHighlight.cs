using UnityEngine;
using System.Collections;
using System;

public class ColliderParticleHighlight : Highlight
{
    private GameObject HighlightEffect;
    private GameObject Source;
    private Collider2D Collider;
    
    private static GameObject HighlightPrefab;

    public ColliderParticleHighlight(GameObject source)
    {
        // TODO Create resource manager and move following condition there
        if (HighlightPrefab == null)
            HighlightPrefab = Resources.Load<GameObject>("Prefabs/InteractableEffect");

        Source = source;
        HighlightEffect = GameObject.Instantiate(HighlightPrefab);
        HighlightEffect.transform.parent = Source.transform;

        StopHighlight();
    }

    public override void StartHighlight()
    {
        if (IsColliderOn())
        {
            AdjustPosition();
            HighlightEffect.SetActive(true);
        }
    }
    public override void StopHighlight()
    {
        // Turn off even if there is no collider - it may have been destroyed
        HighlightEffect.SetActive(false);
    }

    private void AdjustPosition()
    {
        HighlightEffect.transform.position = Collider.bounds.center;
    }

    /// <summary>
    /// Is true only if the object has enabled collider
    /// </summary>
    private bool IsColliderOn()
    {
        // Search for it every time since it may have been created
        Collider = Source.GetComponent<Collider2D>();
        return Collider != null && Collider.enabled;
    }
}