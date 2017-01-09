using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Item = AnnanaTeaPartyInventory.ItemIds;

public class Cup : IInteractable
{
    public Sprite[] TeapotSprites;
    public int SteepTime = 3;
    public int TeaDuration = 3;
    public Color[] TeaColors;
    public GameObject WaterObject;
    public GameObject TeaBagObject;
    SpriteRenderer Water;
    SpringAction[] Actions;
    bool Steeping;

    protected override void Awake()
    {
        base.Awake();
        Water = WaterObject.GetComponent<SpriteRenderer>();
    }

    protected override SpringAction[] GetActionList()
    {
        return Actions;
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        gameObject.SetActive(!newState.AnnanaTeaParty.DrankTea);

        var ActionList = new List<SpringAction>();
        var inventory = newState.AnnanaTeaParty.PickedUpItems;
        var teabagIn = newState.AnnanaTeaParty.TeaBagInTheCup;
        var waterId = newState.AnnanaTeaParty.WaterInTheCup;
        var waterIn = waterId >= 0;

        if (inventory.Contains((int)Item.PotColdWater))
        {
            ActionList.Add(new SpringAction(ActionType.FILL_CUP, "Pour cold water in", icons[1], 1));
        }

        if (inventory.Contains((int)Item.PotHotWater))
        {
            ActionList.Add(new SpringAction(ActionType.FILL_CUP, "Pour with hot water in", icons[2], 2));
        }

        if (inventory.Contains((int)Item.TeaBag))
        {
            ActionList.Add(new SpringAction(ActionType.USE_TEA_BAG, "Put tea bag in", icons[0]));
        }

        if (waterId >= 0)
        {
            ActionList.Add(new SpringAction(ActionType.DRINK_TEA, "Drink", icons[4]));
        }

        if (ActionList.Count == 0)
        {
            ActionList.Add(new SpringAction(ActionType.LOOK, "Nothing to do", icons[3]));
        }


        Actions = ActionList.ToArray();

        TeaBagObject.SetActive(teabagIn);

        WaterObject.SetActive(waterIn);

        if (waterIn)
        {
            Water.color = TeaColors[waterId];
        }

        if ((waterId == 0 || waterId == 2) && teabagIn)
        {
            if (!Steeping)
            {
                Steeping = true;
                StartCoroutine("InfuseTea");
            }
        }
        else
        {
            Steeping = false;
            StopCoroutine("InfuseTea");
        }


        //var hasTeapot = inventory.Contains((int)Item.PotEmpty) ||
        //    inventory.Contains((int)AnnanaTeaPartyInventory.ItemIds.PotColdWater) ||
        //    inventory.Contains((int)AnnanaTeaPartyInventory.ItemIds.PotHotWater);
        //TeapotId = newState.AnnanaTeaParty.TeapotOnTheStove;

    }

    IEnumerator InfuseTea()
    {
        yield return new WaitForSeconds(SteepTime);
        StateManager.Instance.DispatchAction(new SpringAction(ActionType.STEEP_TEA));
        yield return new WaitForSeconds(TeaDuration);
        StateManager.Instance.DispatchAction(new SpringAction(ActionType.OVERSTEEP_TEA));
        Steeping = false;
    }
}