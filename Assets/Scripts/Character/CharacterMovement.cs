using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;

public interface IMoveable
{
    void MoveTo(List<Vector3> targets);
}

public class CharacterMovement : MonoBehaviour, IMoveable {

    public float Speed;
    public AnimationCurve Curve;

    private bool busy;
    private Coroutine move;
    private SkeletonAnimation skeletonAnim;

    // Use this for initialization
    void Start ()
    {
        busy = false;
        skeletonAnim = gameObject.GetComponent<SkeletonAnimation>();
        skeletonAnim.AnimationState.SetAnimation(0, "idle", true);
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    public void MoveTo(List<Vector3> targets)
    {
        if (!busy)
        {
            if (move != null)
                StopCoroutine(move);
            
            move = StartCoroutine(MoveToCoroutine(targets));
        }
    }

    IEnumerator MoveToCoroutine(List<Vector3> targets)
    {
        // Can we continue already started move animation?
        if (skeletonAnim.AnimationName != "walk")
            skeletonAnim.AnimationState.SetAnimation(0, "walk", true);

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
                float scaleChar = (1 - GameController.controller.scaleParam * (transform.position.y - GameController.controller.startPositionY)) * GameController.controller.defaultCharactecScale; 
                transform.localScale = new Vector3(scaleChar, scaleChar, scaleChar); 
                yield return null;
            }
            int diff = GameController.controller.targetArea - GameController.controller.currentArea;
            if (diff != 0)
            {
                if (diff > 0) {
                    GameController.controller.currentArea++;
                } else {
                    GameController.controller.currentArea--;
                }
            }
            targets.RemoveAt(0);
        }
        
    skeletonAnim.AnimationState.SetAnimation(0, "idle", true);
    }
}
