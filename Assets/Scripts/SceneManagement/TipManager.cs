using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public abstract class TipManager : IChangable {

    public Text TutorialTipsText;
    
    void OnMousOver()
    {
        Debug.Log("CLICK");
    }

    // Update is called once per frame
    void Update () {
	
	}

    sealed public override void OnStateChanged(GameState newState, GameState oldState)
    {
        if (!gameObject.activeInHierarchy)
            return;

        var oldText = TutorialTipsText.text;
        var text = GetTipText(newState, oldState);

        // maybe later
        // if (oldText != text)
        //    gameObject.SetActive(false);

        TutorialTipsText.text = text;
    }

    protected virtual string GetTipText(GameState newState, GameState oldState)
    {
        return GetTipText(newState);
    }

    protected abstract string GetTipText(GameState state);
}
