using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] int hp;
    [SerializeField] int attackDamage;
    [SerializeField] float speed;
    [SerializeField] LayerMask targetLayer;
    [SerializeField] Transform targetPos;
    [SerializeField] Transform MuzzleRos;

    private void DetectPlayer()
    {
        //todo
    }

    private void Move()
    {
        //todo
    }

    private void Attack()
    {
        //todo
    }
    private void Die()
    {
        //todo
    }
}
