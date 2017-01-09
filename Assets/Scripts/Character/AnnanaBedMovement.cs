using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using Spine;
using System;

public class AnnanaBedMovement : IChangable
{
    public GameObject WalkableArea;
    public GameObject Dresscover;
    GameObject Character;
    AnnanaCharacterMovement Movement;
    SkeletonAnimation skeletonAnim;
    bool wokeUp;

    void Awake()
    {
        skeletonAnim = gameObject.GetComponent<SkeletonAnimation>();
        Character = GameObject.FindGameObjectWithTag("Character");
    }

    protected override void Start()
    {
        base.Start();
        if(!wokeUp)
            skeletonAnim.AnimationState.SetAnimation(0, "full", false);
    }

    public Spine.AnimationState GetOutAnimation()
    {
        skeletonAnim.AnimationState.SetAnimation(0, "empty", false);
        return skeletonAnim.AnimationState;
    }

    public Spine.AnimationState MoveHandAnimation()
    {
        skeletonAnim.AnimationState.SetAnimation(0, "hand", false);
        return skeletonAnim.AnimationState;
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        wokeUp = newState.AnnanaHouse.AlarmTurnedOff;

        WalkableArea.SetActive(wokeUp);
        Dresscover.SetActive(wokeUp);
        Character.SetActive(wokeUp);
        GameController.Instance.CanCharacterMove = wokeUp;

        if (wokeUp)
            GetOutAnimation();
    }
}
