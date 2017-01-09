using UnityEngine;
using System.Collections;

public class DialogManager : MonoBehaviour
{
    public int currentDialogue = 0;
    public int currentLine = 0;

    public static DialogManager Instance { get; private set; }

    public GameObject[] characters;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    virtual public void SetDialogue(int dialogID) {
        currentDialogue = dialogID;
        currentLine = 0;
    }
    virtual public void Next() {}
}
