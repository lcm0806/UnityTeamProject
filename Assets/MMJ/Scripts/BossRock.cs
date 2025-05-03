using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRock : MonsterBullet
{
    Rigidbody rigid;
    float angularPower = 2;
    float scaleValue = 0.1f;
    bool isShoot;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        StartCoroutine(GainPowerTimer());
        StartCoroutine(GainPower());
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GainPowerTimer()
    { 
        yield return new WaitForSeconds(2.2f);
        isShoot = true;
    }

    IEnumerator GainPower()
    {
        while (!isShoot)
        {
            angularPower += 0.02f;
            scaleValue += 0.65f;
            transform.localScale = Vector3.one * scaleValue * Time.deltaTime;
            rigid.AddTorque(transform.right * angularPower, ForceMode.Acceleration);
            yield return null;
        }
    }
}
