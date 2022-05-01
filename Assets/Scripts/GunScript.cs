using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField]
    [Range(0.5f, 1.5f)]
    float fireRate = 1.0f;
    [Range(1f, 10f)]
    float damageRate = 1.0f;
    float timer;
    public Transform firePoint;
    [SerializeField]
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;
        if (timer >= fireRate)
        {
            if (Input.GetButton("Fire1"))
            {
                timer = 0f;
                ToFireGun();

            }
        }
    }

    private void ToFireGun()
    {
        Debug.DrawRay(firePoint.position, transform.forward * 100, Color.red, 2f);
        Ray ray = new Ray(firePoint.position, transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100f))
        {
            /*GameObject hitZombiee = hitInfo.collider.gameObject;
            Debug.Log("Got Hot");
            if(hitZombiee.tag == "Zombiee")
            {
                Debug.Log("Found ZOmbiee");
                hitZombiee.GetComponent<EnemyScript>().Death();
            }*/
            // var health = hitInfo.collider.GetComponent<EnemyScript>();
            /* if (health != null)
             {
                 health.DamageMethod(10);

             }*/
            Debug.Log("Fire Shooting");

        }
    }
}
