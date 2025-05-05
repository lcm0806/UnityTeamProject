using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BossBullet_ObjectPool : MonoBehaviour
{
    [SerializeField] List<PooledObject> pool = new List<PooledObject>(); //오브젝트 풀을 구현할 자료형
    [SerializeField] PooledObject prefab; // 풀링될 오브젝트
    [SerializeField] int size; //최대수량

    private void Awake()
    {
        for (int i = 0; i < size; i++) //최대수량을 지정한 만큼 만들어서 오브젝트 풀 생성
        {
            PooledObject instance = Instantiate(prefab);
            instance.gameObject.SetActive(false);
            pool.Add(instance);
        }
    }

    public PooledObject GetPool(Vector3 position, Quaternion rotation) //빌리기
    {
        if (pool.Count == 0)
        {
            return Instantiate(prefab, position, rotation);
        }
        
        PooledObject instance = pool[pool.Count - 1];
        pool.RemoveAt(pool.Count -1);

        instance.returnPool = this;
        instance.transform.position = position;
        instance.transform.rotation = rotation;      
        instance.gameObject.SetActive(true);

        return instance;
    }

    public void ReturnPool(PooledObject instance) //반납하기
    { 
        instance.gameObject.SetActive(false);
        pool.Add(instance);
    }

}
