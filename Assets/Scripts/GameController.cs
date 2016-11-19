using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
class PlayerData
{
    public bool isSoundOn;
}
public enum CursorIcon
{
    NORMAL = 1,
    WALK = 2,
    ACTION = 3
}

public class GameController : MonoBehaviour {
    public static GameController controller = null;
    public bool isSoundOn;
    public bool isUI = false;
    public float lastUITime = -1f;
    public Texture2D[] cursorIcons;
    public CursorIcon currentIcon = CursorIcon.NORMAL;
    public int currentArea = 0;
    public int targetArea = 0;
    public float startPositionY;
    public float scaleParam = 0f;
    public float defaultCharactecScale = 0f;

    void Awake()
    {
        if (controller == null)
        {
            DontDestroyOnLoad(gameObject);
            controller = this;
            Load();
            GetComponent<AudioSource>().enabled = isSoundOn;
        }
        else if (controller != this)
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
        }
        else
        {
            isSoundOn = true;
        }
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        GetComponent<AudioSource>().enabled = isSoundOn;
    }
}
