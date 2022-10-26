using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float movementSpeed = 1000;
    public float jumpStrength = 10000;
    [SerializeField] private Animator animator;
    public float sprintSpeed = 2000;
    public float defaultmovementSpeed = 1000; 

    private Rigidbody playerBody;
    private Camera Camera; 
   

    bool isGrounded; 
    
    void Start()
    {

        animator = GetComponentInChildren<Animator>();
        playerBody = GetComponent<Rigidbody>();
        Camera = GetComponentInChildren<Camera>(); 
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
        Debug.Log("isGrounded"); 
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

        if(Input.GetKey(KeyCode.Space) && isGrounded)
        {
            playerBody.AddRelativeForce(Vector3.up * jumpStrength * Time.deltaTime);
            isGrounded = false;

            Debug.Log("Jumping"); 
        }
        
        if(playerBody.velocity != Vector3.zero)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }

        //sprint// 
        if(Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = sprintSpeed;
            Camera.fieldOfView = 120f; 

        }
        else
        {
            movementSpeed = defaultmovementSpeed;
            Camera.fieldOfView = 60f;
        }

             
    }
    


}