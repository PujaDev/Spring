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

    public GameObject Bus;
    public GameObject Huba;
    public GameObject InvButton;

    public GameObject[] GlowingObjects;
    public GameObject[] Setup1;
    public GameObject[] Setup2;
    public GameObject[] Setup3;

    private List<GameObject[]> Setups;

    protected override void Awake()
    {
        base.Awake();

        Setups = new List<GameObject[]>()
        {
            Setup1,
            Setup2,
            Setup3,
        };
    }

    protected override void Start()
    {
        base.Start();

        GenerateNewLayout();
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        // We just came to ritual site
        if (newState.HubaForest.IsOnSite && (oldState == null || !oldState.HubaForest.IsOnSite))
        {
            string mssg = oldState == null ? "GoSiteSwitch" : "GoSite";
            Debug.Log(mssg);
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
        // First time coming to scene
        else if (oldState == null && newState.HubaBus.getOnTheBus && !newState.HubaForest.IsSceneStarted)
        {
            // Set bus in front of Huba
            Bus.GetComponent<MeshRenderer>().sortingLayerName = "Character";
            Bus.GetComponent<MeshRenderer>().sortingOrder = 15;
            Flowchart.SendFungusMessage("GoStart");
            StateManager.Instance.DispatchAction(new SpringAction(ActionType.START_FOREST_SCENE));
        }
        // Huba cannot be here if she did not take the bus
        else if (oldState == null && !newState.HubaBus.getOnTheBus)
        {
            Huba.SetActive(false);
            var cols = Resources.FindObjectsOfTypeAll<Collider2D>();
            foreach (var c in cols)
            {
                c.enabled = false;
            }
            CameraManager.Instance.gameObject.GetComponentInChildren<Collider2D>().enabled = true;
            InvButton.SetActive(false);
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

        // Deterministic setup switching seems to look better than random
        //int activeSetup = Rnd.Next(0, Setups.Count);
        int activeSetup = StateManager.Instance.State.HubaForest.CurrentForestWay.Count % Setups.Count;
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

    #region FungusMethods
    #endregion
}
