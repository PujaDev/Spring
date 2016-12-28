using UnityEngine;
using System.Collections;

public class SceneSwitch : MonoBehaviour
{
    public Transform StartPoint;
    public ActionType Action;

    void OnMouseDown()
    {
        SpringAction action = new SpringAction(Action, null, null);
        GameController.Instance.MoveCharToObject(gameObject, action);
    }

    public void MoveToStart()
    {
        SceneController.Instance.InitAreaForPos(StartPoint.transform.position);
        GameController.Instance.MoveCharToObject(StartPoint.gameObject);
    }
}
