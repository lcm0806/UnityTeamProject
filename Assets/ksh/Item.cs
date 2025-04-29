using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Item
{

    public string itemName;
    public GameObject itemPrefab;
    public enum Type { Damage, Coin, Heart }
    public Type type;
    public int value;

}
