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
    private bool isHomingBulletEnabled = false;


    public void SetTripleShot(bool enabled)
    {
        isTripleShotEnabled = enabled;
    }

    public void Set8WayShot(bool enabled)
    {
        is8WayShotEnabled = enabled;
    }
    public void SetHomingBullet(bool enabled)
    {
        isHomingBulletEnabled = enabled;
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
        Rigidbody bulletRigidbody = instance.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = muzzlePoint.forward * Player.Instance.BulletSpeed;
        }
        else
        {
            Debug.LogError("생성된 총알 오브젝트에 Rigidbody 컴포넌트가 없습니다.");
            Destroy(instance);
            return;
        }

        // 불렛 크기 적용
        float bulletScale = Player.Instance.GetCurrentBulletScale();
        instance.transform.localScale = Vector3.one * bulletScale;

        Bullet bulletComponent = instance.GetComponent<Bullet>();
        if (bulletComponent != null)
        {
            bulletComponent.SetDamage(Damage);
        }
        else
        {
            Debug.LogError("생성된 총알 오브젝트에 Bullet 스크립트가 없습니다.");
            Destroy(instance);
        }
    }
    private void FireTripleShot(float currentDamage)
    {
        // 발사 각도 조절 (원하는 각도 값으로 변경)
        Quaternion rotation1 = muzzlePoint.rotation * Quaternion.Euler(30f, 0f, 0f);
        Quaternion rotation2 = muzzlePoint.rotation;
        Quaternion rotation3 = muzzlePoint.rotation * Quaternion.Euler(-30f, 0f, 0f);

        // 첫 번째 총알 발사
        GameObject instance1 = Instantiate(bulletPrefab, muzzlePoint.position, rotation1);
        Rigidbody rb1 = instance1.GetComponent<Rigidbody>();
        if (rb1 != null) rb1.velocity = rotation1 * Vector3.forward * Player.Instance.BulletSpeed;
        Bullet bullet1 = instance1.GetComponent<Bullet>();
        if (bullet1 != null) bullet1.SetDamage(currentDamage);
        float bulletScale1 = Player.Instance.GetCurrentBulletScale();
        instance1.transform.localScale = Vector3.one * bulletScale1;

        // 두 번째 총알 발사 (정면)
        GameObject instance2 = Instantiate(bulletPrefab, muzzlePoint.position, rotation2);
        Rigidbody rb2 = instance2.GetComponent<Rigidbody>();
        if (rb2 != null) rb2.velocity = rotation2 * Vector3.forward * Player.Instance.BulletSpeed;
        Bullet bullet2 = instance2.GetComponent<Bullet>();
        if (bullet2 != null) bullet2.SetDamage(currentDamage);
        float bulletScale2 = Player.Instance.GetCurrentBulletScale();
        instance2.transform.localScale = Vector3.one * bulletScale2;

        // 세 번째 총알 발사
        GameObject instance3 = Instantiate(bulletPrefab, muzzlePoint.position, rotation3);
        Rigidbody rb3 = instance3.GetComponent<Rigidbody>();
        if (rb3 != null) rb3.velocity = rotation3 * Vector3.forward * Player.Instance.BulletSpeed;
        Bullet bullet3 = instance3.GetComponent<Bullet>();
        if (bullet3 != null) bullet3.SetDamage(currentDamage);
        float bulletScale3 = Player.Instance.GetCurrentBulletScale();
        instance3.transform.localScale = Vector3.one * bulletScale3;
    }

    private void Fire8WayShot(float currentDamage)
    {
        if (bulletPrefab != null && muzzlePoint != null && Player.Instance != null)
        {
            for (int i = 0; i < 8; i++)
            {
                // 360도를 8등분한 각도 계산
                float angle = i * 360f / 8f;
                Quaternion rotation = muzzlePoint.rotation * Quaternion.Euler(angle, 0f, 0f);

                // 총알 생성
                GameObject instance = Instantiate(bulletPrefab, muzzlePoint.position, rotation);
                Rigidbody bulletRigidbody = instance.GetComponent<Rigidbody>();

                if (bulletRigidbody != null)
                {
                    // 발사 방향 설정
                    bulletRigidbody.velocity = rotation * Vector3.forward * Player.Instance.BulletSpeed;

                    // 총알 크기 적용
                    float bulletScale = Player.Instance.GetCurrentBulletScale();
                    instance.transform.localScale = Vector3.one * bulletScale;

                    // 데미지 설정 (Bullet 스크립트가 있다고 가정)
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
                else
                {
                    Debug.LogError("생성된 총알 오브젝트에 Rigidbody 컴포넌트가 없습니다.");
                    Destroy(instance);
                }
            }
        }
        else
        {
            Debug.LogError("총알 프리팹, 발사 위치, 또는 Player 인스턴스가 할당되지 않았습니다.");
        }



    }

}
