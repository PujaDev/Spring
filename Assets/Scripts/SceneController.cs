using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public static SceneController controller = null;

    public int currentArea = 0;
    public int targetArea = 0;
    public float startPositionY;
    public float scaleParam = 0f;
    public float defaultCharactecScale = 0f;
    public Text title;

    void Awake()
    {
        if (controller == null)
        {
            controller = this;
        }
        else if (controller != this)
        {
            Destroy(gameObject);
        }
    }

}
