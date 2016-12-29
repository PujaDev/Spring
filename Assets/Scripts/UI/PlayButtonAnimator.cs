using UnityEngine;
using System.Collections;
using Spine.Unity;

public class PlayButtonAnimator : MonoBehaviour {

    public UIMainMenuController mainMenuController;
    private SkeletonGraphic skeletonGraphic;

    void Start()
    {
        skeletonGraphic = GetComponent<SkeletonGraphic>();
        skeletonGraphic.AnimationState.SetAnimation(0, "breakApart", false).TrackTime = skeletonGraphic.SkeletonData.FindAnimation("breakApart").duration;
        skeletonGraphic.AnimationState.AddEmptyAnimation(0, 0.5f, 0f);
    }

    public void PlayLevels() {
        StopCoroutine(PlaySwitch());
        StartCoroutine(PlaySwitch());
    }

    IEnumerator PlaySwitch() {
        //anim
        skeletonGraphic.AnimationState.SetAnimation(0, "breakApart", false);
        yield return new WaitForSeconds(skeletonGraphic.SkeletonData.FindAnimation("breakApart").duration - 0.5f);
        mainMenuController.menuTransition(2);
        skeletonGraphic.AnimationState.AddEmptyAnimation(0, 1f, 0f);
    }
    
}
