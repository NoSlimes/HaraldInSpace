using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float movementSpeed = 1000;
    public float jumpStrength = 100000;
    [SerializeField] private Animator animator;

    private Rigidbody playerBody;
   

    bool isGrounded; 
    
    void Start()
    {

        animator = GetComponentInChildren<Animator>();
        playerBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
         


        if (Input.GetKey(KeyCode.W))
        {

            playerBody.AddRelativeForce(Vector3.forward * movementSpeed * Time.deltaTime);
            

        }
        else if (Input.GetKey(KeyCode.S))
        {
            playerBody.AddRelativeForce(Vector3.forward * -movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {

            playerBody.AddRelativeForce(Vector3.left * movementSpeed * Time.deltaTime);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            playerBody.AddRelativeForce(Vector3.left * -movementSpeed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.Space))
        {
            playerBody.AddRelativeForce(Vector3.up * jumpStrength * Time.deltaTime); 
        }
        
        if(playerBody.velocity != Vector3.zero)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }

    }
    


}