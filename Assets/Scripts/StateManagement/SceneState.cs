using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using Spine.Unity;

/// <summary>
/// Wrapper class for Scene States
/// </summary>
[Serializable]
abstract public class SceneState
{
    
    public const string MAIN_MENU_NAME = "MainMenu";
    public const string ANNANA_HOUSE_NAME = "Scena_1_AnnanaHouse";
    public const string HUBA_FOREST_NAME = "Scena_2_HubaForest";
    public const string ANNANA_TEA_PARTY_NAME = "Scena_3_AnnanaTeaParty";
    public const string SILENT_FOREST_NAME = "Scena_4_SilentForest";

    /// <summary>
    /// Character's position in the scene
    /// </summary>
    public Vector3S CharacterPosition { get; protected set; }
    /// <summary>
    /// Describes whether character in the scene is facing left
    /// </summary>
    public bool CharacterFacingLeft { get; protected set; }

    /// <summary>
    /// Maps types of scene state classes to their corresponding scene names
    /// </summary>
    static Dictionary<Type, string> SceneNameMap;
    static Dictionary<Type, int> TimeRangeMap;
    static Dictionary<string, int> NameTimeRangeMap;

    /// <summary>
    /// Initializes class type - scene name map
    /// </summary>
    static SceneState()
    {
        SceneNameMap = new Dictionary<Type, string>();
        SceneNameMap.Add(typeof(AnnanaSceneState), ANNANA_HOUSE_NAME);
        SceneNameMap.Add(typeof(HubaBusSceneState), HUBA_FOREST_NAME);
        SceneNameMap.Add(typeof(HubaBusSceneState), ANNANA_TEA_PARTY_NAME);
        SceneNameMap.Add(typeof(HubaForestSceneState), SILENT_FOREST_NAME);

        TimeRangeMap = new Dictionary<Type, int>();
        TimeRangeMap.Add(typeof(AnnanaSceneState), 0);
        TimeRangeMap.Add(typeof(HubaBusSceneState), 1);
        TimeRangeMap.Add(typeof(AnnanaTeaPartySceneState), 1);
        TimeRangeMap.Add(typeof(HubaForestSceneState), 2);

        NameTimeRangeMap = new Dictionary<string, int>();
        NameTimeRangeMap.Add(MAIN_MENU_NAME, -1);
        NameTimeRangeMap.Add(ANNANA_HOUSE_NAME, 0);
        NameTimeRangeMap.Add(HUBA_FOREST_NAME, 1);
        NameTimeRangeMap.Add(ANNANA_TEA_PARTY_NAME,1);
        NameTimeRangeMap.Add(SILENT_FOREST_NAME, 0);
    }
    
    /// <summary>
    /// Name of corresponding scene
    /// </summary>
    public string SceneName {
        get {
            return SceneNameMap[this.GetType()];
        }
    }

    public int TimeRange
    {
        get
        {
            return TimeRangeMap[this.GetType()];
        }
    }
    public static int ActiveTimeRange
    {
        get
        {
            return NameTimeRangeMap[SceneManager.GetActiveScene().name];
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
                var characterObject = GameObject.FindWithTag("Character");

                CharacterPosition = new Vector3S(characterObject.transform.position);

                CharacterFacingLeft = characterObject.GetComponent<SkeletonAnimation>().skeleton.FlipX;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
    
}
