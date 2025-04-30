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

         // ?????? ???? ??? (?浹 ????)
         if (collision.gameObject.GetComponent<ItemPickup>() != null)
         {
            ItemPickup pickup = collision.gameObject.GetComponent<ItemPickup>();
            AcquireItem(pickup.item);
            Destroy(collision.gameObject); // ?????? ?????? ??????? ?ı?
            ApplyPassiveEffects(); // ???? ?? ?н?? ??? ??? ????
            Debug.Log("?????? ???: " + pickup.item.itemName);
         }



    }

    public void AcquireItem(Item newItem)
    {
        acquiredItems.Add(newItem);
        // UI ??????? (hasitems ?迭 ??? - ???? UI ?????? ?°? ???? ???)
        UpdateHasItemsUI();
    }

    private void ApplyPassiveEffects()
    {
        // ????? ??????? ?α?? ???, ???? ??? ???? ???? ???? ???
        foreach (Item item in acquiredItems)
        {
            if (item.itemType == itemType.Passive)
            {
                Debug.Log("?н?? ?????? ??? ????: " + item.itemName);
                item.UseItem(); // ?? ???????? UseItem() ??? (???? ??? ????)
            }
        }
    }

    private void UseActiveItemInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && acquiredItems.Count > 0)
        {
            UseItem(0); // ? ??° ?????? ???
        }
        
    }

    // ?????? ??? ??? (?ε??? ???)
    public void UseItem(int index)
    {
        if (index >= 0 && index < acquiredItems.Count)
        {
            if (acquiredItems[index].itemType == itemType.Active)
            {
                Debug.Log("????? ?????? ???: " + acquiredItems[index].itemName);
                acquiredItems[index].UseItem(); // ????? ???????? UseItem() ??? (???? ??? ????)
                // ??? ?? ?????? ???? ??? ????? ??? ?? ??? ???? ???
            }
            else
            {
                Debug.Log("??? ?????? ????? ???????? ?????.");
            }
        }
        else
        {
            Debug.Log("??? ?ε????? ???????? ???????.");
        }
    }

    // UI ??????? (???)
    private void UpdateHasItemsUI()
    {
        // ???? UI ?????? ?°? ??????? ??
        Debug.Log("???? ???? ??????:");
        for (int i = 0; i < acquiredItems.Count; i++)
        {
            Debug.Log($"{i + 1}: {acquiredItems[i].itemName} ({acquiredItems[i].itemType})");
        }
    }

}
