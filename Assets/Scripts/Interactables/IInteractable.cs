using UnityEngine;

/// <summary>
/// Parent class for interactable objects
/// </summary>
public abstract class IInteractable : MonoBehaviour {

    public string tooltipText = "";
    public Sprite[] icons;
    private bool keepTooltipOpen = false;
    //GameObject tooltipPrefab;
    //GameObject tooltipObject;

    void Start()
    {
        //tooltipPrefab = (GameObject)Resources.Load("Prefabs/tooltip_text", typeof(GameObject)); // OPTIMIZE
        var gameState = StateManager.Instance.Subscribe(this);
        OnStateChanged(gameState, null);
    }


    void OnMouseEnter()
    {
        keepTooltipOpen = false;
        //Destroy(tooltipObject);
        if (!GameController.controller.isUI)
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
        if (!GameController.controller.isUI)
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

    public bool ComeCloser(Action action = null) {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, Mathf.Infinity, 0 | (1 << LayerMask.NameToLayer("WalkableArea")));
        Vector2 destination;
        //Debug.Log(hit.point);
        if (hit.collider == null) {
            hit = Physics2D.Raycast(transform.position, Vector2.up, Mathf.Infinity, 0 | (1 << LayerMask.NameToLayer("WalkableArea")));
            //Debug.Log(hit.point);
            destination = hit.point;
            destination.y += 0.001f;
        }else{
            destination = hit.point;
            destination.y -= 0.001f;
        }
        if(hit.collider != null)
        {
            //Debug.Log(hit.collider.name);
            CharacterInput walkableArea = hit.collider.gameObject.GetComponent<CharacterInput>();
            if(walkableArea != null)
            {
                walkableArea.MoveToPoint(destination, action, this);
                return true;
            }
        }

        return false;
    }

    virtual public void OnStateChanged(GameState newState, GameState oldState)
    {
    }

    abstract protected Action[] GetActionList();
}
