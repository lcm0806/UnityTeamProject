using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> spawnerList;

    public List<GameObject> doorList;
    public List<GameObject> loadpointList; 
    private int listLength;

    private void Awake()
    {
        listLength = spawnerList.Count;
        if(doorList != null)
        {
            foreach (var door in doorList)
            {
                door.SetActive(true);
            }
        }
        if(loadpointList != null)
        {
            foreach (var loadpoint in loadpointList)
            {
                loadpoint.SetActive(false);
            }
        }
    }

    public void OnEnemyKilled()
    {
        Debug.Log("利 贸摹");
        listLength--;
        if(listLength == 0 )
        {

            Debug.Log("葛电 利 贸摹!");
            if (doorList != null)
            {
                foreach (var door in doorList)
                {
                    door.SetActive(false);
                }
            }
            if (loadpointList != null)
            {
                foreach (var loadpoint in loadpointList)
                {
                    loadpoint.SetActive(true);
                }
            }
        }
    }
}
