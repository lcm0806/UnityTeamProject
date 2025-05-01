using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    private EnemyDeath spawnedEnemy;

    private void Start()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        GameObject enemyObj = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        spawnedEnemy = enemyObj.GetComponent<EnemyDeath>();

        if (spawnedEnemy != null)
        {
            spawnedEnemy.OnDeath += HandleEnemyDeath;
        }
    }

    public void OnDestroy()
    {
        if (spawnedEnemy != null)
        {
            spawnedEnemy.OnDeath -= HandleEnemyDeath;
        }
    }

    private void HandleEnemyDeath()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.OnEnemyKilled();
        }
    }
}
