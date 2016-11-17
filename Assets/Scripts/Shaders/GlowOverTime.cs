using UnityEngine;
using System.Collections;

public class GlowOverTime : MonoBehaviour
{
    public float Duration;

    private float elapsed;
    private SpriteRenderer sRenderer;
    private bool decay;

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
        if (decay)
            elapsed -= Time.deltaTime;
        else
            elapsed += Time.deltaTime;

        if (elapsed > Duration)
        {
            elapsed -= elapsed - Duration;
            decay = true;
        }
        else if (elapsed < 0.0f)
        {
            elapsed = -elapsed;
            decay = false;
        }

        float amount = elapsed / Duration;
        sRenderer.material.SetFloat("_Amount", amount);
    }
}
