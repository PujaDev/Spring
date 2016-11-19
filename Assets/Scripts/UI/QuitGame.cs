using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour
{
    public GameObject panel;
    public RectTransform parent;

    public void QuitYes()
    {
        GameController.controller.Save();

#if UNITY_EDITOR
        // set the PlayMode to stop
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }

    public void QuitNo()
    {
        Destroy(panel);
        GameController.controller.isUI = false;
        GameController.controller.lastUITime = Time.time;
        if (GameController.controller.currentIcon != CursorIcon.NORMAL)
        {
            Cursor.SetCursor(GameController.controller.cursorIcons[(int)GameController.controller.currentIcon - 2], Vector2.zero, CursorMode.Auto);
        }
        else {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    public void QuitButton()
    {
        GameObject child = (GameObject)GameObject.Instantiate(panel);
        child.transform.SetParent(parent, false);
        GameController.controller.isUI = true;
    }
}