using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using Fungus;

public class HubaOwlAnimator : MonoBehaviour
{
    private SkeletonAnimation skeletonAnim;
    
    public Transform[] path;
    public float percentsPerSecond = 0.05f; // %2 of the path moved per second
    public float currentPathPercent = 0.0f; //min 0, max 1
    public float scaleParam;
    public PackagedElixir package;
    

    IEnumerator FlyToCoroutine()
    {
        skeletonAnim.AnimationState.AddAnimation(0, "flying", true, 0).timeScale = 4f;
        
        while (Vector3.Distance(transform.position, path[path.Length - 1].position) > 0.05f && currentPathPercent < 1f)
        {
            currentPathPercent += percentsPerSecond * Time.deltaTime;
            iTween.PutOnPath(gameObject, path, currentPathPercent);
            float tmp = transform.localScale.x - (scaleParam * Time.deltaTime);
            transform.localScale = new Vector3(tmp, tmp, tmp);
            if (currentPathPercent > 0.5f && currentPathPercent < 0.52f)
            {
                skeletonAnim.AnimationState.SetAnimation(1, "package_disappear", false);
                package.TogglePackage(true);
            }
            yield return null;
        }

        skeletonAnim.AnimationState.SetEmptyAnimation(0, 1f);
        StateManager.Instance.DispatchAction(new SpringAction(ActionType.DELIVERY, "Was delivered"));
        Destroy(gameObject);
    }

    public void Fly() {
        skeletonAnim.AnimationState.SetAnimation(1, "package_appear", false);
        StartCoroutine(FlyToCoroutine());
    }

    // Use this for initialization
    void Start()
    {
        skeletonAnim = GetComponent<SkeletonAnimation>();
    }

}
