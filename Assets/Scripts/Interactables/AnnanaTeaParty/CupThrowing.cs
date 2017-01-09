using UnityEngine;
using System.Collections;
using System;
using Spine;

public class CupThrowing : IChangable
{
    public GameObject Cup;
    AnnanaCharacterMovement Movement;
    bool ThrewCup = false;

    void Awake()
    {
        Movement = GameObject.FindGameObjectWithTag("Character").GetComponent<AnnanaCharacterMovement>();
    }

    public override void OnStateChanged(GameState newState, GameState oldState)
    {

        if (!newState.AnnanaTeaParty.IsHappy && 
            !newState.AnnanaTeaParty.IsReadingTheFine &&
            oldState != null &&
            oldState.AnnanaTeaParty.IsReadingTheFine)
        {
            Movement.ThrowTeaAnimation().Event += ThrowCup;
            StateManager.Instance.DispatchAction(new SpringAction(ActionType.THROW_CUP));
        }
    }

    void ThrowCup(TrackEntry trackEntry, Spine.Event e)
    {
        if (!ThrewCup)
        {
            ThrewCup = true;
            StartCoroutine("ThrowingCup");
        }
    }

    IEnumerator ThrowingCup()
    {
        Cup.SetActive(true);
        Cup.GetComponent<Rigidbody2D>().AddForce(new Vector3(-300,200,500));
        yield return new WaitForSeconds(5);
        Cup.SetActive(false);
    }
}
