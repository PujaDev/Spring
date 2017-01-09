using UnityEngine;
using System.Collections;

public class InventoryAnimator : MonoBehaviour
{
    public static InventoryAnimator Instance { get; private set; }

    public AnimationCurve AddItemCurve;
    public GameObject ToAnimate;
    public float Duration;

    private Coroutine AddCr;
    private Vector3 OriginalScale;

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

    private void Reset()
    {
        ToAnimate.transform.localScale = OriginalScale;
    }

    public void Animate()
    {
        if (AddCr != null)
        {
            Reset();
            StopCoroutine(AddCr);
        }
        StartCoroutine(AddItemCoroutine());
    }

    private IEnumerator AddItemCoroutine()
    {
        OriginalScale = ToAnimate.transform.localScale;
        float time = Duration;
        while (time > 0)
        {
            float normTime = time / Duration;
            time -= Time.deltaTime;

            ToAnimate.transform.localScale = AddItemCurve.Evaluate(normTime) * OriginalScale;

            yield return null;
        }

        ToAnimate.transform.localScale = OriginalScale;
        AddCr = null;
    }
}
