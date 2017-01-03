using UnityEngine;
using System.Collections;

public class BusTrigger : MonoBehaviour {
    public TurtleBusFirstStop bus;
    public FollowCharacter follow;
    private Transform defaultFollowTarget;
    public Transform busStopFollowTarget;
    public Transform topTarget;
    public Transform bottomTarget;
    public float Speed;

    void Start()
    {
        defaultFollowTarget = follow.Character;
    }

    IEnumerator MoveToCoroutine(Vector3 target, bool bottom = false)
    {
        while (Vector3.Distance(busStopFollowTarget.position, target) > 0.05f)
        {
            busStopFollowTarget.position = Vector3.MoveTowards(busStopFollowTarget.position, target, Speed * Time.deltaTime);
            yield return null;
        }

        if (bottom) bus.setInteractibleActive();
    }

    public void CameraGoDown() {
        StartCoroutine(MoveToCoroutine(bottomTarget.position, true));
    }
     
    public void OnTriggerEnter2D(Collider2D collision)
    {
        bus.StartBus();
        GetComponent<Collider2D>().isTrigger = false;
        follow.Character = busStopFollowTarget;
        StartCoroutine(MoveToCoroutine(topTarget.position));
    }
}
