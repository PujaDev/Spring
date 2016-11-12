using UnityEngine;
using System.Collections;
using Spine.Unity;

public interface IMoveable
{
    void MoveTo(Vector3 target);
    Vector3 Position { get; }
}

public class CharacterMovement : MonoBehaviour, IMoveable {

    public float Speed;
    public AnimationCurve Curve;
    public Vector3 Position { get { return transform.position; } }

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

    public void MoveTo(Vector3 target)
    {
        if (!busy)
        {
            if (move != null)
                StopCoroutine(move);

            Vector3 origin = transform.position;
            move = StartCoroutine(MoveToCoroutine(origin, target));
        }
    }

    IEnumerator MoveToCoroutine(Vector3 origin, Vector3 target)
    {
        if (origin.x < target.x) // Going right
        {
            skeletonAnim.skeleton.FlipX = false;
        }
        else // Going left
        {
            skeletonAnim.skeleton.FlipX = true;
        }

        // Can we continue already started move animation?
        if (skeletonAnim.AnimationName != "walk")
            skeletonAnim.AnimationState.SetAnimation(0, "walk", true);

        while (Vector3.Distance(transform.position, target) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Speed * Time.deltaTime);
            yield return null;
        }

        skeletonAnim.AnimationState.SetAnimation(0, "idle", true);
    }
}
