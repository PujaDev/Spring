using UnityEngine;
using System.Collections;
using Spine.Unity;
using System;

public class TortoiseBusDayAnimator : MonoBehaviour
{
    private SkeletonAnimation skeletonAnim;
    public Transform startPoint;
    public Transform middlePoint;
    public Transform endPoint;
    public BusTrigger trigger;
    public float Speed;


    public void Start()
    {
        skeletonAnim = GetComponent<SkeletonAnimation>();
    }
    
    public void BusDeparts()
    {
        StartCoroutine(LeaveToCoroutine(endPoint.position));
    }
    public void NoPoison() {
        skeletonAnim.AnimationState.SetAnimation(1, "no_poison", false);
    }

    public void WakeUpLizard()
    {
        skeletonAnim.AnimationState.SetAnimation(1, "wake_up_lizard", false);
        skeletonAnim.AnimationState.AddAnimation(1, "lizard_move", false, 0);
    }

    public void BusArrives() {
        transform.position = startPoint.position;
        StartCoroutine(MoveToCoroutine(middlePoint.position));
    }

    public void BusGoesOn()
    {
        transform.position = startPoint.position;
        StartCoroutine(MoveToCoroutine(endPoint.position));
    }

    IEnumerator MoveToCoroutine(Vector3 target)
    {
        bool passes = (StateManager.Instance.State.HubaBus.isDrunk && StateManager.Instance.State.AnnanaHouse.OwlPackage == (int)AnnanaInventory.ItemIds.Invis) ? true:false;
        skeletonAnim.AnimationState.SetAnimation(0, "walk", true).timeScale = 1f;

        while (Vector3.Distance(transform.position, target) > 4f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Speed * Time.deltaTime);
            yield return null;
        }

        if(!passes)
            skeletonAnim.AnimationState.SetAnimation(1, "breaks", false);

        while (Vector3.Distance(transform.position, target) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Speed * Time.deltaTime);
            yield return null;
        }

        if (passes) {
            skeletonAnim.AnimationState.SetEmptyAnimations(1f);
            StateManager.Instance.DispatchAction(new SpringAction(ActionType.DEPARTURE, "Bus has left"));
        }
        else
        {
            skeletonAnim.AnimationState.SetAnimation(0, "idle", true);
            skeletonAnim.AnimationState.SetAnimation(1, "turn_to_talk", false);
            StateManager.Instance.DispatchAction(new SpringAction(ActionType.ARRIVAL, "Bus Arrived"));
        }
    }

    IEnumerator LeaveToCoroutine(Vector3 target)
    {
        if (skeletonAnim.AnimationName != "idle")
            skeletonAnim.AnimationState.SetAnimation(0, "idle", true);

        skeletonAnim.AnimationState.SetAnimation(1, "turn_to_ride", false);

        yield return new WaitForSeconds(skeletonAnim.skeleton.data.FindAnimation("turn_to_ride").duration);

        skeletonAnim.AnimationState.SetAnimation(0, "walk", true).timeScale = 1f;

        while (Vector3.Distance(transform.position, target) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Speed * Time.deltaTime);
            yield return null;
        }

        StateManager.Instance.DispatchAction(new SpringAction(ActionType.DEPARTURE, "Bus has left"));
    }
    public void Arrived()
    {
        transform.position = middlePoint.position;
        if (skeletonAnim.AnimationName != "idle")
        {
            skeletonAnim.AnimationState.SetAnimation(0, "idle", true);
            skeletonAnim.AnimationState.SetAnimation(1, "turn_to_talk", false);
        }
    }

    public void WaitToGetPaid()
    {
        skeletonAnim.AnimationState.SetAnimation(1, "wake_up_lizard", false);
        skeletonAnim.AnimationState.AddAnimation(1, "lizard_move", false, 0);
    }

    public void GetPaid()
    {
        skeletonAnim.AnimationState.SetAnimation(1, "Lizard_tongue", false).mixDuration = 0f;
    }
}
