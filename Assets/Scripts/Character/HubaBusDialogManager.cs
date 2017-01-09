using UnityEngine;
using System.Collections;

public class HubaBusDialogManager : DialogManager
{
    public enum CharacterTypes {
        Huba = 0,
        Driver = 1
    }

    public enum DialogueTypes
    {
        Poisonous = 0,
        Edible = 1,
        Swearing = 2
    }

    override public void Next() {
        BubbleManager bubble;
        switch (currentDialogue) {
            case 0: //huba and driver -> poisonous
                switch (currentLine) {
                    case 0:
                        bubble = characters[(int)CharacterTypes.Huba].GetComponentInChildren<BubbleManager>();
                        bubble.image_names =  new string[]{ "Bubble__map", "Bubble__question"};
                        bubble.PlayImages();
                        break;
                    case 1:
                        bubble = characters[(int)CharacterTypes.Driver].GetComponentInChildren<BubbleManager>();
                        bubble.image_names = new string[] { "Bubble__poison" };
                        bubble.PlayImages(2f);
                        break;

                }
                break;
            case 1: //huba and driver -> edible
                switch (currentLine)
                {
                    case 0:
                        bubble = characters[(int)CharacterTypes.Huba].GetComponentInChildren<BubbleManager>();
                        bubble.image_names = new string[] { "Bubble__map", "Bubble__question" };
                        bubble.PlayImages();
                        break;
                    case 1:
                        bubble = characters[(int)CharacterTypes.Driver].GetComponentInChildren<BubbleManager>();
                        bubble.image_names = new string[] { "Bubble__map", "Bubble__ok" };
                        bubble.PlayImages();
                        break;
                    case 2:
                        bubble = characters[(int)CharacterTypes.Huba].GetComponentInChildren<BubbleManager>();
                        bubble.image_names = new string[] { "Bubble__get_on", "Bubble__map", "Bubble__shrine", "Bubble__question"};
                        bubble.PlayImages();
                        break;
                    case 3:
                        bubble = characters[(int)CharacterTypes.Driver].GetComponentInChildren<BubbleManager>();
                        bubble.image_names = new string[] { "Bubble__shrine", "Bubble__equation", "Bubble__money" };
                        bubble.PlayImages();
                        break;
                    case 4:
                        bubble = characters[(int)CharacterTypes.Huba].GetComponentInChildren<BubbleManager>();
                        bubble.image_names = new string[] { "Bubble__ok"};
                        bubble.PlayImages();
                        break;

                }
                break;
            case 2: //huba and driver -> swearing
                switch (currentLine)
                {
                    case 0:
                        bubble = characters[(int)CharacterTypes.Huba].GetComponentInChildren<BubbleManager>();
                        bubble.image_names = new string[] { "Bubble__swore_words", "Bubble__bleeh", "Bubble__heart", "Bubble__black_cloud" };
                        bubble.PlayImages(0.5f, BubbleManager.BubbleType.THINK);
                        break;
                }
                break;
        }
        currentLine++;
    }
}
