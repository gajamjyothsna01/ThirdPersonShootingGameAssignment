using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    public float playerSpeed;
    Animator animator;
    public float playerRotateSpeed;
    public int health;
  


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
}
