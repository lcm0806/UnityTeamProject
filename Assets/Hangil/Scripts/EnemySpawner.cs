using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
    }
}
