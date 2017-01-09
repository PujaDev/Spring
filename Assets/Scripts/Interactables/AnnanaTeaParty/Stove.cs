using UnityEngine;
using System.Collections;
using System;

public class Stove : IInteractable
{
    public Sprite[] TeapotSprites;
    public GameObject TeapotObject;
    public GameObject BubbleParticleObject;
    public GameObject FireParticleObject;
    public int BoilingTime;
    ParticleSystem BubbleParticleSystem;
    ParticleSystem FireParticleSystem;
    SpringAction[] Actions;
    bool Boiling;
    int TeapotId;

    protected override void Awake()
    {
        base.Awake();
        BubbleParticleSystem = BubbleParticleObject.GetComponent<ParticleSystem>();
        FireParticleSystem = FireParticleObject.GetComponent<ParticleSystem>();
    }

    protected override SpringAction[] GetActionList()
    {
        return Actions;
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        gameObject.SetActive(!newState.AnnanaTeaParty.DrankTea);

        var inventory = newState.AnnanaTeaParty.PickedUpItems;
        var hasTeapot = inventory.Contains((int)AnnanaTeaPartyInventory.ItemIds.PotEmpty) ||
            inventory.Contains((int)AnnanaTeaPartyInventory.ItemIds.PotColdWater) ||
            inventory.Contains((int)AnnanaTeaPartyInventory.ItemIds.PotHotWater);
        TeapotId = newState.AnnanaTeaParty.TeapotOnTheStove;
        var teapotOnStove = TeapotId >= 0;

        if (hasTeapot)
        {
            Actions = new SpringAction[] {
                new SpringAction(ActionType.PUT_TEAPON_ON_THE_STOVE, "Put teapot on the stove",icons[3])
            };
        }
        else if (teapotOnStove)
        {
            Actions = new SpringAction[] {
                new SpringAction(ActionType.TAKE, "Take teapot",icons[TeapotId],TeapotId)
            };
        }
        else
        {
            Actions = new SpringAction[] {
                new SpringAction(ActionType.LOOK, "You have nothing to put on the stove", icons[4])
            };
        }

        TeapotObject.SetActive(teapotOnStove);

        if (teapotOnStove)
        {
            TeapotObject.GetComponent<SpriteRenderer>().sprite = TeapotSprites[TeapotId];
        }

        if(TeapotId == 1)
        {
            if (!Boiling) // I don't know about any way to find out whether coroutine is running already
            {
                Boiling = true;
                StartCoroutine("BoilingWater");
            }
            FireParticleSystem.Play();
        }
        else
        {
            Boiling = false;
            StopCoroutine("BoilingWater");
            FireParticleSystem.Stop();
        }

        if (TeapotId == 2)
            BubbleParticleSystem.Play();
        else
            BubbleParticleSystem.Stop();

    }

    IEnumerator BoilingWater()
    {
        yield return new WaitForSeconds(BoilingTime);
        StateManager.Instance.DispatchAction(new SpringAction(ActionType.WATER_BOILED));
    }
}