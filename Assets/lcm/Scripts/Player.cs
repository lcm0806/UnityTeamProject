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
    [SerializeField] private List<Item> passiveItems = new List<Item>();
    [SerializeField] private List<Item> activeItems = new List<Item>();
    [Range(10, 30)]
    [SerializeField] private float bulletSpeed;
    public float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value;}
    private int maxhealth;
    public int MaxHealth { get { return maxhealth; } set { maxhealth = value; } }
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
    [SerializeField] private float attackRate = 0.5f;
    private float nextAttackTime = 0f;

    private bool wDown;
    private bool jDown;

    private bool isSide;
    private bool isDodge;
    private bool isDamage = false;

    

    private GameObject nearObject;
    private Invincible invincibleScript;


    Rigidbody rigid;
    MeshRenderer[] meshs; 

    Vector3 moveVec;
    Vector3 sideVec;
    Vector3 dodgeVec;

    private static Player instance = null;

    public static Player Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<Player>();

                if(instance != null)
                {
                    Debug.LogError("Player ������Ʈ�� ���� �����ϴ�.");
                }
            }

            return instance;
        }
    }

    Animator anim;
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;



        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        attack = GetComponent<Attack>();
        meshs = GetComponentsInChildren<MeshRenderer>();
        invincibleScript = GetComponent<Invincible>();
    }

    private void Start()
    {
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
        if (invincibleScript != null && !invincibleScript.isInvincible)
        {
            CulHealth -= damageAmount;
            Debug.Log($"플레이어 피격! 받은 데미지: {damageAmount}, 남은 체력: {CulHealth}");
            invincibleScript.StartInvincible();
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
        if(other.tag == "EnemyBullet")
        {
            MonsterBullet enemyBullet = other.GetComponent<MonsterBullet>();
            CulHealth -= enemyBullet.damage;
            StartCoroutine(OnDamage());
        }
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
        if(newItem.itemType == itemType.Passive)
        {
            newItem.attack = this.attack;
            newItem.player = this;
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

            }
        }
    }


    private void Die()
    {

    }

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
            Debug.Log("??? ?琯????? ???????? ???????.");
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
