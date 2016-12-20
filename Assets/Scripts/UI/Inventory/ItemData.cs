using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;
using System;

public class ItemData : MonoBehaviour, IPointerClickHandler
{

    private bool IsDragged = false;

    public int DataIndex;

    public void SetItemData(Sprite image, int index) {
        gameObject.GetComponent<Image>().sprite = image;
        DataIndex = index;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && !IsDragged)
        {
            //IsDragged = true;
            //OriginalPosition = transform.position;
            //OriginalParent = transform.parent;

            //ItemHolder.Instance.GetComponent<ItemHolder>().StartHolding(Item);
            //transform.SetParent(ItemHolder.Instance.transform);

            Inventory.Instance.Close();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            //ReturnToInventory();
        }
    }

    public void ReturnToInventory()
    {
        //IsDragged = false;
        //transform.SetParent(OriginalParent);
        //transform.position = OriginalPosition;
        //SantaController.controller.changeClothes(Item.Name, true);
    }
}
