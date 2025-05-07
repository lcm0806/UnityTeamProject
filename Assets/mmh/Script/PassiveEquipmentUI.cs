using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PassiveEquipmentUI : MonoBehaviour
{
    [Header("UI에 연결할 패시브 슬롯 이미지들 (10개)")]
    public Image[] passiveSlots; // 10개 슬롯을 Inspector에 배열로 연결
    private Queue<Sprite> passiveQueue = new Queue<Sprite>();

    void Start()
    {
        // 시작 시 모든 슬롯 초기화
        foreach (var slot in passiveSlots)
        {
            slot.sprite = null;
            slot.enabled = false;
        }
    }

    public void AddPassiveItem(Sprite icon)
    {
        if (icon == null)
        {
            Debug.LogWarning("PassiveEquipmentUI: 받은 아이콘이 null입니다!");
            return;
        }

        if (passiveSlots == null || passiveSlots.Length == 0)
        {
            Debug.LogError("PassiveEquipmentUI: passiveSlots가 연결되지 않았습니다!");
            return;
        }

        passiveQueue.Enqueue(icon);

        // 슬롯 개수 초과 시 가장 오래된 것 제거
        if (passiveQueue.Count > passiveSlots.Length)
        {
            passiveQueue.Dequeue();
        }

        UpdateSlots();
    }

    void UpdateSlots()
    {
        Sprite[] currentIcons = passiveQueue.ToArray();

        for (int i = 0; i < passiveSlots.Length; i++)
        {
            if (i < currentIcons.Length && currentIcons[i] != null)
            {
                passiveSlots[i].sprite = currentIcons[i];
                passiveSlots[i].enabled = true;
            }
            else
            {
                passiveSlots[i].sprite = null;
                passiveSlots[i].enabled = false;
            }
        }
    }

    public void ResetUI()
    {
        foreach (var slot in passiveSlots) 
        {
            slot.sprite = null;
            slot.enabled = false;
        }

        // 포인터 리셋
        //currentIndex = 0;
    }
}