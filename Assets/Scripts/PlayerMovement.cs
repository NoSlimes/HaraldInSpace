using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 7.5f;
    [SerializeField] private float jumpStrength = 10f;
    public float speedMultiplier = 1f;

    [Header("Camera")]
    [SerializeField] private float SprinteFOVLerpTime = 3f;
    public float CameraSensitivity = 3f;
    private float xRotation;

    private Rigidbody playerBody;
    private Camera playerCamera;
    private Animator animator;
    private CapsuleCollider capsuleCollider;

    private float speed = 0;
    private PlayerStats playerStats;

    [Header("Ground Checking")]
    [SerializeField] private LayerMask groundMask;

    void Start()
    {
        speed = walkSpeed;
        animator = GetComponentInChildren<Animator>();
        playerBody = GetComponent<Rigidbody>();
        playerStats = GetComponent<PlayerStats>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        CursorLock.LockCursor();
    }

    void Update()
    {
        if(!isLocalPlayer) { return; }
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MovePlayer();
        MovePlayerCamera();

        if (playerBody.velocity.x != 0f || playerBody.velocity.z != 0f)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }

    private void MovePlayer()
    {
        float moveSpeed = speed * speedMultiplier;
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * moveSpeed;
        playerBody.velocity = new Vector3(MoveVector.x, playerBody.velocity.y, MoveVector.z);

        if (Input.GetButtonDown("Jump"))
        {
            playerBody.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
        }

        if(Input.GetButton("Sprint"))
        {
            speed = sprintSpeed;
            float timeElapsed = 0f;
            while (timeElapsed < SprinteFOVLerpTime)
            {
                playerCamera.fieldOfView = Mathf.Lerp(60f, 90f, timeElapsed / SprinteFOVLerpTime);
                timeElapsed += Time.deltaTime;
            }
            playerCamera.fieldOfView = 90;
        }
        else
        {
            speed = walkSpeed;
            float timeElapsed = 0f;
            while (timeElapsed < SprinteFOVLerpTime)
            {
                playerCamera.fieldOfView = Mathf.Lerp(90f, 60f, timeElapsed / SprinteFOVLerpTime);
                timeElapsed += Time.deltaTime;
            }
            playerCamera.fieldOfView = 60f;
        }
    }

    private void MovePlayerCamera()
    {
        xRotation -= PlayerMouseInput.y * CameraSensitivity;
        xRotation = Mathf.Clamp(xRotation, -75f, 75f);

        transform.Rotate(0f, PlayerMouseInput.x * CameraSensitivity, 0f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private bool isGrounded()
    {
        float radius = capsuleCollider.radius * 0.9f;
        Vector3 pos = transform.position + Vector3.up * (radius * 0.9f);
        return Physics.CheckSphere(pos, radius, groundMask);
    }

    public void AssignCamera()
    {
        playerCamera = GetComponentInChildren<Camera>();
    }


}