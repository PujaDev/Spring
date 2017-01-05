using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
class PlayerData
{
    public bool isSoundOn;
    public int lastPlayedTimeline;
    public int[] stateNums;
    public bool playedAny;
}

public enum CursorIcon
{
    NORMAL = 1,
    WALK,
    ACTION,
    SWITCH_SCENE
}

public class GameController : MonoBehaviour {
    public static GameController Instance { get; private set; }

    public bool isUI = false;
    public int[] stateNums = new int[3];
    public float lastUITime = -1f;
    public Texture2D[] cursorIcons;
    public CursorIcon currentIcon = CursorIcon.NORMAL;

    private bool isSoundOn;
    private bool playedAny = false;
    private int lastPlayedTimeRange;
    public int LastPlayedTimeRange
    {
        get { return lastPlayedTimeRange; }
        set {
            lastPlayedTimeRange = value;
            Save();
        }
    }
    public bool PlayedAny
    {
        get { return playedAny; }
        set
        {
            playedAny = value;
            Save();
        }
    }
    public bool IsSoundOn
    {
        get { return isSoundOn; }
    }
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            Load();
            if (SceneState.ActiveTimeRange != -1)
            {
                StateManager.Instance.SetAsLastState(SceneState.ActiveTimeRange);
                LastPlayedTimeRange = SceneState.ActiveTimeRange;
                PlayedAny = true;
            }
            GetComponent<AudioSource>().enabled = isSoundOn;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();
        data.isSoundOn = isSoundOn;
        data.lastPlayedTimeline = lastPlayedTimeRange;
        data.stateNums = stateNums;
        data.playedAny = playedAny;
        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            
            isSoundOn = data.isSoundOn;
            lastPlayedTimeRange = data.lastPlayedTimeline;
            stateNums = data.stateNums;
            playedAny = data.playedAny;
        }
        else
        {
            isSoundOn = true;
            lastPlayedTimeRange = 0;
            //stateNums
            playedAny = false;
        }
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        GetComponent<AudioSource>().enabled = isSoundOn;
        Save();
    }

    public void SetLastStateNum(int timeRange, int stateNum)
    {
        for (int i = timeRange; i < stateNums.Length; i++)
        {
            stateNums[i] = stateNum;
        }
        Save();
    }

    public bool MoveCharToObject(GameObject obj, SpringAction action = null, IInteractable interactable = null)
    {
        RaycastHit2D hit = Physics2D.Raycast(obj.transform.position, -Vector2.up, Mathf.Infinity, 0 | (1 << LayerMask.NameToLayer("WalkableArea")));
        Vector2 destination;
        //Debug.Log(hit.point);
        if (hit.collider == null)
        {
            hit = Physics2D.Raycast(obj.transform.position, Vector2.up, Mathf.Infinity, 0 | (1 << LayerMask.NameToLayer("WalkableArea")));
            //Debug.Log(hit.point);
            destination = hit.point;
            destination.y += 0.001f;
        }
        else
        {
            destination = hit.point;
            destination.y -= 0.001f;
        }
        if (hit.collider != null)
        {
            //Debug.Log(hit.collider.name);
            CharacterInput walkableArea = hit.collider.gameObject.GetComponent<CharacterInput>();
            if (walkableArea != null)
            {
                walkableArea.MoveToPoint(destination, action, interactable);
                return true;
            }
        }

        return false;
    }
}
