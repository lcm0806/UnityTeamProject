using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private float damage;
    public float Damage
    {
        get => damage;
        set => damage = value;
    }

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform muzzlePoint;
    
    public void Fire(float damage)
    {
        get => bulletSpeed;
        set => bulletSpeed = value;
    }

    public void Fire(int damage)
    {
        Debug.Log(muzzlePoint.forward);

        GameObject instance = Instantiate(bulletPrefab, muzzlePoint.position, Quaternion.LookRotation(Vector3.forward));
        Rigidbody bulletRigidbody = instance.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = muzzlePoint.forward * Player.Instance.BulletSpeed;

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
