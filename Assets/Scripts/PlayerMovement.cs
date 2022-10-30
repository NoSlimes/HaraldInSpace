using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{ 
    [SerializeField] private float sprintSpeed = 2000;
    [SerializeField] private float jumpStrength = 10000;
    [SerializeField] private float walkSpeed = 1000;

    [SerializeField] private float LerpTimeFOV = 3f;

    private Rigidbody playerBody;
    private Camera Camera;
    private Animator animator;

    private float speed = 0;
    private PlayerStats playerStats;

   [SerializeField] bool isGrounded; 
    
    void Start()
    {
        speed = walkSpeed;
        animator = GetComponentInChildren<Animator>();
        playerBody = GetComponent<Rigidbody>();
        playerStats = GetComponent<PlayerStats>();
    }

    public void AssignCamera()
    {
        Camera = GetComponentInChildren<Camera>();
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void HandleMovement()
    {
        if (isLocalPlayer)
        {
            speed = speed * playerStats.SpeedMultiplier;
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            playerBody.AddRelativeForce(Vector3.right * (speed * x) * Time.deltaTime);
            playerBody.AddRelativeForce(Vector3.forward * (speed * z) * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                playerBody.AddForce(Vector3.up * jumpStrength * Time.deltaTime);
                isGrounded = false;
            }

            if (Input.GetButton("Sprint"))
            {
                speed = sprintSpeed;
                float timeElapsed = 0f;
                while(timeElapsed < LerpTimeFOV)
                {
                    Camera.fieldOfView = Mathf.Lerp(60f, 90f, timeElapsed / LerpTimeFOV);
                    timeElapsed += Time.deltaTime;
                }
                Camera.fieldOfView = 90;

            }
            else
            {
                speed = walkSpeed;
                float timeElapsed = 0f;
                while (timeElapsed < LerpTimeFOV)
                {
                    Camera.fieldOfView = Mathf.Lerp(90f, 60f, timeElapsed / LerpTimeFOV);
                    timeElapsed += Time.deltaTime;
                }
               Camera.fieldOfView = 60f;
            }

        }
    }


    void Update()
    {
        HandleMovement();

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