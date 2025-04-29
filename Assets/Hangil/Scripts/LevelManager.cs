using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // 각각의 레벨 내 적들의 존재를 관리하는 컴포넌트
    // 적 스포너 갯수를 보유해야 함
    // 적이 0명이 되면 문 오브젝트를 비활성화
    // 적 갯수 = 스포너 갯수로 설정

    [SerializeField] List<GameObject> enemySpanwers;
    [SerializeField] List<GameObject> Doors;
    private int enemyCount;
    public int EnemyCount { get { return enemyCount; } set { enemyCount = value; } }
    // Start is called before the first frame update
    void Start()
    {
        enemyCount = enemySpanwers.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCount == 0)
        {
            OpenDoors();
        }
    }
    public void OpenDoors()
    {
        foreach(var door in Doors)
        {
            door.SetActive(false);
        }
    }
}
