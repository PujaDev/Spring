using UnityEngine;
using System.Collections;

public interface IMoveable
{
    void MoveTo(Vector3 target);
    Vector3 Position { get; }
}

public class CharacterMovement : MonoBehaviour, IMoveable {

    public float Speed;
    public AnimationCurve Curve;
    public Vector3 Position { get { return transform.position; } }

    private bool busy;

    // Use this for initialization
    void Start ()
    {
        busy = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    public void MoveTo(Vector3 target)
    {
        if (!busy)
        {
            Vector3 origin = transform.position;
            StartCoroutine(MoveToCorutine(origin, target));
        }
    }

    IEnumerator MoveToCorutine(Vector3 origin, Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.005f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Speed * Time.deltaTime);
            
            yield return null;
        }
    }
}
