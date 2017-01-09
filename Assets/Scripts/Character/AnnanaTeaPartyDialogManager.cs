using UnityEngine;
using System.Collections;

public class AnnanaTeaPartyDialogManager : DialogManager
{
    public GameObject BubbleObject;
    BubbleManager bubble;

    public void Awake()
    {
        base.Awake();
        bubble = BubbleObject.GetComponent<BubbleManager>();
    }

    public enum DialogueTypes
    {
        Poisonous = 0,
        Edible = 1,
        Swearing = 2
    }

    override public void Next()
    {
        if (currentLine > 0)
            return;
        switch (currentDialogue)
        {
            case 0: // thirsty for tea
                bubble.image_names = new string[] { "Bubble__thirst", "Bubble__tea" };                        
                bubble.PlayImages(3.2f, BubbleManager.BubbleType.THINK);
                break;

            case 1: // tea was good - happy
                bubble.image_names = new string[] { "Bubble__happy"};
                bubble.PlayImages(3.2f, BubbleManager.BubbleType.THINK);
                break;

            case 2: // tea sucked ass - disgusted
                bubble.image_names = new string[] { "Bubble__disgusted" };
                bubble.PlayImages(3.2f, BubbleManager.BubbleType.THINK);
                break;
        }
        currentLine++;
    }
}
