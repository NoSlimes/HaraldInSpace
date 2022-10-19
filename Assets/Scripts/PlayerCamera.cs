using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    public float mouseSensitivity = 100f; // skapar ett nummer som blir mouseSensitivity, g�r att �ndra p� tackvare public. 

    public Transform playerBody; // kallar p� spelarens vektorer och n�mner den playerBody

    float xRotation = 0f;  // skapar ett nummer med namnet xRotation. 
    // Start is called before the first frame update
    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false; 

    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; // kallar p� mouseX inputen och g�ngar det med mouseSensitivity och Time.deltatime
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; // samma som mouseX fast med Y axis. 


        xRotation -= mouseY; // xRotationen �r lika med -mouseY, Subtraherar resultatet genom att anv�nda -= 
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clampar xRotationen s� att kameran inte g�r full exorcist mode (vrider sig 360 grader �verallt) 


        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Roterar kameran genom Y-axeln 
        playerBody.Rotate(Vector3.up * mouseX); // Roterar spelarens kropp 

    }
}
