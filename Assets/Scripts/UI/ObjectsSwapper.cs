using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObjectsSwapper : MonoBehaviour {

    public bool forSprites = false;
    public AnimationCurve scaleCurve;
    public float duration = 0.5f;
    private float minScale = 2;
    private bool swapped = false;
    public int index = 1;

    public void SwapObject(int id = 1)
    {
        index = id;
        StopCoroutine(Swap());
        minScale = 2;
        swapped = false;
        StartCoroutine(Swap());
    }
    IEnumerator Swap()
    {
        float time = 0f;
        while (time <= 1f)
        {
            float scale = scaleCurve.Evaluate(time);
            time += (Time.deltaTime / duration);
            if (!swapped) {
                if (minScale > scale) {
                    minScale = scale;
                } else {
                    swapped = true;
                    if(forSprites)
                        GetComponent<SwitchSprites>().next();
                    else
                        GetComponent<SwitchObjects>().changeToIndex(index);
                }
            }

            Vector3 localScale = transform.localScale;
            localScale.x = scale;
            localScale.y = scale;
            transform.localScale = localScale;
            yield return new WaitForFixedUpdate();
        }
    }
}
