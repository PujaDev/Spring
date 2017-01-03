﻿using UnityEngine;

/// <summary>
/// Parent class for interactable objects
/// </summary>
public abstract class IInteractable : MonoBehaviour, IChangable
{
    public string tooltipText = "";
    public Sprite[] icons;
    private bool keepTooltipOpen = false;
    //GameObject tooltipPrefab;
    //GameObject tooltipObject;

    protected Highlight Highlight;

    protected virtual void Awake()
    {
        Highlight = new BoxParticleHighlight(gameObject);
    }

    protected virtual void Start()
    {
        Highlight.Subscribe();

        //tooltipPrefab = (GameObject)Resources.Load("Prefabs/tooltip_text", typeof(GameObject)); // OPTIMIZE
        var gameState = StateManager.Instance.Subscribe(this);
        OnStateChanged(gameState, null);
    }

    void OnMouseEnter()
    {
        keepTooltipOpen = false;
        //Destroy(tooltipObject);
        if (!GameController.Instance.isUI)
        {
            //tooltipObject = Instantiate(tooltipPrefab);
            //tooltipObject.GetComponent<TextMesh>().text = tooltipText;
            SceneController.Instance.title.text = tooltipText;
        }
    }

    void OnMouseExit()
    {
        if (!keepTooltipOpen)
        {
            //Destroy(tooltipObject);
            SceneController.Instance.title.text = "";
        }
    }

    void OnMouseDown()
    {
        if (!GameController.Instance.isUI)
        {
            var actionList = GetActionList();
            keepTooltipOpen = true;
            ActionWheel.Instance.ShowActions(actionList, this);
            //Debug.Log(ComeCloser());
        }
    }
    
    public static void DestroyInteractable(IInteractable interactable)
    {
        StateManager.Instance.Unsubscribe(interactable);
        Destroy(interactable.gameObject);
    }

    public bool ComeCloser(SpringAction action = null)
    {
        return GameController.Instance.MoveCharToObject(gameObject, action, this);
    }

    virtual public void OnStateChanged(GameState newState, GameState oldState)
    {
    }

    abstract protected SpringAction[] GetActionList();

    
}
