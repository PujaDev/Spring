using UnityEngine;
using System.Collections;
using Spine.Unity;

public class SkyAnimator : MonoBehaviour {
    private SkeletonAnimation skeletonAnim;

    // Use this for initialization
    void Start()
    {
        skeletonAnim = GetComponent<SkeletonAnimation>();
        skeletonAnim.AnimationState.SetAnimation(0, "clouds", true);
        skeletonAnim.AnimationState.SetAnimation(1, "manta_1", true);
        skeletonAnim.AnimationState.SetAnimation(2, "manta_2", true);
        skeletonAnim.AnimationState.SetAnimation(3, "manta_3", true);
    }
}
