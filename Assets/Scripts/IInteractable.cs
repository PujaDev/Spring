using UnityEngine;
using System.Collections;

public abstract class IInteractable : MonoBehaviour {

    public string tooltipText = "";

    GameObject tooltipPrefab;
    GameObject tooltipObject;

    void Start()
    {
        tooltipPrefab = (GameObject)Resources.Load("Prefabs/tooltip_text", typeof(GameObject)); // OPTIMIZE
    }

    void OnMouseEnter()
    {
        Destroy(tooltipObject);
        tooltipObject = Instantiate(tooltipPrefab);
        tooltipObject.GetComponent<TextMesh>().text = tooltipText;
    }

    void OnMouseExit()
    {
        Destroy(tooltipObject);
    }

    void OnMouseDown()
    {
        var actionList = getActionList();
        foreach (var action in actionList)
        {
            Debug.Log(action);
        }
    }

    abstract protected Action[] getActionList();

}
