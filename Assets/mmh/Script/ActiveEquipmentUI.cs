using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ActiveEquipmentUI : MonoBehaviour
{
    public Image[] activeSlots;  // 인스펙터에서 직접 할당할 슬롯들
    private Queue<Sprite> activeQueue = new Queue<Sprite>();

    void Start()
    {
        foreach (var slot in activeSlots)
        {
            if (slot != null)
            {
                slot.sprite = null;
                slot.enabled = false;  // 처음엔 아무것도 안 보이게
            }
        }
    }

    public void AddActiveItem(Sprite icon)
    {
        if (icon == null) return;

        if (activeSlots == null || activeSlots.Length == 0) return;

        activeQueue.Enqueue(icon);
        if (activeQueue.Count > activeSlots.Length)
        {
            activeQueue.Dequeue();
        }

        int index = 0;
        foreach (var item in activeQueue)
        {
            activeSlots[index].sprite = item;
            activeSlots[index].enabled = true;  // 중요: 슬롯을 활성화해야 보여짐!
            index++;
        }

        // 남은 슬롯 비우기
        for (int i = index; i < activeSlots.Length; i++)
        {
            activeSlots[i].sprite = null;
            activeSlots[i].enabled = false;
        }
    }

    public void ResetUI()
    {
        foreach (var slot in activeSlots)
        {
            slot.sprite = null;
            slot.enabled = false;
        }

        // 포인터 리셋
        //currentIndex = 0;
    }
}