using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CameraInstantiate : NetworkBehaviour
{
    [SerializeField] private GameObject CameraMountPoint;
    [SerializeField] private GameObject CameraPrefab;

    private void Start()
    {

        if (isLocalPlayer)
        {
            GameObject cameraObject = Instantiate(CameraPrefab, CameraMountPoint.transform.position, CameraMountPoint.transform.rotation);

            cameraObject.transform.parent = transform;
            cameraObject.GetComponent<PlayerCamera>().playerBody = transform;

            GetComponentInParent<PlayerMovement>().AssignCamera();
            GetComponent<ThrowBerries>().origin = cameraObject.transform.GetChild(0).gameObject;
            GetComponent<InteractableController>().rayOrigin = cameraObject.transform;

        }
    }
}
