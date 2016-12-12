using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }
    public CharacterInput [] walkableAreas;

    public int currentSectionAreaIndex;
    public int currentAreaIndex;
    public int targetAreaIndex;
    public float startPositionY;
    public float scaleParam;
    public float defaultCharactecScale;
    public Text title;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitCharArea();
            // switch between section areas
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void InitCharArea()
    {
        var character = GameObject.FindGameObjectWithTag("Character");
        InitAreaForPos(character.transform.position);
    }

    public void InitAreaForPos(Vector3 pos)
    {
        currentAreaIndex = -1;
        currentSectionAreaIndex = -1;
        //walkableArea = GetComponentInChildren<CharacterInput>(); // Return only the big area
        for (int i = 0; i < walkableAreas.Length; i++)
        {
            var subAreas = walkableAreas[i].GetComponentsInChildren<Collider2D>();

            // i == 0 -> the big collider in parent - but we want only the small sub-areas in childern
            for (int j = 1; j < subAreas.Length; j++)
            {
                if (subAreas[j].OverlapPoint(pos))
                {
                    // The index points to the array of small children areas - but we have the big one at index 0
                    currentAreaIndex = j - 1;
                    currentSectionAreaIndex = i;
                    break;
                }
            }
            if (currentSectionAreaIndex != -1) break;
        }
    }
}
