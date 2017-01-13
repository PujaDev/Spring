using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using System;

public interface IMoveable
{
    void MoveTo(List<Vector3> targets, SpringAction action, IInteractable source);
}

public class CharacterMovement : IChangable, IMoveable {

    public float Speed;
    public float AnimationSpeed = 1f;
    public AnimationCurve Curve;

    protected bool busy = false;
    protected Coroutine move;
    protected SkeletonAnimation skeletonAnim;

    public bool Busy
    {
        get { return busy; }
        set
        {
            busy = value;
        }
    }
    // Use this for initialization
    override protected void Start ()
    {
        base.Start();
        ScaleCharacter();
        SceneController.Instance.InitCharArea();
    }
    virtual protected void Awake()
    {
        skeletonAnim = gameObject.GetComponent<SkeletonAnimation>();
    }
    public void MoveTo(List<Vector3> targets, SpringAction action, IInteractable source)
    {
        if (!Busy)
        {
            if (move != null)
                StopCoroutine(move);
            
            move = StartCoroutine(MoveToCoroutine(targets, action, source));
        }
    }

    protected virtual IEnumerator MoveToCoroutine(List<Vector3> targets, SpringAction action, IInteractable source)
    {

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

        if (source != null && source.orientation != Orientation.UNSPECIFIED) {
            if(source.orientation == Orientation.LEFT) skeletonAnim.skeleton.FlipX = true;
            else skeletonAnim.skeleton.FlipX = false;
        }

        skeletonAnim.AnimationState.SetAnimation(0, "idle", true);

        //calls action after reaching the destination
        if (action != null)
            StateManager.Instance.DispatchAction(action, source);

    }

    /// <summary>
    /// Scale character based on its y distance from start position
    /// </summary>
    protected void ScaleCharacter()
    {
        float scaleChar = (1 - SceneController.Instance.scaleParam * (transform.position.y - SceneController.Instance.startPositionY)) * SceneController.Instance.defaultCharactecScale;
        transform.localScale = new Vector3(scaleChar, scaleChar, scaleChar);
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        if (move == null && skeletonAnim.AnimationName != "idle")
            skeletonAnim.AnimationState.SetAnimation(0, "idle", true);
    }
}
