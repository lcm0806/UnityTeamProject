using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class NextMapLoader : MonoBehaviour
{
    [SerializeField] GameObject nextMap;
    [SerializeField] int nextMapPos;
    [SerializeField] Transform loadPoint;
    [SerializeField] UnityEvent OnMapMove;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(!nextMap.activeSelf)
            {
                Debug.Log($"{nextMap} ·Îµù");
                nextMap.SetActive(true);
            }
            other.gameObject.transform.position = loadPoint.position;
            CameraManager.currentMap = nextMapPos;
            OnMapMove?.Invoke();
        }
    }
}
