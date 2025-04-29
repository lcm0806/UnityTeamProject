using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    public static 
        int currentMap;
    [SerializeField] Camera mainCamera;
    public List<GameObject> Maps;
    [SerializeField] List<Transform> cameraPos;
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
        currentMap = 3;
        foreach (var map in Maps)
        {
            map.gameObject.SetActive(false);
        }
        Maps[2].SetActive(true);
    }

    public void MoveCamera()
    {
        mainCamera.transform.position = cameraPos[currentMap - 1].position;
    }
}
