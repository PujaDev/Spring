using UnityEngine;
using System.Collections;
using Spine.Unity;

public class AnnanaTeaPartySSC : SceneSwitchControler
{
    public GameObject Note;

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        // Initialization
        if (oldState == null)
        {
            Note.SetActive(!newState.AnnanaHouse.IsAddressPickedUp);
            GameObject.FindGameObjectWithTag("Character").GetComponent<SkeletonAnimation>().skeleton.SetSkin(newState.AnnanaHouse.AnnanaDress);
        }

        if (newState.AnnanaTeaParty.IsInside && (oldState == null || !oldState.AnnanaTeaParty.IsInside))
        {
            // Full animation while playing the game
            if (oldState != null)
            {
                Flowchart.SendFungusMessage("GoIn");
            }
            // Load saved game state
            else // oldState == null
            {
                Flowchart.SendFungusMessage("GoInSwitch");
            }
        }

        if (newState.AnnanaTeaParty.IsOutside && (oldState == null || !oldState.AnnanaTeaParty.IsOutside))
        {
            // Full animation while playing the game
            if (oldState != null)
            {
                Flowchart.SendFungusMessage("GoOut");
            }
            // Load saved game state
            else // oldState == null
            {
                Flowchart.SendFungusMessage("GoOutSwitch");
            }

        }
    }
}
