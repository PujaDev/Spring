using UnityEngine;
using System.Collections;

public class CharacterInput : MonoBehaviour
{
    public Collider2D Area;
    public CharacterMovement Character;

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
            Vector3 target = new Vector3(mousePos2D.x, Character.Position.y);
            Character.MoveTo(target);
        }
    }

    
}
