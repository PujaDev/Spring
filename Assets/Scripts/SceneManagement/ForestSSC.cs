using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using Spine;

public class ForestSSC : SceneSwitchControler
{
    public enum Direction
    {
        None = -1,
        Left = 0,
        Right
    }

    public GameObject[] GlowingObjects;
    public GameObject[] Setup1;
    public GameObject[] Setup2;
    public GameObject[] Setup3;

    private System.Random Rnd;
    private List<GameObject[]> Setups;

    protected override void Awake()
    {
        base.Awake();

        Rnd = new System.Random();
        Setups = new List<GameObject[]>()
        {
            Setup1,
            Setup2,
            Setup3
        };
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        // We just came to ritual site
        if (newState.HubaForest.IsOnSite && (oldState == null || !oldState.HubaForest.IsOnSite))
        {
            string mssg = oldState == null ? "GoSiteSwitch" : "GoSite";
            Flowchart.SendFungusMessage(mssg);
        }
        // Regular forest
        else if ((newState.HubaForest.IsInForest && (oldState == null || !oldState.HubaForest.IsInForest))                      // Player just came to forest
            || (oldState != null && oldState.HubaForest.CurrentForestWay.Count < newState.HubaForest.CurrentForestWay.Count))   // Player went further to the forest
        {
            ToggleGlow(newState.HubaForest.IsHubaBlessed);

            string mssg = oldState == null ? "GoForestSwitch" : "GoForest";
            Flowchart.SendFungusMessage(mssg);
        }
    }

    private void ToggleGlow(bool isOn)
    {
        foreach (var item in GlowingObjects)
        {
            item.SetActive(isOn);
        }
    }

    public void GenerateNewLayout()
    {
        foreach (var setup in Setups)
        {
            foreach (var obj in setup)
            {
                obj.SetActive(false);
            }
        }

        int activeSetup = Rnd.Next(0, Setups.Count);
        foreach (var obj in Setups[activeSetup])
        {
            obj.SetActive(true);
        }
    }

    public void SetScaleForRitualSite()
    {
        SceneController.Instance.defaultCharactecScale = 0.24f;
    }

    public static List<int> GenerateNewPath(int depth)
    {
        var rnd = new System.Random();
        List<int> path = new List<int>();
        for (int i = 0; i < depth; i++)
        {
            path.Add(rnd.Next((int)Direction.Left, (int)Direction.Right + 1));
        }

        return path;
    }
}
