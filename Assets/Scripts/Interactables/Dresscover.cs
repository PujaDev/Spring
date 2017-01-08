using UnityEngine;
using System.Collections;
using System;
using Fungus;
using System.Collections.Generic;
using System.Linq;

public class Dresscover : IInteractable
{
    public string Pajamas;
    public string DayClothes;
    public string NightRobe;

    public SpriteRenderer Cover;

    private Flowchart Fungus;
    private Dictionary<string, SpringAction> ActionMap;
    private HashSet<SpringAction> Actions;

    protected override void Awake()
    {
        base.Awake();

        Fungus = GameObject.FindGameObjectWithTag("Scenarios").GetComponent<Flowchart>();

        ActionMap = new Dictionary<string, SpringAction>()
        {
            { Pajamas, new SpringAction(ActionType.CHANGE_CLOTHES, "Wear pajamas", icons[0], Pajamas) },
            { DayClothes, new SpringAction(ActionType.CHANGE_CLOTHES, "Wear day clothes", icons[1], DayClothes) },
            { NightRobe, new SpringAction(ActionType.CHANGE_CLOTHES, "Wear night robe", icons[2], NightRobe) }
        };

        Actions = new HashSet<SpringAction>(ActionMap.Values);
    }

    protected override SpringAction[] GetActionList()
    {
        return Actions.ToArray();
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        if (oldState == null)
        {
            // We cannot change into something we already wear
            Actions.Remove(ActionMap[newState.AnnanaHouse.AnnanaDress]);
        }
        else if (oldState.AnnanaHouse.AnnanaDress != newState.AnnanaHouse.AnnanaDress)
        {
            // We cannot change into something we already wear
            Actions.Add(ActionMap[oldState.AnnanaHouse.AnnanaDress]);
            Actions.Remove(ActionMap[newState.AnnanaHouse.AnnanaDress]);

            Fungus.SendFungusMessage("ChangeClothes");
        }
    }

    public void ToggleCover()
    {
        Cover.sortingLayerName = Cover.sortingLayerName == "Midground" ? "Foreground" : "Midground";
    }
}
