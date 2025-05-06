using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    public LineRenderer laserEffect;
    public string ignoreTag = "Monster";
    public LayerMask layerMask;


    private void Update()
    {
        RaycastHit hit;
        laserEffect.SetPosition(0, transform.position);

       if(Physics.Raycast(transform.position, transform.forward, out hit, 5000,layerMask))
       {
            if (!hit.collider.CompareTag(ignoreTag))
            {
                laserEffect.SetPosition(1, hit.point);
            }
            else
            {
                RaycastHit nextHit;
                if (Physics.Raycast(hit.point + transform.forward * 0.1f, transform.forward, out nextHit, 5000 - hit.distance, layerMask))
                {
                    if (!nextHit.collider.CompareTag(ignoreTag))
                    {
                        laserEffect.SetPosition(1, nextHit.point);
                    }
                    else
                    {
                        laserEffect.SetPosition(1, transform.position + transform.forward * 5000);
                    }
                }
                else
                {
                    laserEffect.SetPosition(1, transform.position + transform.forward * 5000);
                }
            }
                
       }
        else
        {
            laserEffect.SetPosition(1, transform.position + transform.forward * 5000);
        }
    }
}
