using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }
    public CharacterInput walkableArea;

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
            walkableArea = GetComponentInChildren<CharacterInput>(); // Return only the big area
            var subAreas = walkableArea.GetComponentsInChildren<Collider2D>();

            // Find in which sub-area the character is
            var character = GameObject.FindGameObjectWithTag("Character");
            // i == 0 -> the big collider in parent - but we want only the small sub-areas in childern
            for (int i = 1; i < subAreas.Length; i++)
            {
                if (subAreas[i].OverlapPoint(character.transform.position))
                {
                    // The index points to the array of small children areas - but we have the big one at index 0
                    currentAreaIndex = i - 1;
                    break;
                }
            }
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

}
