using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using Fungus;

public class AnnanasOwlAnimator : MonoBehaviour {
    private SkeletonAnimation skeletonAnim;
    //public float SpeedFly;
    //public AnimationCurve Curve;

    private Coroutine idleHead;
    private Coroutine idleWings;

    //Debug 
    public bool fly = false;
    public Transform[] path;
    public float percentsPerSecond = 0.05f; // %2 of the path moved per second
    public float currentPathPercent = 0.0f; //min 0, max 1
    public float scaleParam;


    IEnumerator IdleHeadCoroutine() {
        float randomDelay;
        while (true)
        {
            randomDelay = Random.Range(2f, 5f);
            yield return new WaitForSeconds(randomDelay);
            skeletonAnim.AnimationState.AddAnimation(1, "idle_head", false, 0);
        }
    }

    IEnumerator IdleWingsCoroutine()
    {
        float randomDelay;
        while (true)
        {
            randomDelay = Random.Range(4f, 8f);
            yield return new WaitForSeconds(randomDelay);
            skeletonAnim.AnimationState.SetAnimation(0, "idle_wings", false);
            skeletonAnim.AnimationState.AddAnimation(0, "idle_breathing", true, 0);
        }
    }

    IEnumerator FlyToCoroutine()
    {
        if (idleHead != null)
            StopCoroutine(idleHead);
        if (idleWings != null)
            StopCoroutine(idleWings);

        skeletonAnim.AnimationState.SetAnimation(0, "start_flight", false);
        skeletonAnim.AnimationState.AddAnimation(0, "flying", true, 0).timeScale = 4f;

        Spine.ExposedList<Spine.Animation> allAnim = skeletonAnim.skeleton.data.animations;
        foreach (var item in allAnim) {
            if (item.name == "start_flight") {
                yield return new WaitForSeconds(item.duration);
                break;
            }
        }
               
        while (Vector3.Distance(transform.position, path[path.Length - 1].position) > 0.05f)
        {
            currentPathPercent += percentsPerSecond * Time.deltaTime;
            iTween.PutOnPath(gameObject, path, currentPathPercent);
            float tmp = transform.localScale.x - (scaleParam * Time.deltaTime);
            transform.localScale = new Vector3(tmp, tmp, tmp);

            yield return null;
        }

        skeletonAnim.AnimationState.SetEmptyAnimation(0, 1f);
    }

    public void Fly() {
        StartCoroutine(FlyToCoroutine());
    }

    public void PackageAppear()
    {
        skeletonAnim.AnimationState.SetAnimation(2, "package_appear", false);
    }

    // Use this for initialization
    void Start()
    {
        skeletonAnim = GetComponent<SkeletonAnimation>();
        skeletonAnim.AnimationState.SetAnimation(0, "idle_breathing", true);
        idleHead = StartCoroutine(IdleHeadCoroutine());
        idleWings = StartCoroutine(IdleWingsCoroutine());
    }

    void Update() {
        if (fly) {
            fly = false;
            StartCoroutine(FlyToCoroutine());
        }
    }

}
