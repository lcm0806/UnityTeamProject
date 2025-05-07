using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDeath : MonoBehaviour
{
    // Á×À½ ÀÌº¥Æ®
    public UnityEvent OnDeath;
    public void OnDisable()
    {
        if(OnDeath != null) { Die(); }
    }

    public void Die()
    {
        OnDeath?.Invoke();
    }
}
