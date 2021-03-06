﻿using UnityEngine;
using System.Collections;
using System;
using Spine.Unity;
using Spine;

public class KitchenManager : IChangable
{
    public float ZoomedCameraSize = 3f;
    public float OriginalCameraSize = 5.5f;
    public float ZoomSpeed = 0.02f;
    public Vector3 CameraPosition = new Vector3(-1.2f, 1.92f, -10);
    public Vector3 CharacterPosition = new Vector3(-0.26f, -0.76f, -2.71f);
    public Vector3 CharacterScale = new Vector3(0.42f, 0.42f, 0.584f);
    public GameObject TargetWalkableArea;
    public Vector3 TargetPosition = new Vector3(-2.1f, -4f, 0);
    GameObject Character;
    AnnanaCharacterMovement Movement;
    MeshRenderer Renderer;
    bool TeaInHand = false;

    void Awake()
    {
        Character = GameObject.FindGameObjectWithTag("Character");
        Movement = Character.GetComponent<AnnanaCharacterMovement>();
        Renderer = Character.GetComponent<MeshRenderer>();
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {
        CameraManager.Instance.gameObject.SetActive(newState.AnnanaTeaParty.DrankTea);
        Movement.enabled = newState.AnnanaTeaParty.DrankTea;
        TargetWalkableArea.SetActive(!newState.AnnanaTeaParty.InTheKitchen);

        if (!newState.AnnanaTeaParty.DrankTea)
        {
            if (oldState == null)
            {
                Camera.main.orthographicSize = ZoomedCameraSize;
                Camera.main.transform.position = CameraPosition;
                Character.transform.position = CharacterPosition;
                //Character.transform.localScale = CharacterScale;
                Renderer.sortingLayerName = "Midground";
                Renderer.sortingOrder = 2;
                GameController.Instance.CanCharacterMove = false;
                ActionWheel.Instance.Rescale(0.45f, 4.8f);
                DialogManager.Instance.SetDialogue(0);
                DialogManager.Instance.Next();
            }
        }
        else
        {
            if(oldState == null)
            {
                Movement.TeaInHandAnimation();
                TeaInHand = true;
            }
            else if( oldState.AnnanaTeaParty.DrankTea == false)
            {
                Movement.DrinkTeaAnimation().End += OnTeaDrank;
                DialogManager.Instance.SetDialogue(
                    oldState.AnnanaTeaParty.WaterInTheCup == 0 ? 1 : 2
                    );
                DialogManager.Instance.Next();
                ActionWheel.Instance.Rescale(0.6f, 3.29f);
                var t = TargetWalkableArea.GetComponent<CharacterInput>();
                TargetWalkableArea.SetActive(true); // fuck
                t.MoveToPoint(TargetPosition);
                TargetWalkableArea.SetActive(false); // me
                StartCoroutine("ZoomOut");
            }
        }
    }

    private void OnTeaDrank(TrackEntry trackEntry)
    {
        if (TeaInHand)
            return;
        TeaInHand = true;
        Movement.TeaInHandAnimation();
    }


    IEnumerator ZoomOut()
    {
        for (var i = ZoomedCameraSize; i < OriginalCameraSize; i+=ZoomSpeed)
        {
            Camera.main.orthographicSize = i;
            yield return new WaitForFixedUpdate();
        }
        Renderer.sortingLayerName = "Character";
        Renderer.sortingOrder = 13;
        GameController.Instance.CanCharacterMove = true;
        Camera.main.orthographicSize = OriginalCameraSize;
        StateManager.Instance.DispatchAction(new SpringAction(ActionType.WALKED_OUT));

    }
}
