using UnityEngine;
using System.Collections;

public class SceneExit : MonoBehaviour
{
    public ActionType ExitAction;
    private Highlight Highlight;

    void Awake()
    {
        Highlight = new ColliderParticleHighlight(gameObject);
    }

    void Start()
    {
        Highlight.Subscribe();
    }

    void OnMouseDown()
    {
        SpringAction action = new SpringAction(ExitAction, null, null);
        GameController.Instance.MoveCharToObject(gameObject, action);
    }
}
