using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    public GameObject[] Slots;

    public Sprite[] ItemSprites;
    public string[] ItemNames;

    public List<int> currentItems;

    private bool IsOpen;

    public void Toggle()
    {
        if (Instance != null)
        {
            //if (GameController.controller.IsInputEnabled)
            //{
                if (Instance.IsOpen)
                    Close();
                else
                    Open();
            //}
        }
    }

    public void Open()
    {
        Instance.gameObject.SetActive(true);
        Instance.IsOpen = true;
    }

    public void Close()
    {
        Instance.gameObject.SetActive(false);
        Instance.IsOpen = false;
    }

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            IsOpen = true;

            currentItems = new List<int>();

            //Fill inventory
            currentItems.Add(0);
            currentItems.Add(1);
            currentItems.Add(2);


            int i = 0;
            for (; i < currentItems.Count; i++) {
                Slots[i].SetActive(true);
                Slots[i].GetComponentInChildren<ItemData>().SetItemData(ItemSprites[currentItems[i]], currentItems[i]);
            }
            for (; i < Slots.Length; i++) {
                Slots[i].SetActive(false);
            }

            Close();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private int FindItemsSlotIndex(int dataIndex)
    {
        for (int i = 0; i < currentItems.Count; i++)
        {
            if (Slots[i].GetComponentInChildren<ItemData>().DataIndex == dataIndex) return i;
        }
        return -1;
    }

    public void RemoveItem(int dataIndex)
    {
        int slotIndex = FindItemsSlotIndex(dataIndex);
        if (slotIndex > -1) {
            int followingDataIndex;
            for (; slotIndex < currentItems.Count - 1; slotIndex++)
            {
                followingDataIndex = Slots[slotIndex + 1].GetComponentInChildren<ItemData>().DataIndex;
                Slots[slotIndex].GetComponentInChildren<ItemData>().SetItemData(ItemSprites[followingDataIndex], followingDataIndex);
            }
            currentItems.RemoveAt(currentItems.Count - 1);
            Slots[currentItems.Count].SetActive(false);
        } else {
            Debug.Log("item is not in the inventory");
        }
    }

    public void AddItem(int dataIndex)//, bool duplicates = false
    {
        // We don't want duplicates -> if we already have one return
        int slotIndex = FindItemsSlotIndex(dataIndex);
        //if (!duplicates && slotIndex != -1)
        //    return;
        if (slotIndex > -1) {
            Debug.Log("item is already in the inventory, stack them");
        }
        else {
            if (currentItems.Count < Slots.Length)
            {
                Slots[currentItems.Count].SetActive(true);
                Slots[currentItems.Count].GetComponentInChildren<ItemData>().SetItemData(ItemSprites[dataIndex], dataIndex);
                currentItems.Add(dataIndex);
            }
            else {
                Debug.Log("inventory is full");
            }
        }
    }
}
