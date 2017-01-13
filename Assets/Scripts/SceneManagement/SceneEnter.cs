using UnityEngine;
using System.Collections;

public class SceneEnter : MonoBehaviour
{
    public Transform StartPoint;

    public void EnterScene()
    {
        SceneController.Instance.InitAreaForPos(StartPoint.transform.position);
        GameController.Instance.MoveCharToObject(StartPoint.gameObject);
    }
}
