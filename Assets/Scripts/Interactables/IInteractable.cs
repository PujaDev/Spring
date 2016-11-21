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
            SceneController.controller.title.text = tooltipText;
        }
    }

    void OnMouseExit()
    {
        if (!keepTooltipOpen)
        {
            //Destroy(tooltipObject);
            SceneController.controller.title.text = "";
        }
    }

    void OnMouseDown()
    {
        var actionList = getActionList();
        keepTooltipOpen = true;
        ActionWheel.Instance.ShowActions(actionList,this);
        Debug.Log(ComeCloser());
    }
    
    public static void DestroyInteractable(IInteractable interactable)
    {
        StateManager.Instance.Unsubscribe(interactable);
        Destroy(interactable.gameObject);
    }

    bool ComeCloser() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, Mathf.Infinity, LayerMask.NameToLayer("WalkableArea"));
        Debug.Log(LayerMask.NameToLayer("WalkableArea"));
        Debug.DrawRay(transform.position, -Vector3.up, Color.green);
        Debug.Log(hit.point);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
            CharacterInput walkableArea = hit.collider.gameObject.GetComponent<CharacterInput>();
            if(walkableArea != null)
            {
                walkableArea.MoveToPoint(hit.point);
                return true;
            }
        }

        return false;
    }

    virtual public void OnStateChanged(GameState newState, GameState oldState)
    {
    }

    abstract protected Action[] getActionList();

}
