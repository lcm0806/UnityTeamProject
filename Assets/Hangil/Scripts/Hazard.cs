using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hazard : MonoBehaviour
{
    public Transform teleportPos;
    [Header("함정에 닿았을 때 발동할 효과들 지정")]
    public UnityEvent OnHazardFall;
    // TODO : 빠질 경우 이펙트 추가하기?
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (teleportPos != null)
            {
                OnHazardFall?.Invoke();
                other.transform.position = new Vector3(teleportPos.position.x, teleportPos.position.y + 1, teleportPos.position.z);
            }
        }
    }

    public void Fall()
    {
        Debug.Log("함정에 빠졌습니다.");
    }
}
