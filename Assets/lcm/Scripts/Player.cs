using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private float hAxis;
    private float vAxis;

    [SerializeField] private float speed;
public float Speed
{
    get => speed;
    set => speed = value;
}

[SerializeField] private float speed;

[SerializeField] private List<Item> acquiredItems = new List<Item>();
[SerializeField] private List<Item> passiveItems = new List<Item>();
[SerializeField] private List<Item> activeItems = new List<Item>();

[SerializeField] private int health;
public int Health
{
    get => health;
    set => health = value;
}

[SerializeField] private float soulhealth;
public float SoulHealth
{
    get => soulhealth;
    set => soulhealth = value;
}

[SerializeField] private int damage = 10;
public int Damage
{
    get => damage;
    set => damage = value;
}
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
        // if (anim != null) anim.SetTrigger("doHit");

        // 무적 상태 진입 (선택 사항)
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
    Invoke("EndInvincible", invincibleDuration);
}

private void EndInvincible()
{
    isInvincible = false;
}
    private void OnCollisionEnter(Collision collision)
    {
        anim.SetBool("isJump", false);


         if (collision.gameObject.GetComponent<ItemPickup>() != null)
         {
            ItemPickup pickup = collision.gameObject.GetComponent<ItemPickup>();
            AcquireItem(pickup.item);
Destroy(collision.gameObject); // 몬스터 오브젝트 제거
ApplyPassiveEffects();         // 아이템 습득 시 효과 적용
Debug.Log("아이템 획득: " + pickup.item.itemName);
         }



    }

    public void AcquireItem(Item newItem)
    {
public void AcquireItem(Item newItem)
{
    acquiredItems.Add(newItem); // 전체 아이템 목록 유지
    Debug.Log("아이템 획득: " + newItem.itemName + " (" + newItem.itemType + ")");

    // 타입별 리스트 분류
    if (newItem.itemType == itemType.Passive)
    {
        passiveItems.Add(newItem);
        ApplyPassiveEffects();
    }
    else if (newItem.itemType == itemType.Active)
    {
        activeItems.Add(newItem);
        // 필요하면 UI 업데이트 로직 추가
    }

    UpdateHasItemsUI(); // UI 업데이트
}

private void ApplyPassiveEffects()
{
    // 획득한 패시브 아이템 효과 적용
    foreach (Item item in passiveItems)
    {
        if (item.itemType == itemType.Passive)
        {
            Debug.Log("패시브 아이템 효과 적용: " + item.itemName);
            item.UseItem();
        }
    }
}

// 숫자키 입력 방식 (1번 키 등)
private void UseActiveItemInput()
{
    if (Input.GetKeyDown(KeyCode.Alpha1) && activeItems.Count > 0)
    {
        UseItem(0, itemType.Active); // 첫 번째 액티브 아이템 사용
    }
}

// 아이템 사용 (기존 방식 유지)
public void UseItem(int index)
{
    if (index >= 0 && index < acquiredItems.Count)
    {
        if (acquiredItems[index].itemType == itemType.Active)
        {
            Debug.Log("액티브 아이템 사용: " + acquiredItems[index].itemName);
            acquiredItems[index].UseItem();
        }
        else
        {
            Debug.Log("해당 아이템은 액티브 타입이 아닙니다.");
        }
    }
}

// 오버로드: 타입별 아이템 사용
public void UseItem(int index, itemType type)
{
    List<Item> targetList = (type == itemType.Active) ? activeItems : passiveItems;

    if (index >= 0 && index < targetList.Count)
    {
        if (targetList[index].itemType == type)
        {
            Debug.Log($"[{type}] 아이템 사용: {targetList[index].itemName}");
            targetList[index].UseItem();

            // 예시: 사용 후 제거 로직
            if (type == itemType.Active)
            {
                // activeItems.RemoveAt(index);
                // UpdateActiveItemUI();
            }
        }
        else
        {
            Debug.Log($"선택된 아이템은 {type} 타입이 아닙니다.");
        }
    }
}

            }
        }
        else
        {
        else
        {
            Debug.Log("해당 인덱스의 아이템은 사용할 수 없습니다.");
        }
    }

    private void Die()
    {
        Debug.Log("플레이어 사망!");
    }

    // 전체 아이템 UI 디버그 출력 (예시)
    private void UpdateHasItemsUI()
    {
        Debug.Log("보유한 전체 아이템 목록:");
        for (int i = 0; i < acquiredItems.Count; i++)
        {
            Debug.Log($"{i + 1}: {acquiredItems[i].itemName} ({acquiredItems[i].itemType})");
        }
    }

    // 패시브 아이템만 출력 (예시)
    private void UpdatePassiveItemsUI()
    {
        Debug.Log("보유한 패시브 아이템 목록:");
        for (int i = 0; i < passiveItems.Count; i++)
        {
            Debug.Log($"{i + 1}: {passiveItems[i].itemName} ({passiveItems[i].itemType})");
        }
    }

        }
    }

}
