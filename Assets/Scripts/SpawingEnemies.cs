using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawingEnemies : MonoBehaviour
{
    
    //public GameObject[] enemyPrefabs;
    public int number;
    public float spawnRadius;
    bool spawnOnStart = true;
    // Start is called before the first frame update
    void Start()
    {
        spawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        float distanceBetweenThem = Vector3.Distance(player.transform.position, spawnPosition);
        if (distanceBetweenThem < 2)
        {
           
        }*/
    }

    private void spawnEnemies()
    {
        /*
        spawnPosition = player.transform.position + Vector3.forward;
        Debug.Log("spawn position got");
        for (int i = 0; i < number; i++)
        {
           
            
            Instantiate(enemyPrefabs[0], spawnPosition, Quaternion.identity);
        }*/

        for (int i = 0; i < number; i++)
        {

            Vector3 randompoint = transform.position + UnityEngine.Random.insideUnitSphere * spawnRadius;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randompoint, out hit, 10f, NavMesh.AllAreas))
            {
                //int k = UnityEngine.Random.Range(0, enemyPrefabs.Length);
                // Instantiate(enemyPrefabs[0], randompoint, Quaternion.identity);
                // Instantiate(enemyPrefabs[0], randompoint, Quaternion.identity);
                GameObject temp = PoolScript.instance.GetObjectsFromPool("Monster");
                if (temp != null)
                {
                    temp.SetActive(true);
                    //temp.transform.position = new Vector3(UnityEngine.Random.Range(-11f, 15f), 0, UnityEngine.Random.Range(-12f, -11f));
                    temp.transform.position = randompoint;
                }

            }
            else
            {
                i--;
            }



        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (!spawnOnStart && gameObject.tag == "Player")
            spawnEnemies();
            
    }
}
