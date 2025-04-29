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

    private void Awake()
    {

        
      //  Rigidbody rb = item.GetComponent<Rigidbody>();
        
      //  if(rb != null)
        {
      //      rb.AddForce(Vector3.up * ThrowForce, ForceMode.Impulse);
        }
        


        player = GameObject.FindGameObjectWithTag("Player"); //플레이어 태그 설정
        animator = GetComponent<Animator>();
        IsPlayerEnter = false; //초가엔 False
        IsClosed = true;
        
        items.Add(new SadOnion());
     // items.Add(new TheInnerEye()); 눈모양
     // items.Add(new Pentagram()); 별
        items.Add(new GrowthHormones());
        items.Add(new MagicMushroom());
     // items.Add(new SpoonBender()); 수저
        items.Add(new BlueCap());
     // items.Add(new CricketsState()); 조각상
     // items.Add(new TornPhoto()); 찢어진사진
     // items.Add(new Polyphemus()); 눈알 큰
        items.Add(new BookOfBelial());
        items.Add(new YumHeart());
        items.Add(new BookOfShadow());
     // items.Add(new ShoopDaWhoop()); 귀여운인형
     // items.Add(new TheNail()); 못
     // items.Add(new MrBoom()); 폭탄
     // items.Add(new TammysBlessing()); 축복
        items.Add(new Cross());
        items.Add(new AnarchistCookBook());
     // items.Add(new TheHourglass()); 모래시계
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
        int randomIndex = UnityEngine.Random.Range(0, items.Count);
        Item selectedItem = items[randomIndex];
        currentItem = Instantiate(selectedItem.itemPrefab, ItemSpawn.position, Quaternion.identity);
        Rigidbody rigid = currentItem.GetComponent<Rigidbody>();
        rigid.AddForce(Vector3.forward * 6f, ForceMode.Impulse);
       Debug.Log($"{selectedItem.itemName} 아이템이 생성되었습니다.");
        Invoke("DestoryChest", 1f);
    }

    private void DestoryChest()
    {
        Destroy(gameObject);
        IsClosed = true;
    }
    
}
