using UnityEngine;
using System.Collections;

public class InventoryAnimator : MonoBehaviour
{
    public static InventoryAnimator Instance { get; private set; }

    public AnimationCurve AddItemCurve;
    public GameObject ToAnimate;
    public float Duration;

    private Coroutine AddCr;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Animate()
    {
        if (AddCr != null)
            StopCoroutine(AddCr);
        StartCoroutine(AddItemCoroutine());
    }

    private IEnumerator AddItemCoroutine()
    {
        Vector3 origScale = ToAnimate.transform.localScale;
        float time = Duration;
        while (time > 0)
        {
            float normTime = time / Duration;
            time -= Time.deltaTime;

            ToAnimate.transform.localScale = AddItemCurve.Evaluate(normTime) * origScale;

            yield return null;
        }

        ToAnimate.transform.localScale = origScale;
    }
}
