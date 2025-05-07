using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item; // ?????? ??????
    public Player player;

    private void Awake()
    {
        Debug.Log("아이템인스턴스가 생성되었습니다.");
        DetectionItem();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("충돌감지");
            
            if (item != null)
            {
                collision.gameObject.GetComponent<Player>().AcquireItem(item);
                Destroy(gameObject);
                Debug.Log(item.itemName + "획득");
            }
            else
            {
                Debug.Log("null");
            }
        }
    }
    
    private void DetectionItem()
    {
        Debug.Log("아이템초기화시작" + gameObject.name);
        if (CompareTag("Item") && item == null)
        {
                switch (gameObject.name)
                {
                case "SadOnion(Clone)": 
                    item = new SadOnion();
                    break;
                case "TheInnerEye(Clone)": 
                    item = new TheInnerEye();
                    break;
                case "Pentagram(Clone)":
                    item = new Pentagram();
                    break;
                case "GrowthHormones(Clone)":
                    item = new GrowthHormones();
                    break;
                case "MagicMushroom(Clone)":
                    item = new MagicMushroom();
                    break;
                case "SpoonBender(Clone)":
                    item = new SpoonBender();
                    break;
                case "BlueCap(Clone)":
                    item = new BlueCap();
                    break;
                case "CricketsState(Clone)":
                    item = new CricketsState();
                    break;
                case "TornPhoto(Clone)":
                    item = new TornPhoto();
                    break;
                case "Polyphemus(Clone)":
                    item = new Polyphemus();
                    break;
                case "BookOfBelial(Clone)":
                    item = new BookOfBelial();
                    break;
                case "YumHeart(Clone)":
                    item = new YumHeart();
                    break;
                case "BookOfShadow(Clone)":
                    item = new BookOfShadow();
                    break;
                case "ShoopDaWhoop(Clone)":
                    item = new ShoopDaWhoop();
                    break;
                case "TheNail(Clone)":
                    item = new TheNail();
                    break;
                case "MrBoom(Clone)":
                    item = new MrBoom();
                    break;
                case "TammysBlessing(Clone)":
                    item = new TammysBlessing();
                    break;
                case "Cross(Clone)":
                    item = new Cross();
                    break;
                case "AnarchistCookBook(Clone)":
                    item = new AnarchistCookBook();
                    break;
                case "TheHourglass(Clone)":
                    item = new TheHourglass();
                    break;
                case "Potion(Clone)":
                    item = new Potion();
                    break;
                case "GoldenKey(Clone)":
                    item = new GoldenKey();
                    break;
                case "Bomb(Clone)":
                    item = new Bomb();
                    break;
                default:
                    item = null;
                    break;
                 }
                Debug.Log("아이템초기화완료");
            }
            
        }
    }


