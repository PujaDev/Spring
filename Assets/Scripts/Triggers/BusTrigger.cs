using UnityEngine;
using System.Collections;
using System;

public class BusTrigger : IChangable {
    public HubaBusFirstStop bus;
    public FollowCharacter follow;
    public Transform busStopFollowTarget;
    public Transform[] topTargets;
    public Transform bottomTarget;
    public float Speed;
    public CameraManager cameraManager;
    public BoxCollider2D boxCollider;

    protected Coroutine move;

    IEnumerator MoveToCoroutine(Transform[] targets, bool isEnd = false)
    {
        for (int i = 0; i < targets.Length; i++) {
            while (Vector3.Distance(busStopFollowTarget.position, targets[i].position) > 0.05f)
            {
                busStopFollowTarget.position = Vector3.MoveTowards(busStopFollowTarget.position, targets[i].position, Speed * Time.deltaTime);
                yield return null;
            }
        }
        if (isEnd) {
            HubaDayCharacterMovement character = GameObject.FindGameObjectWithTag("Character").GetComponent<HubaDayCharacterMovement>();
            character.Busy = false;
            if(StateManager.Instance.State.HubaBus.hasBusLeft) character.GetAngry();
        }
    }
     
    public void OnTriggerEnter2D(Collider2D collision)
    {
        bus.BusComesToTheBusstop();
        GetComponent<Collider2D>().isTrigger = false;
        GameObject.FindGameObjectWithTag("Character").GetComponent<CharacterMovement>().Busy = true;
        follow.Character = busStopFollowTarget;
        if (StateManager.Instance.State.HubaBus.isDrunk && StateManager.Instance.State.AnnanaHouse.OwlPackage == (int)AnnanaInventory.ItemIds.Invis)
        {
            Vector3 pos = topTargets[topTargets.Length - 1].transform.position;
            pos = new Vector3(pos.x, pos.y - 6f,pos.z);
            topTargets[topTargets.Length - 1].transform.position = pos;
        }
            move = StartCoroutine(MoveToCoroutine(topTargets, true));
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        if (newState.HubaBus.hasBusLeft)
        {
            GetComponent<Collider2D>().isTrigger = false;
            follow.Character = GameObject.FindGameObjectWithTag("CharacterTarget").transform;
            cameraManager.Bounds = boxCollider;
        } else if (newState.HubaBus.isBusWaiting)
        {
            GetComponent<Collider2D>().isTrigger = false;
            busStopFollowTarget.position = topTargets[topTargets.Length - 1].position;
            follow.Character = busStopFollowTarget;
            cameraManager.Bounds = boxCollider;
        }
    }
}
