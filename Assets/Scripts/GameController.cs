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
    WALK,
    ACTION,
    SWITCH_SCENE
}

public class GameController : MonoBehaviour {
    public static GameController controller = null;
    public bool isSoundOn;
    public bool isUI = false;
    public float lastUITime = -1f;
    public Texture2D[] cursorIcons;
    public CursorIcon currentIcon = CursorIcon.NORMAL;

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
