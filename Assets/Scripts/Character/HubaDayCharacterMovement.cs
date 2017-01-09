using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using System;

public class HubaDayCharacterMovement : CharacterMovement, IMoveable, IItemUsable
{
    public DoorSwitch door;
    public Transform startPosition;
    public ParticleSystem elixirEffect;
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

    public void HeadUp() {
        skeletonAnim.AnimationState.SetAnimation(2, "head_up", false);
    }
    public void HandUp()
    {
        skeletonAnim.AnimationState.SetAnimation(3, "give_money", false);
    }
    public void SetUpPosition()
    {
        skeletonAnim.AnimationState.SetEmptyAnimations(1f);
        skeletonAnim.AnimationState.AddAnimation(0, "idle", true, 0f);
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
                        skeletonAnim.skeleton.SetSkin("Edible");
                        break;
                    case (int)AnnanaInventory.ItemIds.Invis:
                        skeletonAnim.skeleton.a = 0.15f;
                        break;
                    case (int)AnnanaInventory.ItemIds.Shrink:
                        float tmp = gameObject.transform.localScale.x;
                        tmp = tmp / 2f;
                        gameObject.transform.localScale = new Vector3(tmp, tmp, tmp);
                        SceneController.Instance.defaultCharactecScale = SceneController.Instance.defaultCharactecScale / 2f;
                        break;
                    //case (int)HubaBusInventory.ItemIds.Soup:
                    //    skeletonAnim.skeleton.SetSkin("poisonous");
                    //    break;
                    default:
                        Debug.Log("WTF");
                        break;
                }
                if (oldState == null || !oldState.HubaBus.isDrunk) {
                    if (elixirEffect == null)
                    {

                        var elGameObject = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/ElixirEffect"));
                        elGameObject.transform.parent = gameObject.transform;
                        elixirEffect = elGameObject.GetComponent<ParticleSystem>();
                    }
                    var em = elixirEffect.emission;
                    em.enabled = true;
                    Debug.Log("effect");
                }
            }

            if (newState.HubaBus.hasBusLeft)
            {
                if (skeletonAnim.AnimationName != "anger")
                    GetAngry();
            }
            else
            {
                if (newState.HubaBus.getOnTheBus && !newState.HubaBus.isInTheBus) {
                    skeletonAnim.AnimationState.SetAnimation(0, "idle", true);
                    skeletonAnim.AnimationState.SetAnimation(1, "give_money", false);
                    skeletonAnim.AnimationState.AddAnimation(1, "paid", false, 0);
                    skeletonAnim.AnimationState.SetAnimation(2, "head_up", false);
                }
                else {
                    if (move == null && skeletonAnim.AnimationName != "idle")
                    {
                        skeletonAnim.AnimationState.SetAnimation(0, "idle", true);
                    }
                }
            }
        }
    }

    public void GetAngry()
    {
        skeletonAnim.AnimationState.SetAnimation(0, "anger", true);
        DialogManager.Instance.SetDialogue((int)HubaBusDialogManager.DialogueTypes.Swearing);
        DialogManager.Instance.Next();
    }

    public void UseOnSelf(int itemId)
    {
        if (CanUseOnSelf(itemId))
        {
            StateManager.Instance.DispatchAction(new SpringAction(ActionType.DRINK));
        }
    }
}