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
    //float damageRate = 1.0f;
    float timer;
    public Transform firePoint;
    [SerializeField]
    public AudioSource audioSource;
    //public Transform effectPosition;
    //public GameObject shootEffectPrefab;
   // public ParticleSystem particleSystem;
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
       int ammoH =  GameObject.Find("Player").GetComponent<PlayerController>().ammo--;
        Debug.Log("Ammo " + ammoH);
        audioSource.Play();
        Debug.DrawRay(firePoint.position, transform.forward * 100, Color.red, 2f);
        Ray ray = new Ray(firePoint.position, transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100f))
        {
           // particleSystem.Play();
           // Instantiate(shootEffectPrefab,effectPosition.position, Quaternion.identity);    
            Debug.Log("Fire Shooting");
            GameObject hitMonster = hitInfo.collider.gameObject;
            if(hitMonster.tag == "Monster")
            {
                Debug.Log("Enemy Found");
                hitMonster.GetComponent<MonsterController>().DeadEnemy();
                Debug.Log("Going to Dead State");
            }

        }
    }
}
