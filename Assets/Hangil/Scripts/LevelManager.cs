using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("해당 맵의 적 스포너 프리팹 갯수")]
    [SerializeField] List<GameObject> spawnerList;
    [Header("해당 맵의 문 갯수")]
    [SerializeField] List<GameObject> doorList;
    [Header("해당 맵의 로딩포인트 갯수")]
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
            Debug.Log($"{gameObject.name} 비활성화");
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
        Debug.Log("적 처치");
        listLength--;
        if(listLength == 0 )
        {
            Debug.Log("모든 적 처치!");
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
