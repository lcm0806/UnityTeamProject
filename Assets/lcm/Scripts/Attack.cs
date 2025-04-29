using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public enum Type { Book_of_Belial , Shoop_da_Whoop, The_Nail, Bobs_Rotten_Head, Tammys_Head, Book_of_Revelations, Anarchist_Cookbook, Vampiric_Fang }
    [SerializeField] private int damage;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform muzzlePoint;

    private Camera mainCamera;
    private Vector3 mousePos;
    private bool isFire = true;
    private float timer;
    private float delaytime = 0.2f;

    [Range(10, 30)]
    [SerializeField] float bulletSpeed;

    
    public void Fire()
    {
        GameObject instance = Instantiate(bulletPrefab, muzzlePoint.position, Quaternion.LookRotation(Vector3.forward));
        Rigidbody bulletRigidbody = instance.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = muzzlePoint.forward * bulletSpeed;

    }

    

}
