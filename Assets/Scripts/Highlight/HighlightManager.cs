using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HighlightManager : MonoBehaviour
{
    const KeyCode HIGHLIGHT_KEY = KeyCode.Space;

    public static HighlightManager Instance { get; private set; }

    private HashSet<IHighlightable> Highlitables;
    private bool IsHighliting;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Highlitables = new HashSet<IHighlightable>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Subscribe(IHighlightable h)
    {
        Highlitables.Add(h);
    }

    public void Unsubscribe(IHighlightable h)
    {
        Highlitables.Remove(h);
    }

    void Update()
    {
        if (Input.GetKeyDown(HIGHLIGHT_KEY) && !IsHighliting)
        {
            IsHighliting = true;
            foreach (var item in Highlitables)
            {
                item.StartHighlight();
            }
        }
        else if (Input.GetKeyUp(HIGHLIGHT_KEY) && IsHighliting)
        {
            IsHighliting = false;
            foreach (var item in Highlitables)
            {
                item.StopHighlight();
            }
        }
    }
}
