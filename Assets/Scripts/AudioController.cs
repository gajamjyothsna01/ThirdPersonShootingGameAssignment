using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
     AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
      audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void RunPlayer()
    {
        audioSource.Play();

        Debug.Log("Player Running Sound");
    }
}
