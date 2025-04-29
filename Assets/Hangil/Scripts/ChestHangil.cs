using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestHangil : MonoBehaviour
{
    // 1. 플레이어가 열쇠에 닿는다.
    // 2. 열쇠가 비활성화되고 상자도 비활성화된다
    // 3. 상자는 비활성화되고, 상자의 위치에 아이템이 등장한다.
    [SerializeField] GameObject item;
    // Start is called before the first frame update
    public void Open()
    {
        gameObject.SetActive(false);
        GameObject tresure = Instantiate(item, transform.position, Quaternion.identity);
    }
}
