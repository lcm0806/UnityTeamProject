using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;
    public Transform target;
    public float moveSpeed;
    public bool isChase;


    Rigidbody rigid;
    BoxCollider boxCollider;
    Material mat;
    Animator anime;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<MeshRenderer>().material;
        anime = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "공격수단") //todo
        {
            //공격수단(태그지정) 공격수단이름 = other.GetComponent<공격수단>();
            //curHealth -= 공격수단공격력;
            Vector3 reactVec = transform.position - other.transform.position;

            //Destroy(other.gameObject); //(총알 같은 투사체일경우 맞았을때 삭제)

            StartCoroutine(OnDamage(reactVec));
        }
    }

    private void Update()
    {
        if (isChase)
        {
            anime.SetBool("isWalk", true);
            Trace();
        }
        else
        {
            anime.SetBool("isWalk", false);
        }
    }


    IEnumerator OnDamage(Vector3 reactVec)
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
            isChase = false;
            anime.SetTrigger("doDie");

            reactVec = reactVec.normalized;
            reactVec += Vector3.up;
            rigid.AddForce(reactVec * 5, ForceMode.Impulse);
            Destroy(gameObject, 4);
        }
    }

    private void Trace()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        transform.LookAt(target.transform.position);

    }

}
