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

    private bool isTripleShotEnabled = false;
    private bool is8WayShotEnabled = false;

    public void SetTripleShot(bool enabled)
    {
        isTripleShotEnabled = enabled;
    }

    public void Set8WayShot(bool enabled)
    {
        is8WayShotEnabled = enabled;
    }


    public void Fire(float damage)
    {
        if (bulletPrefab != null && muzzlePoint != null && Player.Instance != null)
        {
            if (isTripleShotEnabled)
            {
                // 3갈래로 발사
                FireTripleShot(damage);
            }
            else if (is8WayShotEnabled)
            {
                // 8방향으로 발사
                Fire8WayShot(damage);
            }
            else
            {
                // 단발 발사 (기존 로직)
                FireSingleShot(damage);
            }
        }
        else
        {
            Debug.LogError("총알 프리팹, 발사 위치, 또는 Player 인스턴스가 할당되지 않았습니다.");
        }
    }

    private void FireSingleShot(float Damage)
    {
        GameObject instance = Instantiate(bulletPrefab, muzzlePoint.position, muzzlePoint.rotation);
        InitializeBullet(instance, muzzlePoint.forward, Damage);
    }
    private void FireTripleShot(float currentDamage)
    {
        // 발사 각도 조절 (원하는 각도 값으로 변경)
        Quaternion rotation1 = muzzlePoint.rotation * Quaternion.Euler(30f, 0f, 0f);
        Quaternion rotation2 = muzzlePoint.rotation;
        Quaternion rotation3 = muzzlePoint.rotation * Quaternion.Euler(-30f, 0f, 0f);

        // 첫 번째 총알 발사
        GameObject instance1 = Instantiate(bulletPrefab, muzzlePoint.position, rotation1);
        InitializeBullet(instance1, rotation1 * Vector3.forward, currentDamage);

        // 두 번째 총알 발사 (정면)
        GameObject instance2 = Instantiate(bulletPrefab, muzzlePoint.position, rotation2);
        InitializeBullet(instance2, rotation2 * Vector3.forward, currentDamage);

        // 세 번째 총알 발사
        GameObject instance3 = Instantiate(bulletPrefab, muzzlePoint.position, rotation3);
        InitializeBullet(instance3, rotation3 * Vector3.forward, currentDamage);
    }

    private void Fire8WayShot(float currentDamage)
    {
        if (bulletPrefab != null && muzzlePoint != null && Player.Instance != null)
        {
            for (int i = 0; i < 8; i++)
            {
                float angle = i * 360f / 8f;
                Quaternion rotation = muzzlePoint.rotation * Quaternion.Euler(angle, 0f, 0f);
                GameObject instance = Instantiate(bulletPrefab, muzzlePoint.position, rotation);
                InitializeBullet(instance, rotation * Vector3.forward, currentDamage);
            }
        }
        else
        {
            Debug.LogError("총알 프리팹, 발사 위치, 또는 Player 인스턴스가 할당되지 않았습니다.");
        }
    }

    private void InitializeBullet(GameObject instance, Vector3 initialDirection, float currentDamage)
    {
        Rigidbody bulletRigidbody = instance.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = initialDirection * Player.Instance.BulletSpeed;
        }
        else
        {
            Debug.LogError("생성된 총알 오브젝트에 Rigidbody 컴포넌트가 없습니다.");
            Destroy(instance);
            return;
        }


        float bulletScale = Player.Instance.CurrentBulletScale;
        instance.transform.localScale = Vector3.one * bulletScale;

        Bullet bulletComponent = instance.GetComponent<Bullet>();
        if (bulletComponent != null)
        {
            bulletComponent.SetDamage(currentDamage);
        }
        else
        {
            Debug.LogError("생성된 총알 오브젝트에 Bullet 스크립트가 없습니다.");
            Destroy(instance);
        }
    }

}
