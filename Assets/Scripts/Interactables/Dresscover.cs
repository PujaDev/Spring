using UnityEngine;
using System.Collections;
using System;

public class Dresscover : IInteractable {
    protected override SpringAction[] GetActionList()
    {
        return new SpringAction[] {
            new SpringAction(ActionType.CHANGE_CLOTHES, "Change clothes",icons[0])
        };
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        if (newState.Test.ChangeClothes)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(!child.gameObject.activeInHierarchy);
            }
        }
    }
}
