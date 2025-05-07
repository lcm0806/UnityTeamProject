using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class NewBehaviourScript : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private List<Item> items = new List<Item>();
    [SerializeField] private Transform ItemSpawn;
    private Animator animator;
    private bool IsPlayerEnter;
    private bool IsClosed;
    private GameObject currentItem;
    itemType chooseitemType;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player"); //플레이어 태그 설정
        animator = GetComponent<Animator>();
        IsPlayerEnter = false; //초가엔 False
        IsClosed = true;
        
  items.Add(new SadOnion()); //이펙트
  items.Add(new TheInnerEye());
  items.Add(new Pentagram()); //이펙트
  items.Add(new GrowthHormones()); //이펙트
  items.Add(new MagicMushroom()); //이펙트
  items.Add(new SpoonBender());
  items.Add(new BlueCap());//이펙트
  items.Add(new CricketsState()); //이펙트
  items.Add(new TornPhoto());
  items.Add(new Polyphemus()); //이펙트
  items.Add(new BookOfBelial()); //이펙트
  items.Add(new YumHeart());
  items.Add(new BookOfShadow());//이펙트
  items.Add(new ShoopDaWhoop());
  items.Add(new TheNail()); 
  items.Add(new MrBoom()); //이펙트
  items.Add(new TammysBlessing());
  items.Add(new Cross());
  items.Add(new AnarchistCookBook()); //이펙트
  items.Add(new TheHourglass());
    }

    private void Update()
    {
        if (IsPlayerEnter && Input.GetKeyDown(KeyCode.F)) //플레이어가 범위안에 있고 F키를 누르면
        {
            if (IsClosed)
            {
                IsClosed = false;
                animator.SetTrigger("Open");
                Invoke(nameof(OpenChest), 1f);
            }
            else
            {
                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("충돌");
        if (other.gameObject == player)
        {
            IsPlayerEnter = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            IsPlayerEnter = false;
        }
    }

    private void OpenChest()
    {
        Debug.Log("상자 열렸다!");

        int randomIndex = UnityEngine.Random.Range(0,items.Count);
        Item selectedItem = items[randomIndex];

       int TypeChoose = UnityEngine.Random.Range(0, 100);
       if (TypeChoose < 30)
       {
           chooseitemType = itemType.Active;
       }
       else
       {
           chooseitemType = itemType.Passive;
       }
       List<Item> CurrentItems = items.FindAll(item => item.itemType == chooseitemType);
        
       // int randomIndex = UnityEngine.Random.Range(0, CurrentItems.Count);
       // Item selectedItem = CurrentItems[randomIndex];


        currentItem = Instantiate(selectedItem.itemPrefab, ItemSpawn.position, selectedItem.itemPrefab.transform.rotation);


        // 아이템 정보 주입
        ItemHolder holder = currentItem.AddComponent<ItemHolder>();
        holder.Init(selectedItem);
        Rigidbody rigid = currentItem.GetComponent<Rigidbody>();
        rigid.AddForce(Vector3.up * 6f, ForceMode.Impulse);
        rigid.AddForce(Vector3.back * 6f, ForceMode.Impulse);
       //Debug.Log($"{TypeChoose}% 확률로 {selectedItem.itemName} 아이템이 생성되었습니다.");
        Invoke("DestoryChest", 1f);
    }

    private void DestoryChest()
    {
        Destroy(gameObject);
        IsClosed = true;
    }
    
}
