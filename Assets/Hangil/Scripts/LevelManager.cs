using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("�ش� ���� �� ������ ������ ����")]
    [SerializeField] List<GameObject> spawnerList;
    [Header("�ش� ���� �� ����")]
    [SerializeField] List<GameObject> doorList;
    [Header("�ش� ���� �ε�����Ʈ ����")]
    [SerializeField] List<GameObject> loadpointList; 
    private int listLength;

    private void Start()
    {
        if(gameObject.activeSelf == true)
        {
            if (spawnerList != null)
            {
                listLength = spawnerList.Count;
                ActivateAllSpawners();
            }
            if (doorList != null)
            {
                foreach (var door in doorList)
                {
                    if (door != null) { door.SetActive(true); }
                }
            }
            if (loadpointList != null)
            {
                foreach (var loadpoint in loadpointList)
                {
                    if (loadpoint != null) { loadpoint.SetActive(false); }
                }
            }
        }
        else
        {
            Debug.Log($"{gameObject.name} ��Ȱ��ȭ");
        }
    }

    public void ActivateAllSpawners()
    {
        foreach (var spawner in spawnerList)
        {
            if (spawner != null) { spawner.SetActive(true); }
        }
    }
    public void OnEnemyKilled()
    {
        Debug.Log("�� óġ");
        listLength--;
        if(listLength == 0 )
        {
            Debug.Log("��� �� óġ!");
            if (doorList != null)
            {
                foreach (var door in doorList)
                {
                    if (door != null) { door.SetActive(false); }
                }
            }
            if (loadpointList != null)
            {
                foreach (var loadpoint in loadpointList)
                {
                    if (loadpoint != null) { loadpoint.SetActive(true); }
                }
            }
        }
    }

}
