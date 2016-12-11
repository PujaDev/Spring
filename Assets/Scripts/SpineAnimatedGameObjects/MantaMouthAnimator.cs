using UnityEngine;
using System.Collections;
using Spine.Unity;


public class MantaMouthAnimator : MonoBehaviour {
    private SkeletonAnimation skeletonAnim;

    // Use this for initialization
    void Start()
    {
        skeletonAnim = GetComponent<SkeletonAnimation>();
        skeletonAnim.AnimationState.SetAnimation(0, "clothes", true);
        skeletonAnim.AnimationState.SetAnimation(1, "swing_houses", true);
    }
}
