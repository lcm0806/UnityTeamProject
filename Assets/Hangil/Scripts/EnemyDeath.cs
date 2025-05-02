using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public event Action OnDeath; // Á×À½ ÀÌº¥Æ®

    public void OnDisable()
    {
        if(OnDeath != null) { Die(); }
    }

    public void Die()
    {
        OnDeath?.Invoke();
    }
}
