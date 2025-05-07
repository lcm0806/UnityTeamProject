using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombLogic : MonoBehaviour
{
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private float explosionDamage = 20f;
    [SerializeField] private GameObject explosionEffectPrefab;
    [SerializeField] private float explosionDelay = 0f;




    public void Explode()
    {
        if (explosionDelay > 0)
        {
            Invoke("PerformExplosion", explosionDelay);
        }
        else
        {
            PerformExplosion();
        }
    }

    private void PerformExplosion()
    {
        // 1. 시각 효과 생성
        if (explosionEffectPrefab != null)
        {

        }
        else
        {
            Debug.LogWarning("폭발 효과 프리팹이 할당되지 않았습니다.");
            // 기본적인 시각 효과 생성 (선택 사항)
        }

        // 2. 주변 오브젝트에 데미지 적용
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider hitCollider in hitColliders)
        {
            Monster monster = hitCollider.GetComponent<Monster>();
            if (monster != null)
            {
                Debug.Log($"{monster.gameObject.name}이 폭발에 맞아 {explosionDamage} 데미지를 입었습니다.");
                //monster.TakeDamage(explosionDamage);
                // 넉백 등 추가 효과
                Rigidbody rb = monster.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 explosionDirection = (monster.transform.position - transform.position).normalized;
                    rb.AddForce(explosionDirection * 5f, ForceMode.Impulse);
                }
            }
            // 다른 오브젝트에 대한 처리 추가 가능
        }

        // 3. 폭탄 오브젝트 제거
        Destroy(gameObject);
    }
}
