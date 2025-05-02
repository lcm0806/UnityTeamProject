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
    public bool isAttack;
    public bool isDead;


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
        if (other.tag == "���ݼ���") //todo
        {
            //���ݼ���(�±�����) ���ݼ����̸� = other.GetComponent<���ݼ���>();
            //curHealth -= ���ݼ��ܰ��ݷ�;
            Vector3 reactVec = transform.position - other.transform.position;

            //Destroy(other.gameObject); //(�Ѿ� ���� ����ü�ϰ�� �¾����� ����)

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
