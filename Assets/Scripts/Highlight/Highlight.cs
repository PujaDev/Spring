using UnityEngine;
using System.Collections;
using System;

public abstract class Highlight : IHighlightable
{
    public abstract void StartHighlight();
    public abstract void StopHighlight();
    
    public virtual void Subscribe()
    {
        HighlightManager.Instance.Subscribe(this);
    }

    public virtual void Unsubscribe()
    {
        HighlightManager.Instance.Unsubscribe(this);
    }
}
