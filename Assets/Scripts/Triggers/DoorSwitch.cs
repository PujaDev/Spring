using UnityEngine;
using System.Collections;
using System;
using Spine.Unity;

public class DoorSwitch : IChangable {
    public GameObject[] objs;
    public void OpenDoor()
    {
        objs[0].SetActive(true);
        objs[0].GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "open", false);
    }
    public void CloseDoor()
    {
        objs[0].SetActive(true);
        objs[0].GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "wave", false).mixDuration = 0f;
        objs[0].GetComponent<SkeletonAnimation>().AnimationState.AddEmptyAnimation(0, 0.5f,0);
    }
    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        if (!newState.HubaBus.isOutOfTheHouse)
        {
            objs[0].SetActive(true);
            objs[1].SetActive(false);
        }
        else
        {
            objs[0].SetActive(false);
            objs[1].SetActive(true);
        }
    }
}
