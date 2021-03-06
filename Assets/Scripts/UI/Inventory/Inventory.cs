﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Runtime.Serialization;
using System;

public abstract class Inventory : IChangable
{
    public static Inventory Instance { get; private set; }

    public GameObject[] Slots;

    public Sprite[] ItemSprites;
    public string[] ItemNames;

    // Set initial items from Unity inspector in editor
    public List<int> currentItems;

    /// <summary>
    /// Implement by "return this;" to allow casting Instance to derrived classes
    /// </summary>
    /// <returns>"this" of derived class</returns>
    protected abstract Inventory GetInstance();

    // Start - not Awake so we can subscribe to StateManager 
    override protected void Start()
    {
        if (Instance == null)
        {
            Instance = GetInstance();

            int i = 0;
            for (; i < currentItems.Count; i++) {
                Slots[i].SetActive(true);
                Slots[i].GetComponentInChildren<ItemData>().SetItemData(ItemSprites[currentItems[i]], currentItems[i]);
            }
            for (; i < Slots.Length; i++) {
                Slots[i].SetActive(false);
            }

            base.Start();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool Contains(int dataIndex)
    {
        return FindItemsSlotIndex(dataIndex) != -1;
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
