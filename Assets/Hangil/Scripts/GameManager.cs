using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int currentMap;
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject[] Maps;
    [SerializeField] Transform[] cameraPos;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
        currentMap = 0;

        foreach(var map in Maps)
        {
            map.gameObject.SetActive(false);
        }
        Maps[0].SetActive(true);
    }

    public void HazardDrop()
    {
        Debug.Log("함정에 빠졌습니다");
    }

    public void MoveCamera()
    {
        mainCamera.transform.position = cameraPos[currentMap-1].position;
    }
}
