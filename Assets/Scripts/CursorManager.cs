using UnityEngine;
using System.Collections;

public class CursorManager : MonoBehaviour
{
    public CursorIcon icon;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    void OnMouseEnter()
    {
        GameController.controller.currentIcon = icon;
        if (!GameController.controller.isUI)
            Cursor.SetCursor(GameController.controller.cursorIcons[(int)icon - 2], hotSpot, cursorMode);
    }
    void OnMouseExit()
    {
        GameController.controller.currentIcon = CursorIcon.NORMAL;
        if (!GameController.controller.isUI)
            Cursor.SetCursor(null, hotSpot, cursorMode);
    }
}
