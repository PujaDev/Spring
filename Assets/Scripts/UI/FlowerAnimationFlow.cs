using UnityEngine;
using System.Collections;
using Spine.Unity;

public class FlowerAnimationFlow : MonoBehaviour {

    private SkeletonGraphic skeletonGraphic;

    // Use this for initialization
    void Start () {
        skeletonGraphic = GetComponent<SkeletonGraphic>();
        skeletonGraphic.AnimationState.SetAnimation(0, "Grow_animation", false);
        skeletonGraphic.AnimationState.AddAnimation(0, "Idle_animation", true, 0f);
        skeletonGraphic.AnimationState.AddAnimation(1, "IdleLeaves_animation", true, 0f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
