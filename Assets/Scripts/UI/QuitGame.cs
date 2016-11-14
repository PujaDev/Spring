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
    }

    public void QuitButton()
    {
        GameObject child = (GameObject)GameObject.Instantiate(panel);
        child.transform.SetParent(parent, false);
    }
}