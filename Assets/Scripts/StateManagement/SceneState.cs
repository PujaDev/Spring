using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

/// <summary>
/// Wrapper class for Scene States
/// </summary>
[Serializable]
abstract public class SceneState
{
    public Vector3S CharacterPosition { get; protected set; }

    /// <summary>
    /// Maps types of scene state classes to their corresponding scene names
    /// </summary>
    static Dictionary<Type,string> SceneNameMap;

    /// <summary>
    /// Initializes class type - scene name map
    /// </summary>
    static SceneState()
    {
        SceneNameMap = new Dictionary<Type, string>();
        SceneNameMap.Add(typeof(AnnanaSceneState), "Scena_1_AnnanaHouse");
        SceneNameMap.Add(typeof(HubaBusSceneState), "Scena_2_HubaForest");
        SceneNameMap.Add(typeof(HubaForestSceneState), "Scena_4_SilentForest");
    }

    /// <summary>
    /// Name of corresponding scene
    /// </summary>
    public string SceneName {
        get {
            return SceneNameMap[this.GetType()];
        }
    }

    /// <summary>
    /// Sets current character position to this state
    /// </summary>
    public void SetCharacterPosition()
    {
        try
        {
            if (SceneManager.GetActiveScene().name == SceneName)
            {
                CharacterPosition = new Vector3S(GameObject.FindWithTag("Character").transform.position);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
    
}
