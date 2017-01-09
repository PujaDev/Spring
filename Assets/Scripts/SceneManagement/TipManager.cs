using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public abstract class TipManager : IChangable {

    public Text TutorialTipsText;
    
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
