using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Spine.Unity;
using Spine;

public class SpiritForestController : IChangable
{
    #region EditorFields
    public GameObject Spirit;
    public string[] LeftSpiritAnim;
    public string[] RightSpiritAnim;
    #endregion

    #region SpiritData
    private Dictionary<ForestSSC.Direction, string[]> SpiritAnims;
    private SkeletonAnimation SpiritAnim;
    private MeshRenderer SpiritMesh;
    #endregion

    #region FungusData
    private bool IsSpiritReleased;
    private string CurrentSpiritAnim;
    #endregion

    private System.Random Rnd;

    protected void Awake()
    {
        Rnd = new System.Random();

        SpiritAnims = new Dictionary<ForestSSC.Direction, string[]>();
        SpiritAnims.Add(ForestSSC.Direction.Left, LeftSpiritAnim);
        SpiritAnims.Add(ForestSSC.Direction.Right, RightSpiritAnim);

        SpiritMesh = Spirit.GetComponent<MeshRenderer>();

        SpiritAnim = Spirit.GetComponent<SkeletonAnimation>();
    }

    protected override void Start()
    {
        base.Start();

        SpiritAnim.AnimationState.Event += OnSpiritChanged;
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        if (!(newState.HubaForest.IsOnSite && (oldState == null || !oldState.HubaForest.IsOnSite)) &&                           // Player just came on site - spirit no longer needed
            ((newState.HubaForest.IsInForest && (oldState == null || !oldState.HubaForest.IsInForest))                           // Player just came to forest
             || (oldState != null && oldState.HubaForest.CurrentForestWay.Count < newState.HubaForest.CurrentForestWay.Count)))  // Player went further to the forest
        {
            // Prepare spirit if need be
            IsSpiritReleased = false;
            if (newState.HubaForest.IsHubaBlessed)
            {
                ForestSSC.Direction dir = GetCurrentDir(newState);
                if (dir != ForestSSC.Direction.None)
                {
                    // Prepare data for Fungus method
                    var currAnimArr = SpiritAnims[dir];
                    CurrentSpiritAnim = currAnimArr[Rnd.Next(0, currAnimArr.Length)];
                    IsSpiritReleased = true;
                }
            }

            Spirit.SetActive(IsSpiritReleased);
        }
    }

    /// <summary>
    /// Call from Fungus with prepared data
    /// </summary>
    public void ReleaseSpirit()
    {
        if (IsSpiritReleased)
        {
            // Reset spirit
            SpiritMesh.sortingOrder = 13;
            SpiritAnim.AnimationState.ClearTrack(0);

            // Set new animation
            SpiritAnim.AnimationState.SetAnimation(0, CurrentSpiritAnim, false);
            SpiritAnim.Skeleton.SetSkin(CurrentSpiritAnim);
        }
    }

    private void OnSpiritChanged(TrackEntry trackEntry, Spine.Event e)
    {
        switch (e.Data.Name)
        {
            case "InfrontOfFrontTrees":
                SpiritMesh.sortingOrder = 13;
                break;
            case "BehindFrontTrees":
                SpiritMesh.sortingOrder = 4;
                break;
            case "InfrontOfMiddleTrees":
                SpiritMesh.sortingOrder = 4;
                break;
            case "BehindMiddleTrees":
                SpiritMesh.sortingOrder = 1;
                break;
            default:
                SpiritMesh.sortingOrder = 13;
                break;
        }
    }

    private ForestSSC.Direction GetCurrentDir(GameState state)
    {
        List<int> rightWay = state.HubaForest.RightForestWay;
        List<int> currWay = state.HubaForest.CurrentForestWay;

        // Is the player too deep to be on the right way?
        if (rightWay.Count < currWay.Count)
            return ForestSSC.Direction.None;

        // Did the player ever took the wrong turn
        for (int i = 0; i < currWay.Count; i++)
        {
            if (currWay[i] != rightWay[i])
                return ForestSSC.Direction.None;
        }

        // Player is on the right way, what is the next turn?
        return (ForestSSC.Direction)rightWay[currWay.Count];
    }
}