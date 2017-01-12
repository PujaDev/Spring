using UnityEngine;
using System.Collections;

public class OneBubbleLight : MonoBehaviour
{
    public Color initialColor;
    public Color blinkColor;
    public float Speed;

    private float i;

    // Use this for initialization
    void Start()
    {
        initialColor = new Color(1, 1, 1, 1);
        blinkColor = new Color(1, 1, 1, 0.7f);
        Speed = Random.Range(0.5f, 2f);
    }

    // Update is called once per frame
    void Update()
    {

        // Blend towards the current target colour
        i += Time.deltaTime * Speed;
        GetComponent<SpriteRenderer>().color = Color.Lerp(initialColor, blinkColor, i);

        // If we've got to the current target colour, choose a new one
        if (i >= 1)
        {
            i = 0;
            blinkColor = initialColor;
            initialColor = GetComponent<SpriteRenderer>().color;
            Speed = Random.Range(0.5f, 2f);
        }
    }
}
