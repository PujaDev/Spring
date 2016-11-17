using UnityEngine;
using System.Collections;

public class ClickShaker : MonoBehaviour
{
    public BoxCollider2D Area;
    public float Duration;
    public float Magnitude;
    public float Speed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var mousePos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos3D.x, mousePos3D.y); // Cast the mouse position to 2D

        if (Input.GetMouseButtonUp(0) && Area.bounds.Contains(mousePos2D))
        {
            CameraManager.Instance.Shake(Duration, Magnitude, Speed);
        }
    }
}
