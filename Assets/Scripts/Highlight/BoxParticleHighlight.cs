using UnityEngine;
using System.Collections;
using System;

public class ColliderParticleHighlight : Highlight
{
    private ParticleSystem HighlightEffect;
    private GameObject Source;
    private Collider2D Collider;
    
    private static GameObject HighlightPrefab;

    public ColliderParticleHighlight(GameObject source)
    {
        // TODO Create resource manager and move following condition there
        if (HighlightPrefab == null)
            HighlightPrefab = Resources.Load<GameObject>("Prefabs/InteractableEffect");

        Source = source;

        var hlGameObject = GameObject.Instantiate(HighlightPrefab);
        hlGameObject.transform.parent = Source.transform;
        HighlightEffect = hlGameObject.GetComponent<ParticleSystem>();

        StopHighlight();
    }

    public override void StartHighlight()
    {
        if (IsColliderOn())
        {
            AdjustPosition();

            // Enable emission
            var em = HighlightEffect.emission;
            em.enabled = true;
        }
    }
    public override void StopHighlight()
    {
        // Turn off even if there is no collider - it may have been destroyed
        var em = HighlightEffect.emission;
        em.enabled = false;
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