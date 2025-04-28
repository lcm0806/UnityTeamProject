using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextMapLoader : MonoBehaviour
{
    [SerializeField] GameObject nextMap;
    [SerializeField] Transform loadPoint;
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
        }
    }
}
