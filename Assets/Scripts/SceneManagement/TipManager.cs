using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public abstract class TipManager : IChangable {

    public Text TutorialTipsText;

    protected const string ALL_DONE = "There's nothing else you need to do";
    
    sealed public override void OnStateChanged(GameState newState, GameState oldState)
    {
        var oldText = TutorialTipsText.text;
        var text = GetTipText(newState, oldState);
        
        TutorialTipsText.text = text;
    }

    protected virtual string GetTipText(GameState newState, GameState oldState)
    {
        return GetTipText(newState);
    }

    protected abstract string GetTipText(GameState state);
}
