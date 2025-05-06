using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EquipmentUIManager : MonoBehaviour
{
    // 슬롯 2개 연결
    public Image[] itemSlots = new Image[2];

    // 현재 슬롯에 표시된 스프라이트 큐 (선입선출)
    private Queue<Sprite> itemSpriteQueue = new Queue<Sprite>();

    // 아이템 이미지 추가
    public void AddItemToUI(Sprite itemSprite)
    {
        // 2개가 이미 차있으면 가장 오래된 것 제거
        if (itemSpriteQueue.Count >= 2)
        {
            itemSpriteQueue.Dequeue(); // 큐에서 제거
        }

        itemSpriteQueue.Enqueue(itemSprite); // 새 이미지 추가

        // UI에 반영
        UpdateUISlots();
    }

    // 슬롯 이미지 실제 갱신
    private void UpdateUISlots()
    {
        Sprite[] sprites = itemSpriteQueue.ToArray();

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (i < sprites.Length)
            {
                itemSlots[i].sprite = sprites[i];
                itemSlots[i].enabled = true;
            }
            else
            {
                itemSlots[i].sprite = null;
                itemSlots[i].enabled = false;
            }
        }
    }
}