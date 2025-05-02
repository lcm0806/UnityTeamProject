using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincible : MonoBehaviour
{
    public GameObject targetObject;
    public bool isInvincible = false;
    private float invincibleDuration = 0.5f;
    public float InvincibleDuration { get { return invincibleDuration; } set { invincibleDuration = value; } }
    public void StartInvincible()
    {
        isInvincible = true;
        // 무적 시간 후 무적 상태 해제
        Invoke("EndInvincible", InvincibleDuration);
    }

    public void EndInvincible()
    {
        isInvincible = false;
        if(invincibleDuration > 0.5f)
        {
            invincibleDuration = 0.5f;
        }
    }

    public void Activate_Sheild()
    {
        InvincibleDuration = 10.0f;
        targetObject.SetActive(true);
        StartInvincible(); 
        targetObject.SetActive(false);

    }
}
