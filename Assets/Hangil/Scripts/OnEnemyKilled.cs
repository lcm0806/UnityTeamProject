using System.Collections;
using System.Collections.Generic;
using UnityEditor.VisionOS;
using UnityEngine;
using UnityEngine.Events;

public class OnEnemyKilled : MonoBehaviour
{
    [SerializeField] UnityEvent OnKilled;
    private void OnDisable()
    {
        OnKilled?.Invoke();
    }

    public void Dead()
    {

    }
}
