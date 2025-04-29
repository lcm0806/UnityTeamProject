using System.Collections;
using System.Collections.Generic;
using UnityEditor.VisionOS;
using UnityEngine;
using UnityEngine.Events;

public class OnEnemyKilled : MonoBehaviour
{
    public GameObject levelManager;
    [SerializeField] UnityEvent OnKilled;
    private void OnDisable()
    {
        OnKilled?.Invoke();
    }

    public void Dead()
    {
        LevelManager dead = levelManager.GetComponent<LevelManager>();
        dead.EnemyCount--;
    }
}
