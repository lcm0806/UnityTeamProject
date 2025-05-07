using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("��ȯ�� ���� ������")]
    [SerializeField] GameObject enemyPrefab;
    [Header("����� : ��ȯ�� ���� �� ��ġ")]
    [SerializeField] int levelNumber;
    private EnemyDeath spawnedEnemy;
    [Header("���� ���� ��� �߻��� �̺�Ʈ")]
    [SerializeField] UnityEvent OnEnemyKilled;
    private void Start()
    {
        SpawnEnemy();
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
