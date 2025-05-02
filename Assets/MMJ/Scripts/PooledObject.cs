using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    // 다 썼을때 돌아가기 
    public BossBullet_ObjectPool returnPool;
    [SerializeField] float returnTime;
    private float timer;


    private void OnEnable()
    {
        timer = returnTime;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        { 
            ReturnPool();
        }
    }
    public void ReturnPool()
    {
        if (returnPool == null)
        {
            Destroy(gameObject);
        }
        returnPool.ReturnPool(this);
    }
}
