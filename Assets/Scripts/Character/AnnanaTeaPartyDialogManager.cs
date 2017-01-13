using UnityEngine;
using System.Collections;
using Spine.Unity;
using Spine;

public class AnnanaTeaPartyDialogManager : DialogManager
{
    
    public GameObject BubbleObject;
    Skeleton characterSkeleton;
    BubbleManager bubble;

    public void Awake()
    {
        base.Awake();
        bubble = BubbleObject.GetComponent<BubbleManager>();
        characterSkeleton = GameObject.FindGameObjectWithTag("Character").GetComponent<SkeletonAnimation>().skeleton;
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
        {
            if (currentDialogue == 3 || currentDialogue == 4)
            {
                StateManager.Instance.DispatchAction(new SpringAction(ActionType.FINISH_READING_FINE));
                characterSkeleton.flipX = true;
            }
            return;
        }
        switch (currentDialogue)
        {
            case 0: // thirsty for tea
                bubble.image_names = new string[] { "Bubble__thirst", "Bubble__tea" };                        
                bubble.PlayImages(3.2f, BubbleManager.BubbleType.THINK);
                break;

            case 1: // tea was good - happy
                bubble.image_names = new string[] { "Bubble__happy"};
                bubble.PlayImages(4f, BubbleManager.BubbleType.THINK);
                break;

            case 2: // tea sucked ass - disgusted
                bubble.image_names = new string[] { "Bubble__disgusted" };
                bubble.PlayImages(4f, BubbleManager.BubbleType.THINK);
                break;

            case 3: // fined - angry
                bubble.image_names = new string[] { "Bubble__fine", "Bubble__money_no", "Bubble__angry" };
                bubble.PlayImages(3.2f, BubbleManager.BubbleType.THINK);
                break;

            case 4: // fined - surprised - sad
                bubble.image_names = new string[] { "Bubble__fine", "Bubble__shocked", "Bubble__money_no", "Bubble__sad" };
                bubble.PlayImages(2.5f, BubbleManager.BubbleType.THINK);
                break;
        }
        currentLine++;
    }
}
