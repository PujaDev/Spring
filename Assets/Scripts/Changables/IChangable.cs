using UnityEngine;

public abstract class IChangable : MonoBehaviour
{
    abstract public void OnStateChanged(GameState newState, GameState oldState);

    virtual protected void Start()
    {
        var gameState = StateManager.Instance.Subscribe(this);
        OnStateChanged(gameState, null);
    }
}
