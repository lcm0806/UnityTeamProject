using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public Item itemInstance;

    public void Init(Item item)
    {
        itemInstance = item;

        //   if (itemInstance.itemIcon == null)
        {
            string iconName = itemInstance.itemName.Replace(" ", "").ToLower();  // ex: "Sad Onion" → "sadonion"
                                                                                 //       itemInstance.itemIcon = Resources.Load<Sprite>($"ImageSource/{iconName}");

            //       if (itemInstance.itemIcon == null)
            {
                Debug.LogWarning($"[ItemHolder] 아이콘 로드 실패: {iconName}");
            }
         //   else
            {
                Debug.Log($"[ItemHolder] 아이콘 로드 성공: {iconName}");
            }
        }
    }
}