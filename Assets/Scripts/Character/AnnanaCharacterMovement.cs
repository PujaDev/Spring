using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using Spine;

public class AnnanaCharacterMovement : CharacterMovement, IMoveable {
    
    private Coroutine idleTail;
    private Coroutine idleHand;
    private bool TeaInHand;

    // Use this for initialization
    override protected void Start ()
    {
        base.Start();
        idleTail = StartCoroutine(IdleTailCoroutine());
        idleHand = StartCoroutine(IdleHandCoroutine());
    }

    public Spine.AnimationState DrinkTeaAnimation()
    {
        TeaInHand = true;
        skeletonAnim.AnimationState.SetAnimation(2, "tea_drink", false);
        return skeletonAnim.AnimationState;
    }

    public Spine.AnimationState ThrowTeaAnimation()
    {
        TeaInHand = false;// :(
        skeletonAnim.AnimationState.SetAnimation(0, "tea_throw", false);
        skeletonAnim.AnimationState.SetAnimation(1, "tea_throw", false);
        skeletonAnim.AnimationState.SetAnimation(2, "tea_throw", false);
        return skeletonAnim.AnimationState;
    }

    public Spine.AnimationState TeaInHandAnimation()
    {
        skeletonAnim.AnimationState.AddAnimation(2, "tea_in_hand", true, 1f);
        return skeletonAnim.AnimationState;
    }

    IEnumerator IdleHandCoroutine()
    {
        float randomDelay;
        while (true)
        {
            randomDelay = Random.Range(8f, 15f);
            yield return new WaitForSeconds(randomDelay);
            if (!TeaInHand)
            {
                if (Random.Range(0f, 1f) < 0.5f)
                {
                    skeletonAnim.AnimationState.AddAnimation(2, "idle_hand_scratch", false, 0);
                    skeletonAnim.AnimationState.AddAnimation(2, "idle_hand_scratch", false, 0);
                }
                else skeletonAnim.AnimationState.AddAnimation(2, "idle_hand_hair", false, 0);
            }
        }
    }

    IEnumerator IdleTailCoroutine()
    {

        float randomDelay;
        while (true)
        {
            randomDelay = Random.Range(3f, 8f);
            yield return new WaitForSeconds(randomDelay);
            if (!TeaInHand)
                skeletonAnim.AnimationState.AddAnimation(1, "idle_tail", false, 0);

        }
    }
    protected override IEnumerator MoveToCoroutine(List<Vector3> targets, SpringAction action, IInteractable source)
    {
        if (idleTail != null)
            StopCoroutine(idleTail);
        if (idleHand != null)
            StopCoroutine(idleHand);

        // Can we continue already started move animation?
        if (skeletonAnim.AnimationName != "walk")
            skeletonAnim.AnimationState.SetAnimation(0, "walk", true).timeScale = AnimationSpeed;

        while (targets.Count > 0)
        {
            if (transform.position.x < targets[0].x) // Going right
            {
                skeletonAnim.skeleton.FlipX = false;
            }
            else // Going left
            {
                skeletonAnim.skeleton.FlipX = true;
            }
            while (Vector3.Distance(transform.position, targets[0]) > 0.05f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targets[0], Speed * Time.deltaTime);
                ScaleCharacter();
                yield return null;
            }
            //update current area while moving
            int diff = SceneController.Instance.targetAreaIndex - SceneController.Instance.currentAreaIndex;
            if (diff != 0)
            {
                if (diff > 0) {
                    SceneController.Instance.currentAreaIndex++;
                } else {
                    SceneController.Instance.currentAreaIndex--;
                }
            }
            targets.RemoveAt(0);
        }

        if (source != null && source.orientation != Orientation.UNSPECIFIED)
        {
            if (source.orientation == Orientation.LEFT) skeletonAnim.skeleton.FlipX = true;
            else skeletonAnim.skeleton.FlipX = false;
        }

        skeletonAnim.AnimationState.SetAnimation(0, "idle", true);

        idleTail = StartCoroutine(IdleTailCoroutine());
        idleHand = StartCoroutine(IdleHandCoroutine());

        //calls action after reaching the destination
        if (action != null)
            StateManager.Instance.DispatchAction(action, source);

    }
}
