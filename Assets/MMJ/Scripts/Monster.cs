using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public enum Type { A, B, C, D };
    public Type monsterType;
    public float maxHealth;
    public float curHealth;
    public Transform target;
    public float moveSpeed;
    public BoxCollider meleeArea;
    public GameObject bullet;
    public bool isChase;
    public bool isAttack;
    public bool isDead;

    public Rigidbody rigid;
    public BoxCollider boxCollider;
    public MeshRenderer[] meshs;
    
    public Animator anime;
    
    
    [SerializeField] private List<GameObject> itemprefabs;
    private bool DropItem = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        meshs = GetComponentsInChildren<MeshRenderer>();
        anime = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            Bullet bullet = other.GetComponent<Bullet>();
            curHealth -= bullet.damageAmount;
            Destroy(other.gameObject);
            Vector3 reactVec = transform.position - other.transform.position;

            Debug.Log(curHealth);
            

            StartCoroutine(OnDamage(reactVec));
        }
    }


    private void Update()
    {
        if (isDead)
        { 
            StopAllCoroutines();
            if (!DropItem)
            {
                RandomItem();
                DropItem = true;
            }
            Debug.Log("���ְ;�");
            return;
        }

        if (isChase)
        {
            anime.SetBool("isWalk", true);
            Trace();
        }
        //  else
        //  {
        //      anime.SetBool("isWalk", false);
        //  }
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            Targeting();
        }
        else 
        {
            StopAllCoroutines();
        }
    }


    void Targeting()
    {
        float targetRadius = 0;
        float targetRange = 0;

        if (!isDead)
        {
            switch (monsterType)
            {
                case Type.A:
                    targetRadius = 1f;
                    targetRange = 1f;
                    break;
                case Type.B:
                    targetRadius = 1f;
                    targetRange = 12f;
                    break;
                case Type.C:
                    targetRadius = 0.5f;
                    targetRange = 25f;
                    break;
            }

            RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));

            if (rayHits.Length > 0 && !isAttack)
            {
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        isChase = false;
        isAttack = true;
        anime.SetBool("isAttack", true);

        if (!isDead)
        {
            switch (monsterType)
            {
                case Type.A:

                    yield return new WaitForSeconds(0.5f);
                    meleeArea.enabled = true;

                    yield return new WaitForSeconds(1f);
                    meleeArea.enabled = false;

                    yield return new WaitForSeconds(1f);

                    break;
                case Type.B:

                    yield return new WaitForSeconds(0.1f);
                    rigid.AddForce(transform.forward * 30, ForceMode.Impulse);
                    meleeArea.enabled = true;

                    yield return new WaitForSeconds(0.5f);
                    rigid.velocity = Vector3.zero;
                    meleeArea.enabled = false;

                    yield return new WaitForSeconds(2f);

                    break;
                case Type.C:
                    yield return new WaitForSeconds(0.5f);
                    GameObject instantBullet = Instantiate(bullet, transform.position, transform.rotation);
                    Rigidbody rigidBullet = instantBullet.GetComponent<Rigidbody>();
                    rigidBullet.velocity = transform.forward * 20;

                    yield return new WaitForSeconds(2f);

                    break;
            }


            isChase = true;
            isAttack = false;
            anime.SetBool("isAttack", false);
        }
    }

    IEnumerator OnDamage(Vector3 reactVec)
    {
        foreach (MeshRenderer mesh in meshs)
        { 
            mesh.material.color = Color.red;
        }
        yield return new WaitForSeconds(0.1f);

        if (curHealth > 0)
        {
            foreach (MeshRenderer mesh in meshs)
            {
                mesh.material.color = Color.white;
            }
        }
        else
        {
            foreach (MeshRenderer mesh in meshs)
            {
                mesh.material.color = Color.gray;
            }
            gameObject.layer = 21;
            isDead = true;
            isChase = false;
            anime.SetTrigger("doDie");


            Destroy(gameObject, 3);
        }
    }

    private void Trace()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        transform.LookAt(target.transform.position);
    }

    private void RandomItem()
    {
        int randomIndex = UnityEngine.Random.Range(0, itemprefabs.Count);
        GameObject selectedPrefab = itemprefabs[randomIndex];
        
        GameObject item = Instantiate(selectedPrefab, transform.position, Quaternion.identity);
        Rigidbody rigid = item.GetComponent<Rigidbody>();
        rigid.AddForce(Vector3.up * 6f, ForceMode.Impulse);
        rigid.AddForce(Vector3.forward * 6f, ForceMode.Impulse);
    }

}
