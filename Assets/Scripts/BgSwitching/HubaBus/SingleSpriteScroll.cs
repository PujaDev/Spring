using UnityEngine;
using System.Collections;

public class SingleSpriteScroll : MonoBehaviour
{
    public float speedX;
    public float initialOffsetX = 0f;
    
    public float cameraWidth;

    private float width;
    private Vector3 initialPosition;
    private bool endWasNotReached = true;
    public float startTime;

    void OnEnable()
    {
        width = GetComponent<SpriteRenderer>().bounds.size.x - cameraWidth;
        initialPosition = transform.position;
        startTime = Time.time;
    }

    void Update()
    {
        if (endWasNotReached)
        {
            float offset = initialOffsetX;
            offset += (Time.time - startTime) * speedX;
            if (offset >= width)
            {
                endWasNotReached = false;
                initialOffsetX = offset;
            }
            transform.position = initialPosition - new Vector3(offset, 0, 0);
        }
    }
}
