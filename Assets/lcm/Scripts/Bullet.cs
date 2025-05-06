using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float damageAmount;




    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.tag == "Monster")
        {
            Monster monster = collision.gameObject.GetComponent<Monster>();
            if (monster != null)
            { 
                Destroy(gameObject);     
            }
            else
            {
                Debug.LogError("'Monster' 태그가 붙었지만 Monster 스크립트가 없는 오브젝트와 충돌했습니다!");
                Destroy(gameObject); 
            }
        }
        else if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject); // 바닥이나 벽에 부딪히면 총알을 파괴합니다.
        }
    }

    public void SetDamage(float damage)
    {
        damageAmount = damage;
    }

}
