using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterInput : MonoBehaviour
{
    public CharacterMovement Character;

    [HideInInspector] public Collider2D WalkableArea;
    [HideInInspector] public Collider2D[] areas;
    [HideInInspector] public Transform[] transPoints;

    private List<Vector3> targets;

    // Initializes collider areas and transforms from children in the ORDER as they are in the Hierarchy view
    void Awake()
    {
        List<Transform> tmp_T = new List<Transform>();
        List<Collider2D> tmp_C = new List<Collider2D>();

        GetComponentsInChildren(tmp_T);
        GetComponentsInChildren(tmp_C);

        WalkableArea = tmp_C[0];
        tmp_T.RemoveAt(0);
        tmp_C.RemoveAt(0);

        transPoints = tmp_T.ToArray();
        areas = tmp_C.ToArray();
        targets = new List<Vector3>();
    }

    // Calculates path for character to move along to reach destination 
    public void MoveToPoint(Vector2 destination, Action action=null, IInteractable source=null) {
        if (WalkableArea.OverlapPoint(destination)) //checks whether mouse is in the walkable area
        {
            int i;
            int target = -1;
            targets.Clear();
            for (i = 0; i < areas.Length; i++) //checks which area is the destination in
            {
                if (areas[i].OverlapPoint(destination))
                {
                    target = i;
                    break;
                }
            }
            Vector3 tmp;
            i = SceneController.Instance.currentAreaIndex;
            if (target < i) //path through areas with decreasing numbers
            {
                while (i > target)
                {
                    tmp = new Vector3(transPoints[i].position.x, transPoints[i].position.y);
                    targets.Add(tmp);
                    i--;
                }
            }
            else //path through areas with increasing numbers
            {
                while (i < target)
                {
                    tmp = new Vector3(transPoints[i + 1].position.x, transPoints[i + 1].position.y);
                    targets.Add(tmp);
                    i++;
                }
            }
            tmp = new Vector3(destination.x, destination.y);
            targets.Add(tmp);
            SceneController.Instance.targetAreaIndex = target;

            Character.MoveTo(targets, action, source);
        }
    }

    // called each frame, checks for mouse clicks on movable areas to move the character there
    void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0) && !(GameController.controller.isUI) && GameController.controller.lastUITime != Time.time)
        {
            var mousePos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos3D.x, mousePos3D.y); // Cast the mouse position to 2D

            MoveToPoint(mousePos2D);
        }
    }
}
