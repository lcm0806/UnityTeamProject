using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;
    public static int currentMap;
    [SerializeField] Camera mainCamera;
    public List<GameObject> Maps;
    public int startMapPos;
    [SerializeField] List<Transform> cameraPos;
    // Start is called before the first frame update
    void Awake()
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
        currentMap = startMapPos;
        foreach (var map in Maps)
        {
            map.gameObject.SetActive(false);
        }
        Maps[startMapPos-1].SetActive(true);
    }

    public void MoveCamera()
    {
        mainCamera.transform.position = cameraPos[currentMap - 1].position;
    }
}
