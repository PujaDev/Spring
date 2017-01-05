using UnityEngine;
using System.Collections;
using System;

public class ColliderParticleHighlight : Highlight
{
    private GameObject HighlightEffect;
    /// <summary>
    /// Is true only if the object has collider
    /// </summary>
    private bool Enabled;
    private static GameObject HighlightPrefab;

    public ColliderParticleHighlight(GameObject source)
    {
        // TODO Create resource manager and move following there
        if (HighlightPrefab == null)
            HighlightPrefab = Resources.Load<GameObject>("Prefabs/InteractableEffect");

        HighlightEffect = GameObject.Instantiate(HighlightPrefab);
        HighlightEffect.transform.parent = source.transform;

        var collider = source.GetComponent<Collider2D>();
        if (collider != null)
        {
            Enabled = true;
            HighlightEffect.transform.position = collider.bounds.center;
        }

        HighlightEffect.SetActive(false);
    }

    public override void StartHighlight()
    {
        if (Enabled)
            HighlightEffect.SetActive(true);
    }
    public override void StopHighlight()
    {
        if (Enabled)
            HighlightEffect.SetActive(false);
    }
}