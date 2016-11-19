using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterInput : MonoBehaviour
{
    public Collider2D Area;
    public CharacterMovement Character;
    public Collider2D[] areas;
    public Transform[] transPoints;
    List<Vector3> targets;

    void Start()
    {
        targets = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        var mousePos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos3D.x, mousePos3D.y); // Cast the mouse position to 2D
        
        if (Input.GetMouseButtonUp(0) && !(GameController.controller.isUI) && GameController.controller.lastUITime != Time.time)
        {
            if (Area.OverlapPoint(mousePos2D)) {
                int i;
                int target = -1;
                targets.Clear();
                for (i = 0; i < areas.Length; i++)
                {
                    if (areas[i].OverlapPoint(mousePos2D)) {
                        target = i;
                        break;
                    }
                }
                Vector3 tmp;
                i = GameController.controller.currentArea;
                if (target < i) {
                    while(i > target) {
                        tmp = new Vector3(transPoints[i].position.x, transPoints[i].position.y);
                        targets.Add(tmp);
                        i--;
                    }
                } else {
                    while (i < target)
                    {
                        tmp = new Vector3(transPoints[i+1].position.x, transPoints[i+1].position.y);
                        targets.Add(tmp);
                        i++;
                    }
                }
                tmp = new Vector3(mousePos2D.x, mousePos2D.y);
                targets.Add(tmp);
                GameController.controller.targetArea = target;

                Character.MoveTo(targets);
            }
        }
    }

    
}
