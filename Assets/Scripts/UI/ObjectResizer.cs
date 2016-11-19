using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObjectResizer : MonoBehaviour {

    public AnimationCurve scaleCurve;
    public float duration = 0.5f;
    public float defaultScale = 0.3f;
    public float enlargement = 0.5f;
    private bool enlarged = false;
    public Text title;

    public void ResizeObject()
    {
        StopCoroutine(Resize());
        enlarged = !enlarged;
        StartCoroutine(Resize());
    }
    IEnumerator Resize()
    {
        float time = 0f;
        while (time <= 1f)
        {
            float scale = scaleCurve.Evaluate(time);
            time += (Time.deltaTime / duration);
            Color alpha = title.color;
            if (enlarged)
            {
                alpha.a = 1f - scale;
                scale = defaultScale + scale * enlargement;
            }else
            {
                alpha.a = scale;
                scale = defaultScale + enlargement - scale*enlargement;
            }

            title.color = alpha;

            Vector3 localScale = transform.localScale;
            localScale.x = scale;
            localScale.y = scale;
            transform.localScale = localScale;
            yield return new WaitForFixedUpdate();
        }
    }
}
