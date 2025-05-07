using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("소환할 적의 프리팹")]
    [SerializeField] GameObject enemyPrefab;
    [Header("디버깅 : 소환될 적의 맵 위치")]
    [SerializeField] int levelNumber;
    private EnemyDeath spawnedEnemy;
    [Header("적이 죽을 경우 발생할 이벤트")]
    [SerializeField] UnityEvent OnEnemyKilled;
    private void Start()
    {
        //SpawnEnemy();
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    public void SpawnEnemy()
    {
        GameObject enemyObj = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemyObj.name = $"Level{levelNumber}Monster";
        spawnedEnemy = enemyObj.GetComponent<EnemyDeath>();

        //if (spawnedEnemy != null) { spawnedEnemy.OnDeath += HandleEnemyDeath; }
    }

    public void OnDestroy()
    {
        //if (spawnedEnemy != null) { spawnedEnemy.OnDeath -= HandleEnemyDeath; }
    }

    private void HandleEnemyDeath()
    {
        OnEnemyKilled?.Invoke();
    }
}
