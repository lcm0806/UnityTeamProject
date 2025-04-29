using System.Collections;
using UnityEngine;

public class Boss : Monster
{

    public GameObject missile;
    public Transform missilePortA;
    public Transform missilePortB;

    Vector3 lookVec;
    Vector3 tauntVec;
    bool isLook;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<MeshRenderer>().material;
        anime = GetComponentInChildren<Animator>();

        StartCoroutine(Think());
    }


    void Update()
    {
        if (isLook)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            lookVec = new Vector3(h, 0, v) * 5f;
            transform.LookAt(target.position + lookVec);
        }
    }

    IEnumerator Think()
    {
        yield return new WaitForSeconds(0.1f);

        int ranAction = Random.Range(0, 5);
        switch (ranAction)
        {
            case 0:
            case 1:
                //미사일발사
                StartCoroutine(MissileShot());
                break;
            case 2:
            case 3:
                //돌굴러가유~
                StartCoroutine(RockShot());
                break;
            case 4:
                //쓰끈하게 함 뛰어볼까?
                StartCoroutine(Taunt());
                break;

        }
    }

    IEnumerator MissileShot()
    {
        anime.SetTrigger("doShot");
        yield return new WaitForSeconds(2.5f);

        StartCoroutine(Think());
    }

    IEnumerator RockShot()
    {
        anime.SetTrigger("doBigShot");
        yield return new WaitForSeconds(3f);

        StartCoroutine(Think());
    }

    IEnumerator Taunt()
    {
        anime.SetTrigger("doTaunt");
        yield return new WaitForSeconds(3f);

        StartCoroutine(Think());
    }


}
