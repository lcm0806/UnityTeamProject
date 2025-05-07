using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static OpeningUIManager;

public class Player : MonoBehaviour
{
    private float hAxis;
    private float vAxis;

    [SerializeField] private float speed;
    [SerializeField] private GameObject bombPrefab; // Inspector에서 할당할 폭탄 프리팹
    [SerializeField] private float bombOffset = 1f;
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


    [SerializeField] private float defaultBulletScale = 1f;
    [SerializeField] private GameObject pickupTextUIPrefab; // 인스펙터에서 연결할 프리팹

    public void ShowPickupText(string message)
    {
        Vector3 spawnPos = transform.position + Vector3.up * 2f;
        GameObject obj = Instantiate(pickupTextUIPrefab, spawnPos, Quaternion.identity);

        PickupTextWorldUI textUI = obj.GetComponent<PickupTextWorldUI>();
        if (textUI != null)
        {
            textUI.SetText(message);
        }
    }
    public float DefaultBulletScale
    {
        get => defaultBulletScale;
        set => defaultBulletScale = value;
    }

    private float currentBulletScale;
    public float CurrentBulletScale
    {
        get => currentBulletScale;
        private set => currentBulletScale = value;
    }


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

    public int hasGranade = 0;

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
                    Debug.LogError("플레이어 인스턴스가 씬에 존재하지 않습니다.");
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

        //rayser = GetComponent<Rayser>();

        if (GameBootFlags.isNewGame)
        {
            ResetPlayer();
            FindObjectOfType<PassiveEquipmentUI>()?.ResetUI();
            FindObjectOfType<ActiveEquipmentUI>()?.ResetUI();
            GameBootFlags.isNewGame = false; // 한 번만 초기화되도록
        }
    }

    // Update is called once per frame
    private void Update()
    {
        GetInput();
        Move();
        Turn();
        Dodge();
        Attack();

        if (Input.GetKeyDown(KeyCode.R))
        {
            UseGranade();
        }

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
        if (Input.GetKey(KeyCode.Q))
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
                //Die();
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
        //newItem.EnsureIconLoaded();

        if (newItem.itemType == itemType.Passive)
        {
            //newItem.attack = this.attack;
            //newItem.player = this;
            passiveItems.Add(newItem);
            ApplyPassiveEffects();
            //FindObjectOfType<PassiveEquipmentUI>()?.AddPassiveItem(newItem.itemIcon);
        }
        else if (newItem.itemType == itemType.Active)
        {
            activeItems.Add(newItem);
            //FindObjectOfType<ActiveEquipmentUI>()?.AddActiveItem(newItem.itemIcon);
        }

        else if(newItem.itemType == itemType.Normal && newItem.itemName == "폭탄")
        {
            hasGranade += 1;
        }
        ShowPickupText(newItem.itemName);
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

   //private void OnTriggerEnter(Collider other)
   //{
   //    ItemHolder holder = other.GetComponent<ItemHolder>();
   //    if (holder != null && holder.itemInstance != null)
   //    {
   //        AcquireItem(holder.itemInstance);
   //        Destroy(other.gameObject);
   //    }
   //}


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


                    if (type == itemType.Active && Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        Debug.Log("액티브 아이템 사용: " + targetList[index].itemName);
                        targetList[index].UseItem();
                        // 예시: 사용 후 첫 번째 액티브 아이템 제거
                        activeItems.RemoveAt(index);

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
    }

    public void ResetPlayer()
    {
        this.CulHealth = this.MaxHealth;

        // 아이템 초기화
        passiveItems.Clear();
        activeItems.Clear();

        // 체력 초기화 (UI 쪽과 연동된다면 HealthManager에서도 추가 작업 필요)
        FindObjectOfType<HealthManager>()?.ResetHealth(MaxHealth);

        // 위치 초기화
        transform.position = new Vector3(0f, 5f, 0f);
        transform.rotation = Quaternion.identity;
    }

    private void UseGranade()
    {
        if (hasGranade > 0)
        {
            Debug.Log("폭탄사용");
            hasGranade--;
            Debug.Log($"남은 폭탄개수: {hasGranade}");

            Bomb tempBomb = new Bomb();
            tempBomb.player = this;
            tempBomb.UseItem();
        }
        else
        {
            Debug.Log("사용 가능한 폭탄이 없습니다.");
        }
    }

    public void SpawnBomb(GameObject bombPrefab)
    {
        if (bombPrefab != null)
        {
            Vector3 spawnPosition = transform.position + transform.forward * bombOffset;
            GameObject bombInstance = Instantiate(bombPrefab, spawnPosition, Quaternion.identity);

            // 생성된 폭탄 오브젝트에 폭발 효과 컴포넌트 추가
            BombLogic bombEffect = bombInstance.AddComponent<BombLogic>();

        }
        else
        {
            Debug.LogError("Bomb 프리팹이 null입니다.");
        }
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
