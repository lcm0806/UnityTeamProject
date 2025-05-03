using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    public int damage;
    public bool isMelee;
    public bool isrock;


    private void OnCollisionEnter(Collision collision)
    {
        if (!isrock && collision.gameObject.tag == "Floor")
        {
            Destroy(gameObject, 3);
        }
        else if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isMelee && other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        if (!isMelee && other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

}
