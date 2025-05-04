using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SixBomb : MonoBehaviour
{
    private bool playerInRoom = false;
    public GameObject BombPrefab;
    private int Bombcount = 6;
    private Room room;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Room"))
        {
            playerInRoom = true;
            room = other.GetComponent<Room>();
            Debug.Log("범위안에 들어왔습니다.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Room"))
        {
            playerInRoom = false;
            room = null;
            Debug.Log("플레이어가 범위밖을 나갔습니다.");
        }
    }

    public void SpawnBomb()
    {
        if (playerInRoom)
        {
            RandomSpawnBomb();
        }
    }
    
    public void RandomSpawnBomb()
    {
        Debug.Log("폭탄 발동");
        RaycastHit hit;

        for (int i = 0; i < Bombcount; i++)
        {
            Vector3 randompos = room.GetRandomSpawnPosition();
            if (Physics.Raycast(randompos, Vector3.down, out hit))
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    Instantiate(BombPrefab, hit.point, Quaternion.identity);
                }
            }
        }
    }
}
