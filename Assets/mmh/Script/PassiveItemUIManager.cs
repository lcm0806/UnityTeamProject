using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassiveItemUIManager : MonoBehaviour
{
    public Transform iconParent;              // PassiveItemUI Panel
    public GameObject iconSlotPrefab;         // ItemIconSlot ÇÁ¸®ÆÕ

    public void AddPassiveItemIcon(Sprite icon)
    {
        GameObject newSlot = Instantiate(iconSlotPrefab, iconParent);
        newSlot.GetComponentInChildren<Image>().sprite = icon;
    }
}