using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hazard : MonoBehaviour
{
    //public Transform teleportPos;
    public Transform[] teleportPosList;
    public float teleportYPosPadding = 5.5f;
    [Header("함정에 닿았을 때 발동할 효과들 지정")]
    public UnityEvent OnHazardFall;
    // TODO : 빠질 경우 이펙트 추가하기?
    private int posListCount;
    private void Start()
    {
        posListCount = teleportPosList.Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (teleportPosList != null)
            {
                OnHazardFall?.Invoke();
                int teleRandNum = Random.Range(0, posListCount-1);
                other.transform.position = new Vector3(teleportPosList[teleRandNum].position.x, 
                                                       teleportPosList[teleRandNum].position.y + teleportYPosPadding,
                                                       teleportPosList[teleRandNum].position.z);
            }
        }
    }

    public void Fall()
    {
        Debug.Log("함정에 빠졌습니다.");
    }
}
