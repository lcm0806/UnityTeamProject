using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public event Action OnDeath; // 죽음 이벤트

    public void OnDisable()
    {
        Die();
    }

    public void Die()
    {
        OnDeath?.Invoke(); // 이벤트 호출
        Destroy(gameObject);
    }
}
