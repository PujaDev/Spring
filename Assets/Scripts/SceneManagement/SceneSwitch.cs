using UnityEngine;
using System.Collections;

public class SceneSwitch : MonoBehaviour
{
    public Transform StartPoint;
    public ActionType Action;

    private Highlight Highlight;

    void Awake()
    {
        Highlight = new BoxParticleHighlight(gameObject);
    }

    void Start()
    {
        Highlight.Subscribe();
    }

    void OnMouseDown()
    {
        SpringAction action = new SpringAction(Action, null, null);
        GameController.Instance.MoveCharToObject(gameObject, action);
    }

    public void SwitchScene()
    {
        SceneController.Instance.InitAreaForPos(StartPoint.transform.position);
        GameController.Instance.MoveCharToObject(StartPoint.gameObject);
    }
}
