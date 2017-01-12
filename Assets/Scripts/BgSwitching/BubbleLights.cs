using UnityEngine;
using System.Collections;

public class BubbleLights : MonoBehaviour {
    public Color initialColor;
    public float Speed;
    public float diff = 0.1f;

    private float i;
    private Color startColor;
    private Color endColor;

    // Use this for initialization
    void Start () {
        initialColor = GetComponent<SpriteRenderer>().color;
        startColor = new Color(Mathf.Clamp(initialColor.r + Random.Range(-diff, diff), 0, 1), Mathf.Clamp(initialColor.g + Random.Range(-diff, diff), 0, 1), Mathf.Clamp(initialColor.b + Random.Range(-diff, diff), 0, 1));
        endColor = new Color(Mathf.Clamp(initialColor.r + Random.Range(-diff, diff), 0, 1), Mathf.Clamp(initialColor.g + Random.Range(-diff, diff), 0, 1), Mathf.Clamp(initialColor.b + Random.Range(-diff, diff), 0, 1));
    }
	
	// Update is called once per frame
	void Update () {

        // Blend towards the current target colour
        i += Time.deltaTime * Speed;
        GetComponent<SpriteRenderer>().color = Color.Lerp(startColor, endColor, i);

        // If we've got to the current target colour, choose a new one
        if (i >= 1)
        {
            i = 0;
            startColor = GetComponent<SpriteRenderer>().color;
            endColor = new Color(Mathf.Clamp(initialColor.r + Random.Range(-diff, diff), 0, 1), Mathf.Clamp(initialColor.g + Random.Range(-diff, diff), 0, 1), Mathf.Clamp(initialColor.b + Random.Range(-diff, diff), 0, 1));
        }
    }
}
