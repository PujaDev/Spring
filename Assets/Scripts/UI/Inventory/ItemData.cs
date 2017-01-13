using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;
using System;

public class ItemData : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private bool IsDragged;

    public int DataIndex;

    private Sprite Sprite { get { return gameObject.GetComponent<Image>().sprite; } }

    public void SetItemData(Sprite image, int index)
    {
        gameObject.GetComponent<Image>().sprite = image;
        DataIndex = index;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ItemHolder.Instance.GetComponent<ItemHolder>().StartHolding(DataIndex, Sprite);
        Inventory.Instance.RemoveItem(DataIndex);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!GameController.Instance.isUI)
        {
            SceneController.Instance.title.text = Inventory.Instance.ItemNames[DataIndex];
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SceneController.Instance.title.text = "";
    }
}
