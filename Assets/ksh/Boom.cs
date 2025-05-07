using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public GameObject meshObj;
    public GameObject effectObj;
    public Rigidbody rb;
    public Player player;
    private bool Expolde = false;
    [SerializeField] private float damage;
    
    
    void Start()
    {
        StartCoroutine(Explosion());
    }

    private IEnumerator Explosion()
    {
        Expolde = true;
        yield return new WaitForSeconds(3f);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        meshObj.SetActive(false);
        

        RaycastHit[] rayHits =
            Physics.SphereCastAll(transform.position, 15, Vector3.up, 0f, LayerMask.GetMask("Monster"));
        foreach (RaycastHit hitObj in rayHits)
        {
            Monster monster = hitObj.transform.GetComponent<Monster>();
            if (monster != null)
            {
                monster.TakeDamage(damage); 
            }
        }
        
        yield return new WaitForSeconds(1f);
        if (effectObj != null)
        {
            effectObj.SetActive(true);
        }
        Destroy(gameObject);
    }
    
}
