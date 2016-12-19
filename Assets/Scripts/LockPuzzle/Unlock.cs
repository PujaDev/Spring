using UnityEngine;
using System.Collections;

public class Unlock : MonoBehaviour
{
    public LockController LockController;

    void OnMouseDown()
    {
        LockController.Unlock();
    }
}
