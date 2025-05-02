using System.Collections;
using UnityEngine;

public class Boss : Monster
{

    public GameObject missile;
    public Transform missilePortA;
    public Transform missilePortB;
    [SerializeField] BossBullet_ObjectPool bulletPool;

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

        yield return new WaitForSeconds(0.2f);
        PooledObject instanceA = bulletPool.GetPool(missilePortA.position, missilePortA.rotation);
        BossMonsterMissile bossMonsterMissileA = instanceA.GetComponent<BossMonsterMissile>();
        bossMonsterMissileA.target = target;

        yield return new WaitForSeconds(0.3f);
        PooledObject instanceB = bulletPool.GetPool(missilePortB.position, missilePortB.rotation);
        BossMonsterMissile bossMonsterMissileB = instanceB.GetComponent<BossMonsterMissile>();
        bossMonsterMissileB.target = target;

        yield return new WaitForSeconds(2f);
        StartCoroutine(Think());
    }

    IEnumerator RockShot()
    {
        anime.SetTrigger("doBigShot");
        Instantiate(bullet, transform.position, transform.rotation);

        yield return new WaitForSeconds(3f);

        isLook = true;
        StartCoroutine(Think());
    }

    IEnumerator Taunt()
    {
        isLook = false;
        anime.SetTrigger("doTaunt");

        tauntVec = target.position;

        Vector3 startPos = transform.position;
        Vector3 endPos = tauntVec;

        float jumpTime = 1.0f;
        float elapsed = 0f;
        boxCollider.enabled = false;
        while (elapsed < jumpTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / jumpTime;


            float height = 5f;
            float parabola = 4 * height * (t - t * t);

            transform.position = Vector3.Lerp(startPos, endPos, t) + Vector3.up * parabola;

            yield return null;
        }

        transform.position = endPos;

        meleeArea.enabled = true;
        boxCollider.enabled = true;
        yield return new WaitForSeconds(0.5f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(1f);
        isLook = true;

        StartCoroutine(Think());
    }


}
