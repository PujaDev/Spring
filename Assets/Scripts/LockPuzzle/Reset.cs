using UnityEngine;
using System.Collections;

public class Reset : MonoBehaviour
{
    public LockController LockController;

    void OnMouseDown()
    {
        LockController.Reset();
    }
}
