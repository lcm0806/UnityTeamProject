using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private float hAxis;
    private float vAxis;

    [SerializeField] private float speed;
    [SerializeField] private List<Item> acquiredItems = new List<Item>();
    [SerializeField] private int health;
    [SerializeField] private int Damage;
    [SerializeField] Attack attack;

    private bool wDown;
    private bool jDown;

    private bool isSide;
    private bool isDodge;

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
            attack.Fire();
        }
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
        acquiredItems.Add(newItem);
        // UI 업데이트 (hasitems 배열 활용 - 실제 UI 로직에 맞게 수정 필요)
        UpdateHasItemsUI();
    }

    private void ApplyPassiveEffects()
    {
        // 현재는 간단하게 로그만 출력, 실제 효과 적용 로직 구현 필요
        foreach (Item item in acquiredItems)
        {
            if (item.itemType == itemType.Passive)
            {
                Debug.Log("패시브 아이템 효과 적용: " + item.itemName);
                item.UseItem(); // 각 아이템의 UseItem() 호출 (실제 효과 구현)
            }
        }
    }

    private void UseActiveItemInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && acquiredItems.Count > 0)
        {
            UseItem(0); // 첫 번째 아이템 사용
        }
        
    }

    // 아이템 사용 함수 (인덱스 기반)
    public void UseItem(int index)
    {
        if (index >= 0 && index < acquiredItems.Count)
        {
            if (acquiredItems[index].itemType == itemType.Active)
            {
                Debug.Log("액티브 아이템 사용: " + acquiredItems[index].itemName);
                acquiredItems[index].UseItem(); // 액티브 아이템의 UseItem() 호출 (실제 효과 구현)
                // 사용 후 아이템 제거 또는 쿨타임 처리 등 추가 로직 필요
            }
            else
            {
                Debug.Log("해당 슬롯은 액티브 아이템이 아닙니다.");
            }
        }
        else
        {
            Debug.Log("해당 인덱스의 아이템이 없습니다.");
        }
    }

    // UI 업데이트 (임시)
    private void UpdateHasItemsUI()
    {
        // 실제 UI 시스템에 맞게 구현해야 함
        Debug.Log("현재 보유 아이템:");
        for (int i = 0; i < acquiredItems.Count; i++)
        {
            Debug.Log($"{i + 1}: {acquiredItems[i].itemName} ({acquiredItems[i].itemType})");
        }
    }

}
