using UnityEngine;
using System.Collections;
using Spine.Unity;

public class ShrineAnimator : MonoBehaviour
{
    private SkeletonAnimation skeletonAnim;

    void Start()
    {
        skeletonAnim = GetComponent<SkeletonAnimation>();
        skeletonAnim.AnimationState.SetAnimation(0, "no_lights", false);
        //MoneyIn();
    }

    public void MoneyIn() {
        skeletonAnim.AnimationState.SetAnimation(0, "lit_up", false).timeScale = 0.5f;
        Spine.TrackEntry entry = skeletonAnim.AnimationState.SetAnimation(1, "spirit_fly_out", false);
        entry.delay = skeletonAnim.skeleton.data.FindAnimation("lit_up").duration * 0.6f;
        entry.timeScale = 0.5f;
        entry.mixDuration = 0f;
        skeletonAnim.AnimationState.AddAnimation(0, "glow", true, -0.5f).timeScale = 0.5f;
        skeletonAnim.AnimationState.SetAnimation(2, "spirit_glow", false).delay = skeletonAnim.skeleton.data.FindAnimation("lit_up").duration;
        for (int i = 0; i < 5; i++)
        {
            skeletonAnim.AnimationState.AddAnimation(2, "spirit_glow", false, 0f).timeScale = 0.5f;
        }
    }
    
}
