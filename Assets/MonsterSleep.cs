using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSleep : MonoBehaviour
{
    float time = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time = time + Time.deltaTime;
        if(time > 10f)
        {
            this.gameObject.SetActive(false);
            time = 0f;
        }
    }
}
