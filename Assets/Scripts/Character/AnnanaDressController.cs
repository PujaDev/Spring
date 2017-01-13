using UnityEngine;
using System.Collections;
using System;
using Spine.Unity;

public class AnnanaDressController : IChangable
{
    private SkeletonAnimation Annana;

    private void Awake()
    {
        Annana = GetComponent<SkeletonAnimation>();
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        if (oldState == null)
        {
            Annana.Skeleton.SetSkin(newState.AnnanaHouse.AnnanaDress);
        }
    }

    #region FungusMethods
    public void ChangeDress()
    {
        Annana.Skeleton.SetSkin(StateManager.Instance.State.AnnanaHouse.AnnanaDress);
    }

    public void StartWalking()
    {
        if (Annana.AnimationName != "walk")
            Annana.AnimationState.SetAnimation(0, "walk", true).timeScale = 1f;
    }

    public void Turn()
    {
        Annana.skeleton.FlipX = !Annana.skeleton.FlipX;
    }

    public void StopWalking()
    {
        Annana.AnimationState.SetAnimation(0, "idle", true);
    }
    #endregion
}
