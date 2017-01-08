using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;


public class HubaDayCharacterMovement : CharacterMovement, IMoveable
{
    public DoorSwitch door;
    public Transform startPosition;

    public IEnumerator MoveToStartCoroutine()
    {
        yield return new WaitForSeconds(2f);

        skeletonAnim.AnimationState.SetAnimation(0, "walk", true).timeScale = AnimationSpeed;
        
        while (Vector3.Distance(transform.position, startPosition.position) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition.position, Speed * Time.deltaTime);
            ScaleCharacter();
            yield return null;
        }
        skeletonAnim.AnimationState.SetAnimation(0, "idle", true);
        skeletonAnim.skeleton.FlipX = true;

        door.CloseDoor();

        yield return new WaitForSeconds(3f);

        skeletonAnim.skeleton.FlipX = false;

        StateManager.Instance.DispatchAction(new SpringAction(ActionType.EXIT_HOUSE));

    }
    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        if (!newState.HubaBus.isOutOfTheHouse)
        {
            Busy = true;
            skeletonAnim.AnimationState.SetAnimation(0, "exit_house", false);
            skeletonAnim.AnimationState.AddAnimation(0, "idle", true, 0f);

            door.OpenDoor();
            StartCoroutine(MoveToStartCoroutine());

        }
        else
        {
            Busy = false;
            if (move == null && skeletonAnim.AnimationName != "idle")
            {
                skeletonAnim.AnimationState.SetAnimation(0, "idle", true);
            }
        }
    }
}