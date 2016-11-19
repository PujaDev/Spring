using UnityEngine;

/// <summary>
/// Parent class for interactable objects
/// </summary>
public abstract class IInteractable : MonoBehaviour {

    public string tooltipText = "";
    public Sprite[] icons;
    private bool keepTooltipOpen = false;
    GameObject tooltipPrefab;
    GameObject tooltipObject;

    void Start()
    {
        tooltipPrefab = (GameObject)Resources.Load("Prefabs/tooltip_text", typeof(GameObject)); // OPTIMIZE
        var gameState = StateManager.Instance.Subscribe(this);
        OnStateChanged(gameState, null);
    }


    void OnMouseEnter()
    {
        keepTooltipOpen = false;
        Destroy(tooltipObject);
        tooltipObject = Instantiate(tooltipPrefab);
        tooltipObject.GetComponent<TextMesh>().text = tooltipText;
    }

    void OnMouseExit()
    {
        if(!keepTooltipOpen)
            Destroy(tooltipObject);
    }

    void OnMouseDown()
    {
        var actionList = getActionList();
        keepTooltipOpen = true;
        ActionWheel.Instance.ShowActions(actionList,this);
    }

    virtual public void OnStateChanged(GameState newState, GameState oldState)
    {
    }

    abstract protected Action[] getActionList();

}
