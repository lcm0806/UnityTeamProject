using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item; // ?????? ??????
    
    private void DetectionItem()
    {
        if (CompareTag("Item"))
        {
            switch (item.itemName)
            {
                case "SadOnion": 
                    item = new SadOnion();
                    break;
                case "TheInnerEye": 
                    item = new TheInnerEye();
                    break;
                case "Pentagram":
                    item = new Pentagram();
                    break;
                case "GrowthHormones":
                    item = new GrowthHormones();
                    break;
                case "MagicMushroom":
                    item = new MagicMushroom();
                    break;
                case "SpoonBender":
                    item = new SpoonBender();
                    break;
                case "BlueCap":
                    item = new BlueCap();
                    break;
                case "CricketsState":
                    item = new CricketsState();
                    break;
                case "TornPhoto":
                    item = new TornPhoto();
                    break;
                case "Polyphemus":
                    item = new Polyphemus();
                    break;
                case "BookOfBelial":
                    item = new BookOfBelial();
                    break;
                case "YumHeart":
                    item = new YumHeart();
                    break;
                case "BookOfShadow":
                    item = new BookOfShadow();
                    break;
                case "ShoopDaWhoop":
                    item = new ShoopDaWhoop();
                    break;
                case "TheNail":
                    item = new TheNail();
                    break;
                case "MrBoom":
                    item = new MrBoom();
                    break;
                case "TammysBlessing":
                    item = new TammysBlessing();
                    break;
                case "Cross":
                    item = new Cross();
                    break;
                case "AnarchistCookBook":
                    item = new AnarchistCookBook();
                    break;
                case "TheHourglass":
                    item = new TheHourglass();
                    break;
                case "Potion":
                    item = new Potion();
                    break;
                case "GoldenKey":
                    item = new GoldenKey();
                    break;
                case "Bomb":
                    item = new Bomb();
                    break;
                default:
                    item = null;
                    break;
            }
        }
    }

}
