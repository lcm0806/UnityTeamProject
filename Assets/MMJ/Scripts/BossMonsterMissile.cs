using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterMissile : MonsterBullet
{
    public Transform target;
    public float moveSpeed;

  
    private void Trace()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        transform.LookAt(target.transform.position);
    }
}
