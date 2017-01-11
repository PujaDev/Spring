using UnityEngine;
using System.Collections;

public class MoveUpAndDown : MonoBehaviour {
    public float Speed = 3f;
    public float Height = 1f;

    private Vector3 startPosition;

    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveVertical();
    }


    void MoveVertical()
    {
        transform.position = new Vector3(transform.position.x, ( Mathf.Sin(Time.time * Speed)) * Height, transform.position.z);
    }
}
