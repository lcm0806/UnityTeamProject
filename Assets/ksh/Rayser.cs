using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Rayser : MonoBehaviour
{
    public GameObject LayserPrefab;
    public Transform muzzlePoint;
    private Vector3 hitpoint;

    public void Laser(float damage)
    {
        RaycastHit hit;
        float maxDistance = 10f;
        Ray ray = new Ray(muzzlePoint.position, muzzlePoint.forward);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("진입");
            hitpoint = hit.point;

            Monster monster = hit.collider.GetComponent<Monster>();
            if (monster != null)
            {
                //monster.TakeDamage(damage);
                Destroy(hit.collider.gameObject);
            }
        }
        else
        {
            hitpoint = muzzlePoint.position + muzzlePoint.forward * maxDistance;
        }

        Vector3 direction = (hitpoint - muzzlePoint.position).normalized;
        GameObject laser = Instantiate(LayserPrefab, muzzlePoint.position, Quaternion.LookRotation(direction));
        
        float length = Vector3.Distance(muzzlePoint.position, hitpoint);
        laser.transform.localScale = new Vector3(1, 1, length);
        
        Destroy(laser, 0.5f);
}
}
