using UnityEngine;
using System.Collections;

public class GlowOverTime : MonoBehaviour
{
    public float Duration;
    public AnimationCurve Curve;

    private float elapsed;
    private SpriteRenderer sRenderer;

    // Use this for initialization
    void Start()
    {
        Debug.Log(sRenderer);
        sRenderer = GetComponent<SpriteRenderer>();
        Debug.Log(sRenderer.material);
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed > Duration)
            elapsed -= Duration;

        float percent = elapsed / Duration;
        float amount = Mathf.Clamp(Curve.Evaluate(percent), 0f, 1f);
        sRenderer.material.SetFloat("_Amount", amount);
    }
}
