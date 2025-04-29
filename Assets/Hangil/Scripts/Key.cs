using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Key : MonoBehaviour
{
    [SerializeField] UnityEvent OnCollide;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("플레이어가 열쇠를 획득하였습니다.");
            OnCollide?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
