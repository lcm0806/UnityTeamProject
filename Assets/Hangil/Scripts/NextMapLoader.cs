using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextMapLoader : MonoBehaviour
{
    public string nextMapName;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("´ÙÀ½ ¾À ·Îµù");
            SceneManager.LoadScene(nextMapName);
        }
    }
}
