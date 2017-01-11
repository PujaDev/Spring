using UnityEngine;
using System.Collections;

public class SpriteScroll : MonoBehaviour {
    public float speedX;
    public float initialOffsetX = 0f;
    public bool firstOn = true;

    public float SpeedY = 3f;
    public float Amplitude = 1f;
    public AnimationCurve curve;

    private float width;
    private float height;
    private Vector3 initialPosition;

    void Start () {
        width = GetComponent<SpriteRenderer>().bounds.size.x;
        width -= 0.1f;
        initialPosition = transform.position + new Vector3(width, 0, 0);
        if (firstOn)
            initialOffsetX += width;
        width = width * 2;
    }

    void Update()
    {
        float offset = (initialOffsetX + Time.time * speedX) % width;
        height = curve.Evaluate((Time.time % SpeedY) / SpeedY) * Amplitude;
        transform.position = initialPosition - new Vector3(offset, height, 0);
    }
}
