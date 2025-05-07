using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class NextMapLoader : MonoBehaviour
{
    [SerializeField] GameObject nextMap;           // 다음 맵 오브젝트
    [SerializeField] int nextMapPos;               // MapManager에서의 다음 맵 번호
    [SerializeField] Transform loadPoint;          // 플레이어가 도착할 위치

    public static event System.Action OnMapMove2;  // 맵 이동 알림용 이벤트

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 맵이 비활성화 상태면 활성화
            if (!nextMap.activeSelf)
            {
                Debug.Log($"{nextMap} 등장!");
                nextMap.SetActive(true);
            }

            // 플레이어 위치 이동
            other.transform.position = loadPoint.position;

            // 안전하게 MapManager 참조 및 카메라 이동 포함
            var mm = FindObjectOfType<MapManager>();
            if (mm != null)
            {
                mm.ChangeMap(nextMapPos);
            }
            else
            {
                Debug.LogError("MapManager 인스턴스를 찾을 수 없습니다! 카메라가 이동하지 않습니다.");
            }

            // 맵 이동 이벤트 발행
            OnMapMove2?.Invoke();
        }
    }
}