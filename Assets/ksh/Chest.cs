using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
   public List<Item> items = new List<Item>();
    public Transform ItemSpawn;
    private float ThrowForce = 5f;

    public void OpenChest()
    {
        
       // Rigidbody rb = item.GetComponent<Rigidbody>();
       // 
       // if(rb != null)
       // {
       //     rb.AddForce(Vector3.up * ThrowForce, ForceMode.Impulse);
       // }
        
    }
}
