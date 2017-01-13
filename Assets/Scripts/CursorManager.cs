using UnityEngine;
using System.Collections;

public class CursorManager : MonoBehaviour
{
    public CursorIcon icon;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    void OnMouseEnter()
    {
        GameController.Instance.currentIcon = icon;
        if (!GameController.Instance.isUI)
            Cursor.SetCursor(GameController.Instance.cursorIcons[(int)icon - 2], hotSpot, cursorMode);
    }
    void OnMouseExit()
    {
        GameController.Instance.currentIcon = CursorIcon.NORMAL;
        if (!GameController.Instance.isUI)
            Cursor.SetCursor(null, hotSpot, cursorMode);
    }
}
