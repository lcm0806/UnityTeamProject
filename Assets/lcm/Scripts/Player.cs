using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private float hAxis;
    private float vAxis;

    [SerializeField] private float speed;
    [SerializeField] private List<Item> passiveItems = new List<Item>();
    [SerializeField] private List<Item> activeItems = new List<Item>();
    [SerializeField] private int health;
    public int Health { get { return health; } set { health = value; } }
    [SerializeField] private int damage = 10;
    public int Damage { get { return damage; } set { damage = value; } }
    [SerializeField] Attack attack;

    private bool wDown;
    private bool jDown;

    private bool isSide;
    private bool isDodge;

    private bool isInvincible = false;
    [SerializeField] private float invincibleDuration = 0.5f;

    private GameObject nearObject;


    Rigidbody rigid;

    Vector3 moveVec;
    Vector3 sideVec;
    Vector3 dodgeVec;

    Animator anim;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        AcquireItem(new SadOnion());
        AcquireItem(new Pentagram());
        ApplyPassiveEffects();
    }

    // Update is called once per frame
    private void Update()
    {
        GetInput();
        Move();
        Turn();
        Dodge();
        Attack();
    }

    private void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
    }

    private void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (isDodge)
            moveVec = dodgeVec;

        if (isSide && moveVec == sideVec)
            moveVec = Vector3.zero;

        transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);
    }

    private void Turn()
    {
        transform.LookAt(transform.position + moveVec);
    }

    

    private void Dodge()
    {
        if (jDown && moveVec != Vector3.zero && !isDodge)
        {
            dodgeVec = moveVec;
            speed *= 2;
            anim.SetTrigger("doDodge");
            isDodge = true;

            Invoke("DodgeOut", 0.4f);
        }
    }

    private void DodgeOut()
    {
        speed *= 0.5f;
        isDodge = false;
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            attack.Fire(damage);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        // 무적 상태가 아닐 때만 데미지를 받음
        if (!isInvincible)
        {
            health -= damageAmount;
            Debug.Log($"플레이어 피격! 받은 데미지: {damageAmount}, 남은 체력: {health}");

            // 피격 애니메이션 재생 (선택 사항)
            //if (anim != null)
            //{
            //    anim.SetTrigger("doHit");
            //}

            // 피격 시 무적 상태 시작 (선택 사항)
            StartInvincible();

            // 체력이 0 이하로 떨어졌을 때 사망 처리
            if (health <= 0)
            {
                Die();
            }
        }
        else
        {
            Debug.Log("플레이어 무적 상태로 데미지를 받지 않음!");
        }
    }

    private void StartInvincible()
    {
        isInvincible = true;
        // 무적 시간 후 무적 상태 해제
        Invoke("EndInvincible", invincibleDuration);
        // 필요하다면 무적 상태 시 시각적 효과를 줄 수도 있습니다.
    }

    private void EndInvincible()
    {
        isInvincible = false;
        // 무적 상태 해제 시 시각적 효과를 제거할 수 있습니다.
    }

    private void OnCollisionEnter(Collision collision)
    {
        anim.SetBool("isJump", false);

         // 아이템 습득 처리 (충돌 감지)
         if (collision.gameObject.GetComponent<ItemPickup>() != null)
         {
            ItemPickup pickup = collision.gameObject.GetComponent<ItemPickup>();
            AcquireItem(pickup.item);
            Destroy(collision.gameObject); // 습득한 아이템 오브젝트 파괴
            ApplyPassiveEffects(); // 습득 후 패시브 효과 다시 적용
            Debug.Log("아이템 획득: " + pickup.item.itemName);
         }



    }

    public void AcquireItem(Item newItem)
    {
        if(newItem.itemType == itemType.Passive)
        {
            passiveItems.Add(newItem);
            ApplyPassiveEffects();
        }
        else if(newItem.itemType == itemType.Active)
        {
            activeItems.Add(newItem);

        }
        Debug.Log("아이템 획득 :" + newItem.itemName + " (" + newItem.itemType + ")");
    }

    private void ApplyPassiveEffects()
    {
        // 현재는 간단하게 로그만 출력, 실제 효과 적용 로직 구현 필요
        foreach (Item item in passiveItems)
        {
            if (item.itemType == itemType.Passive)
            {
                Debug.Log("패시브 아이템 효과 적용: " + item.itemName);
                item.UseItem(); // 각 아이템의 UseItem() 호출 (실제 효과 구현)
            }
        }
    }


    // 아이템 사용 함수 (인덱스 기반)
    public void UseItem(int index, itemType type)
    {
        List<Item> targetList = (type == itemType.Active) ? activeItems : passiveItems;

        if (index >= 0 && index < targetList.Count)
        {
            if (targetList[index].itemType == type)
            {
                Debug.Log("액티브 아이템 사용: " + targetList[index].itemName);
                targetList[index].UseItem(); // 액티브 아이템의 UseItem() 호출 (실제 효과 구현)
                // 사용 후 아이템 제거 또는 쿨타임 처리 등 추가 로직 필요
                if (type == itemType.Active)
                {
                    // 예시: 사용 후 첫 번째 액티브 아이템 제거
                    // activeItems.RemoveAt(index);
                    // UpdateActiveItemUI();
                }
            }
            else
            {
                Debug.Log("해당 슬롯은 " + type + " 아이템이 아닙니다.");
            }
        }
        else
        {
            Debug.Log("해당 인덱스의 아이템이 없습니다.");
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (!isDodge)
        {
            health -= damageAmount;
            Debug.Log("플레이어 피격! 남은 체력: " + health);

            if(health <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        Debug.Log("플레이어 사망!");
    }

    // UI 업데이트 (임시)
    private void UpdatePassiveItemsUI()
    {
        // 실제 UI 시스템에 맞게 구현해야 함
        Debug.Log("현재 보유 아이템:");
        for (int i = 0; i < passiveItems.Count; i++)
        {
            Debug.Log($"{i + 1}: {passiveItems[i].itemName} ({passiveItems[i].itemType})");
        }
    }

}
