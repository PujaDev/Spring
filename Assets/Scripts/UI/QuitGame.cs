using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour
{
    public GameObject panel;
    public RectTransform parent;

    public void QuitYes()
    {
        GameController.Instance.Save();

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
        GameController.Instance.isUI = false;
        GameController.Instance.lastUITime = Time.time;
        if (GameController.Instance.currentIcon != CursorIcon.NORMAL)
        {
            Cursor.SetCursor(GameController.Instance.cursorIcons[(int)GameController.Instance.currentIcon - 2], Vector2.zero, CursorMode.Auto);
        }
        else {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    public void QuitButton()
    {
        GameObject child = (GameObject)GameObject.Instantiate(panel);
        child.transform.SetParent(parent, false);
        GameController.Instance.isUI = true;
    }
}