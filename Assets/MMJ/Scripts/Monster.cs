using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;

    Rigidbody rigid;
    BoxCollider BoxCollider;
    Material mat;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        BoxCollider = GetComponent<BoxCollider>();
        mat = GetComponent<MeshRenderer>().material;
    }

    private void OnTriggerEnter(Collider other) //충돌판정
    {
        //  if (other.tag == "태그이름")
        //  {
        //      Weapon weapon = other.GetComponent<Weapon>();
        //      curHealth -= weapon.damage;
        StartCoroutine(OnDamage());
        //  }
        //  else if (other.tag == "태그이름1")
        //  {
        //      Weapon1 weapon1 = other.GetComponent<Weapon1>();
        //      curHealth -= weapon1.damage;
        StartCoroutine(OnDamage());
        //  }
    }

    IEnumerator OnDamage()
    {
        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        if (curHealth > 0)
        {
            mat.color = Color.white;
        }
        else 
        {
            mat.color = Color.gray;
            gameObject.layer = 21;
            Destroy(gameObject, 4);
        }
    }

}
