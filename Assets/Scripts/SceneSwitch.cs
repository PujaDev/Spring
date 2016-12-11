using UnityEngine;
using System.Collections;

public class SceneSwitch : MonoBehaviour
{
    public Transform StartPoint;

    void OnMouseDown()
    {
        SpringAction action = new SpringAction(ActionType.GO_OUTSIDE, "Go outside", null);
        GameController.controller.MoveCharToObject(gameObject, action);
    }

    public void MoveToStart()
    {
        SceneController.Instance.InitAreaForPos(StartPoint.transform.position);
        GameController.controller.MoveCharToObject(StartPoint.gameObject);
    }
}
