using UnityEngine;
using System.Collections;

public class SwitchObjects : MonoBehaviour {

    public GameObject[] objects;
    private int index;

    // Use this for initialization
    void Start()
    {
        index = 0;
        foreach (GameObject g in objects) {
            g.SetActive(false);
        }
        objects[index].SetActive(true);
    }

    public void changeToIndex(int id)
    {
        if (index != 0)
        {
            objects[index].SetActive(false);
            index = 0;
            objects[0].SetActive(true);
        }
        else {
            objects[index].SetActive(false);
            index = id;
            objects[id].SetActive(true);
        }
    }

    public void next()
    {
        objects[index].SetActive(false);
        index++;
        if (index >= objects.Length)
        {
            index = 0;
        }
        objects[index].SetActive(true);
    }
}
