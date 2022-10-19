using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float movementSpeed = 1000;
    public float jumpStrength = 100000;


    public Rigidbody playerBody;
   

    bool isGrounded; 
    
    void Start()
    {



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
        

    }
    


}