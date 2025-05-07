using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float damageAmount;
    [SerializeField] private float homingSpeed = 10f; // 유도 속도
    private Transform target; // 추적할 대상
    private bool isHomingEnabled = false;
    public bool IsHomingEnabled { get { return isHomingEnabled; }  set { isHomingEnabled = value; } }
    private Rigidbody rb;
    [SerializeField] private float rotationSpeed = 5f;
    private float initialY; // 총알의 초기 Y 좌표
    private bool hasTarget = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialY = transform.position.y; // 초기 Y 좌표 저장

        // 호밍 기능이 활성화되면 가장 가까운 몬스터를 찾고 초기 방향을 설정합니다.
        if (isHomingEnabled)
        {
            FindClosestMonsterAndSetInitialDirection();
        }
    }

    void FixedUpdate()
    {
        if (isHomingEnabled && target != null)
        {
            Vector3 targetPosition = new Vector3(target.position.x, initialY, target.position.z);
            Vector3 direction = (targetPosition - transform.position).normalized;

            // 부드럽게 회전
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

            // 전방으로 이동 (기존 속도 유지)
            rb.velocity = transform.forward * homingSpeed;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.tag == "Monster")
        {
            Monster monster = collision.gameObject.GetComponent<Monster>();
            if (monster != null)
            { 
                Destroy(gameObject);     
            }
            else
            {
                Debug.LogError("'Monster' 태그가 붙었지만 Monster 스크립트가 없는 오브젝트와 충돌했습니다!");
                Destroy(gameObject); 
            }
        }
        else if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject); // 바닥이나 벽에 부딪히면 총알을 파괴합니다.
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }

    private void FindClosestMonsterAndSetInitialDirection()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject monster in monsters)
        {
            float distanceToMonster = Vector3.Distance(currentPosition, monster.transform.position);
            if (distanceToMonster < closestDistance)
            {
                closestDistance = distanceToMonster;
                closestTarget = monster.transform;
            }
        }
        target = closestTarget;

        if (target != null)
        {
            // 타겟이 있다면 초기 방향을 설정합니다.
            Vector3 targetPosition = new Vector3(target.position.x, initialY, target.position.z);
            Vector3 initialDirection = (targetPosition - transform.position).normalized;
            rb.rotation = Quaternion.LookRotation(initialDirection);
            rb.velocity = initialDirection * homingSpeed; // 초기 속도 설정
            hasTarget = true;
        }
        else
        {
            isHomingEnabled = false;
            Debug.Log("추적할 몬스터를 찾지 못했습니다.");
        }
    }

    public void EnableHoming()
    {
        isHomingEnabled = true;
        FindClosestMonsterAndSetInitialDirection(); // 호밍 활성화 시 타겟을 찾고 초기 방향 설정
    }

    public void SetDamage(float damage)
    {
        damageAmount = damage;
    }

}
