using UnityEngine;

/// <summary>
/// Reducer parent class. Reducers are used to describe state changes.
/// </summary>
abstract public class Reducer : MonoBehaviour  {

    // When scene starts, add every reducer existing in the scene
    // to the state manager
    void Start()
    {
        StateManager.Instance.AddReducer(this); 
    }

    /// <summary>
    /// Processes dispatched action. Describes how state should change
    /// based on dispatched action and current state.
    /// </summary>
    /// <param name="action">Dispatched action</param>
    /// <param name="state">Current state</param>
    /// <param name="source">Interactable that dispatched action</param>
    /// <returns>Updated state</returns>
    abstract public GameState Reduce(GameState state, SpringAction action, IInteractable source = null);
}
