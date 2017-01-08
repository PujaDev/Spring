using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using System;

public class HubaDayCharacterMovement : CharacterMovement, IMoveable, IItemUsable
{
    public DoorSwitch door;
    public Transform startPosition;
    private HashSet<int> UsableItems;

    public bool CanUseOnSelf(int itemId)
    {
        return UsableItems.Contains(itemId);
    }
    override protected void Awake()
    {
        base.Awake();
        UsableItems = new HashSet<int>()
        {
            (int)HubaBusInventory.ItemIds.Antidote,
            (int)HubaBusInventory.ItemIds.Shrink,
            (int)HubaBusInventory.ItemIds.Invis,
            (int)HubaBusInventory.ItemIds.Soup
        };
    }

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

            if (newState.HubaBus.isDrunk)
            {
                switch (newState.AnnanaHouse.OwlPackage)
                {
                    case (int)AnnanaInventory.ItemIds.Antidote:
                        skeletonAnim.skeleton.SetSkin("Edible"); Debug.Log("edible");
                        break;
                    case (int)AnnanaInventory.ItemIds.Invis:
                        skeletonAnim.skeleton.a = 0.15f;
                        break;
                    case (int)AnnanaInventory.ItemIds.Shrink:
                        float tmp = gameObject.transform.localScale.x;
                        tmp = tmp / 2f;
                        gameObject.transform.localScale = new Vector3(tmp, tmp, tmp); Debug.Log("shrink");
                        SceneController.Instance.defaultCharactecScale = SceneController.Instance.defaultCharactecScale / 2f;
                        break;
                    //case (int)HubaBusInventory.ItemIds.Soup:
                    //    skeletonAnim.skeleton.SetSkin("poisonous");
                    //    break;
                    default:
                        Debug.Log("WTF");
                        break;
                }
            }

            if (newState.HubaBus.hasBusLeft)
            {
                if (skeletonAnim.AnimationName != "anger")
                    GetAngry();

            }
            else
            {
                if (move == null && skeletonAnim.AnimationName != "idle")
                {
                    skeletonAnim.AnimationState.SetAnimation(0, "idle", true);
                }
            }
        }
    }
    public void GetAngry()
    {
        skeletonAnim.AnimationState.SetAnimation(0, "anger", true);
    }

    public void UseOnSelf(int itemId)
    {
        if (CanUseOnSelf(itemId))
        {
            //if (itemId == (int)HubaForestInventory.ItemIds.Coin)
            //{
            //    ComeCloser(new SpringAction(ActionType.GIVE_MONEY_TO_SHRINE, "", null));
            //}
            StateManager.Instance.DispatchAction(new SpringAction(ActionType.DRINK));
        }
    }
}