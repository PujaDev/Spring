using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float Duration;
    public float Magnitude;
    public float Speed;
    public Collider2D Area;

    private Coroutine shake;

    public void Update()
    {
        var mousePos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos3D.x, mousePos3D.y); // Cast the mouse position to 2D

        if (Input.GetMouseButtonUp(0) && Area.bounds.Contains(mousePos2D))
        {
            if (shake != null)
                StopCoroutine(shake);
            shake = StartCoroutine(Shake());
        }
    }

    private IEnumerator Shake()
    {
        float elapsed = 0.0f;

        Vector3 originalCamPos = Camera.main.transform.position;

        while (elapsed < Duration)
        {
            elapsed += Time.deltaTime;

            float percentComplete = elapsed / Duration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            // map value to [-1, 1]
            float x = Mathf.PerlinNoise(Time.time * Speed, 0.0f) * 2.0f - 1.0f;
            float y = Mathf.PerlinNoise(Time.time * Speed, 0.0f) * 2.0f - 1.0f;

            x *= Magnitude * damper;
            y *= Magnitude * damper;

            Camera.main.transform.position = new Vector3(x + originalCamPos.x, y + originalCamPos.y, originalCamPos.z);

            yield return null;
        }

        Camera.main.transform.position = originalCamPos;
    }
}

