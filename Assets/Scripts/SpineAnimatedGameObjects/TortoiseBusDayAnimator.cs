using UnityEngine;
using System.Collections;
using Spine.Unity;

public class TortoiseBusDayAnimator : MonoBehaviour
{
    private SkeletonAnimation skeletonAnim;
    public Transform startPoint;
    public Transform middlePoint;
    public Transform endPoint;
    public BusTrigger trigger;
    public float Speed;

    void Start()
    {
        skeletonAnim = GetComponent<SkeletonAnimation>();

        //skeletonAnim.AnimationState.SetAnimation(0, "no_lights", false);
        //MoneyIn();
    }

    public void BusDeparts()
    {

    }

    public void BusArrives() {
        transform.position = startPoint.position;
        StartCoroutine(MoveToCoroutine(middlePoint.position));
    }

    IEnumerator MoveToCoroutine(Vector3 target)
    {
        skeletonAnim.AnimationState.SetAnimation(0, "walk", true).timeScale = 1f;

        while (Vector3.Distance(transform.position, target) > 8f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Speed * Time.deltaTime);
            yield return null;
        }

        trigger.CameraGoDown(); 

        while (Vector3.Distance(transform.position, target) > 4f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Speed * Time.deltaTime);
            yield return null;
        }

        skeletonAnim.AnimationState.SetAnimation(1, "breaks", false);

        while (Vector3.Distance(transform.position, target) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Speed * Time.deltaTime);
            yield return null;
        }

        skeletonAnim.AnimationState.SetAnimation(0, "idle", true);
        skeletonAnim.AnimationState.SetAnimation(1, "turn_to_talk", false);
    }
}
