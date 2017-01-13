using UnityEngine;
using System.Collections;

public class Close : MonoBehaviour
{
    void OnMouseDown()
    {
        LockManager.Instance.Close();
    }
}
