using UnityEngine;
using System.Collections;

public class Step : MonoBehaviour
{
    public Sprite Selected;
    public Sprite Deselected;

    public void OnTriggerEnter2D(Collider2D other)
    {
        GetComponent<SpriteRenderer>().sprite = Selected;
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        GetComponent<SpriteRenderer>().sprite = Deselected;
    }
}
