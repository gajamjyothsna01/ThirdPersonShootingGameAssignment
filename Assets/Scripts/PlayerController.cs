using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    public float playerSpeed;
    Animator animator;
    public float playerRotateSpeed;
    int medical = 100;
    int maxMedical = 100;
    public int ammo = 100;
    int maxAmmo = 100;
  


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal") * playerSpeed;
        float inputZ = Input.GetAxis("Vertical") * playerSpeed;

        Vector3 movement = new Vector3(inputX, 0f, inputZ);

       

        animator.SetFloat("Speed", movement.magnitude);
        /*
        if(movement.magnitude > 0f)

        {
            Quaternion tempDirection = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, tempDirection, Time.deltaTime * playerRotateSpeed);

        }*/

        transform.Rotate(Vector3.up, inputX * playerRotateSpeed * Time.deltaTime);
        if (inputZ != 0)
        {
            //characterController.SimpleMove(transform.forward * Time.deltaTime * inputZ);
            characterController.Move(transform.forward * Time.deltaTime * inputZ);
        }



    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Medical" && medical < maxMedical)
        {
            Debug.Log("Collected Medical");
            other.gameObject.SetActive(false);
            medical = Mathf.Clamp(medical + 10, 0, maxMedical);

        }
        if (other.gameObject.tag == "Ammo" && ammo < maxAmmo)
        {
            Debug.Log("Collected Ammo");
            Debug.Log("Current Ammo" +ammo);
            other.gameObject.SetActive(false);
            medical = Mathf.Clamp(ammo + 10, 0, maxAmmo);

        }
    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Medical" && medical < maxMedical)
        {
            Debug.Log("Collected Medical");
            collision.gameObject.SetActive(false);
            medical = Mathf.Clamp(medical + 10, 0, maxMedical);
            
        }
        if (collision.gameObject.tag == "Ammo" && ammo < maxAmmo)
        {
            Debug.Log("Collected Ammo");
            collision.gameObject.SetActive(false);
            medical = Mathf.Clamp(ammo + 10, 0, maxAmmo);

        }
    }*/
}
