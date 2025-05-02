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

    [SerializeField] private List<Item> acquiredItems = new List<Item>();
    [SerializeField] private List<Item> passiveItems = new List<Item>();
    [SerializeField] private List<Item> activeItems = new List<Item>();

    [SerializeField] private int maxhealth;
    private int culhealth;
    public int CulHealth { get { return culhealth; } set { culhealth = value; } }
    [SerializeField] private float damage = 10f;
    public float Damage { get { return damage; } set { damage = value; } }

    [SerializeField] private float soulhealth;
    public float SoulHealth
    {
        get => soulhealth;
        set => soulhealth = value;
    }


    [SerializeField] Attack attack;
    [SerializeField] private float attackRate = 0.5f; //���ݼӵ�
    private float nextAttackTime = 0f;

    private bool wDown;
    private bool jDown;

    private bool isSide;
    private bool isDodge;
    private bool isDamage = false;

    private bool isInvincible = false;
    [SerializeField] private float invincibleDuration = 0.5f;

    private GameObject nearObject;

    Rigidbody rigid;
    MeshRenderer[] meshs; 

    Vector3 moveVec;
    Vector3 sideVec;
    Vector3 dodgeVec;

    Animator anim;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        meshs = GetComponentsInChildren<MeshRenderer>();
    }

    void Start()
    {
        ApplyPassiveEffects();
    }

    private void Update()
    {
        GetInput();
        Move();
        Turn();
        Dodge();
        Attack();
        UseActiveItemInput();
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
        else if (Input.GetKey(KeyCode.Q))
        {
            if (Time.time >= nextAttackTime)
            {
                attack.Fire(damage);
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (!isInvincible)
        {
            CulHealth -= damageAmount;
            Debug.Log($"플레이어 피격! 받은 데미지: {damageAmount}, 남은 체력: {health}");


            StartInvincible();


            if (CulHealth <= 0)
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
         if (collision.gameObject.GetComponent<ItemPickup>() != null)
         {
            ItemPickup pickup = collision.gameObject.GetComponent<ItemPickup>();
            AcquireItem(pickup.item);
            Destroy(collision.gameObject);
            ApplyPassiveEffects();
            Debug.Log("아이템 획득: " + pickup.item.itemName);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }

    IEnumerator OnDamage()
    {
        isDamage = true;
        foreach(MeshRenderer mesh in meshs)
        {
            mesh.material.color= Color.red;
        }
        yield return new WaitForSeconds(1f);

        isDamage = false;
        foreach (MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.white;
        }
    }

    public void AcquireItem(Item newItem)
    {
        acquiredItems.Add(newItem);
        Debug.Log("아이템 획득: " + newItem.itemName + " (" + newItem.itemType + ")");

        if (newItem.itemType == itemType.Passive)
        {
            passiveItems.Add(newItem);
            ApplyPassiveEffects();
        }
        else if (newItem.itemType == itemType.Active)
        {
            activeItems.Add(newItem);
        }

        UpdateHasItemsUI();
    }

    private void ApplyPassiveEffects()
    {
        foreach (Item item in passiveItems)
        {
            if (item.itemType == itemType.Passive)
            {
                Debug.Log("패시브 아이템 효과 적용: " + item.itemName);
            }
        }
    }

    private void UseActiveItemInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && activeItems.Count > 0)
        {
            UseItem(0, itemType.Active);
        }
    }

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
        else
        {
            Debug.Log("해당 인덱스의 아이템은 사용할 수 없습니다.");
        }
    }

    public void UseItem(int index, itemType type)
    {
        List<Item> targetList = (type == itemType.Active) ? activeItems : passiveItems;

        if (index >= 0 && index < targetList.Count)
        {
            if (targetList[index].itemType == type)
            {
                Debug.Log($"[{type}] 아이템 사용: {targetList[index].itemName}");
                targetList[index].UseItem();
            }
            else
            {
                Debug.Log($"선택된 아이템은 {type} 타입이 아닙니다.");
            }
        }
    }

    private void UpdateHasItemsUI()
    {
        Debug.Log("보유한 전체 아이템 목록:");
        for (int i = 0; i < acquiredItems.Count; i++)
        {
            Debug.Log($"{i + 1}: {acquiredItems[i].itemName} ({acquiredItems[i].itemType})");
        }
    }

    private void UpdatePassiveItemsUI()
    {
        Debug.Log("보유한 패시브 아이템 목록:");
        for (int i = 0; i < passiveItems.Count; i++)
        {
            Debug.Log($"{i + 1}: {passiveItems[i].itemName} ({passiveItems[i].itemType})");
        }
    }
}
