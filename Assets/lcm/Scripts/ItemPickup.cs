using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item; // 습득할 아이템

    // Inspector 창에서 아이템 할당을 용이하게 하기 위한 방법 (선택 사항)
    public string itemNameForEditor;

    private void OnValidate()
    {
        // 에디터에서 itemNameForEditor 기반으로 아이템 생성 (간단한 예시)
        if (!string.IsNullOrEmpty(itemNameForEditor))
        {
            switch (itemNameForEditor)
            {
                case "SadOnion":
                    item = new SadOnion();
                    break;
                case "TheInnerEye":
                    item = new TheInnerEye();
                    break;
                case "Pentagram":
                    item = new Pentagram();
                    break;
                // 다른 아이템 케이스 추가
                default:
                    item = null;
                    break;
            }
            itemNameForEditor = ""; // 값 초기화
        }
    }

}
