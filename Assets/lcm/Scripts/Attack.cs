using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public enum Type { Book_of_Belial, Shoop_da_Whoop, The_Nail, Bobs_Rotten_Head, Tammys_Head, Book_of_Revelations, Anarchist_Cookbook, Vampiric_Fang }
    [SerializeField] private float damage;
    public float Damage
    {
        get => damage;
        set => damage = value;
    }

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform muzzlePoint;

    private Camera mainCamera;
    private Vector3 mousePos;
    private bool isFire = true;
    private float timer;
    private float delaytime = 0.2f;

    [Range(10, 30)]
    [SerializeField] private float bulletSpeed;
    public float BulletSpeed
    {
        get => bulletSpeed;
        set => bulletSpeed = value;
    }

    public void Fire(int damage)
    {
        muzzlePoint.rotation = Quaternion.Euler(0f, 0f, 0f);
        Debug.Log(muzzlePoint.forward);

        GameObject instance = Instantiate(bulletPrefab, muzzlePoint.position, Quaternion.LookRotation(Vector3.forward));
        Rigidbody bulletRigidbody = instance.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = muzzlePoint.forward * bulletSpeed;

        Bullet bulletComponent = instance.GetComponent<Bullet>();
        if (bulletComponent != null)
        {
            bulletComponent.SetDamage(damage);
        }
        else
        {
            Debug.LogError("생성된 총알 오브젝트에 Bullet 스크립트가 없습니다.");
        }
    }
}