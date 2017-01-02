using UnityEngine;
using System.Collections;
using System;

public class BoxParticleHighlight : Highlight
{
    private GameObject HighlightEffect;
    private static GameObject HighlightPrefab;

    public BoxParticleHighlight(GameObject source)
    {
        // TODO Create resource manager and move following there
        if (HighlightPrefab == null)
            HighlightPrefab = Resources.Load<GameObject>("Prefabs/InteractableEffect");

        HighlightEffect = GameObject.Instantiate(HighlightPrefab);
        HighlightEffect.transform.parent = source.transform;

        var collider = source.GetComponent<BoxCollider2D>();
        if (collider != null)
            HighlightEffect.transform.position = collider.bounds.center;
        else
            HighlightEffect.transform.position = source.transform.position;

        HighlightEffect.SetActive(false);
    }

    public override void StartHighlight()
    {
        HighlightEffect.SetActive(true);
    }
    public override void StopHighlight()
    {
        HighlightEffect.SetActive(false);
    }
}