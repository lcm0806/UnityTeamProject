using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class NextMapLoader : MonoBehaviour
{
    [SerializeField] GameObject nextMap;           // ���� �� ������Ʈ
    [SerializeField] int nextMapPos;               // MapManager������ ���� �� ��ȣ
    [SerializeField] Transform loadPoint;          // �÷��̾ ������ ��ġ

    public static event System.Action OnMapMove2;  // �� �̵� �˸��� �̺�Ʈ

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // ���� ��Ȱ��ȭ ���¸� Ȱ��ȭ
            if (!nextMap.activeSelf)
            {
                Debug.Log($"{nextMap} ����!");
                nextMap.SetActive(true);
            }

            // �÷��̾� ��ġ �̵�
            other.transform.position = loadPoint.position;

            // �����ϰ� MapManager ���� �� ī�޶� �̵� ����
            var mm = FindObjectOfType<MapManager>();
            if (mm != null)
            {
                mm.ChangeMap(nextMapPos);
            }
            else
            {
                Debug.LogError("MapManager �ν��Ͻ��� ã�� �� �����ϴ�! ī�޶� �̵����� �ʽ��ϴ�.");
            }

            // �� �̵� �̺�Ʈ ����
            OnMapMove2?.Invoke();
        }
    }
}