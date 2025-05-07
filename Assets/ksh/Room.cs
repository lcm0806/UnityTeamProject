using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Vector3 GetRandomSpawnPosition()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        Vector3 pos = boxCollider.size;
        
        float x = Random.Range(-pos.x/2, pos.x/2);
        float z = Random.Range(-pos.z/2, pos.z/2);
        
        Vector3 localPos = new Vector3(x, 0, z);
        return transform.TransformPoint(localPos);
    }
}
