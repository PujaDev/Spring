using UnityEngine;
using System.Collections;
using System;

public class CameraBoundsTrigger : MonoBehaviour
{
    public CameraManager cameraManager;
    public BoxCollider2D boxCollider;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        cameraManager.Bounds = boxCollider;
    }
}
