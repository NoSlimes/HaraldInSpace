using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkDoor : NetworkBehaviour
{
    private Animator animator;
    [SerializeField] private bool doorOpenState = false;
    [SerializeField] private bool doorLockedState = false;

    public DoorType doorType;

    [SerializeField] private int TimedDoorSeconds = 2;

    private LocalAudioManager audioManager;

    private void Start()
    {
        audioManager = GetComponentInChildren<LocalAudioManager>();
        animator = GetComponent<Animator>();
    }

    [Command(requiresAuthority = false)]
    public void CmdOpenDoor()
    {
        if(doorOpenState) { return; }
        switch (doorType){
            case DoorType.Door:
                RpcOpenDoor();
                break;
            case DoorType.TimedDoor:
                RpcTimedDoor();
                break;
            default:
                break;
        }

        doorOpenState = true;
    }

    [Command(requiresAuthority = false)]
    public void CmdCloseDoor()
    {
        if (!doorOpenState) { return; }
        RpcCloseDoor();
        doorOpenState = false;
    }

    [Command]
    private void CmdChangeLockState(bool lockDoor)
    {
        doorLockedState = lockDoor;
        RpcChangeLockState(lockDoor);
    }

    [ClientRpc]
    private void RpcOpenDoor()
    {
        if (!doorLockedState)
        {
            animator.SetTrigger("openDoor");
            audioManager.Play("keypadSFX");
            audioManager.Play("openDoorSfx");
            doorOpenState = true;
        }
        else
        {
            audioManager.Play("keypadLockedDoorSfx");
            return;
        }

    }

    [ClientRpc]
    private void RpcCloseDoor()
    {
        if (!doorLockedState)
        {
            animator.SetTrigger("closeDoor");
            audioManager.Play("keypadSFX");
            audioManager.Play("closeDoorSfx");
            doorOpenState = false;
        }
        else
        {
            audioManager.Play("keypadLockedDoorSfx");
            return;
        }

    }

    [ClientRpc]
    private void RpcTimedDoor()
    {
        StartCoroutine(TimedDoorCoroutine(TimedDoorSeconds));
    }

    [ClientRpc]
    private void RpcChangeLockState(bool lockDoor)
    {
        doorLockedState = lockDoor;
    }

    [Client]
    public void ToggleDoor()
    {
        switch (doorType)
        {
            case DoorType.Door:
                if (doorOpenState)
                {
                    CmdCloseDoor();
                }
                else
                {
                    CmdOpenDoor();
                }

                break;
            case DoorType.TimedDoor:
                break;
            default:
                break;
        }
    }

    public void ToggleDoorLocked(bool lockDoor)
    {
        CmdChangeLockState(lockDoor);
    }

    private IEnumerator TimedDoorCoroutine(int Timer)
    {
        RpcOpenDoor();
        yield return new WaitForSeconds(Timer);
        RpcCloseDoor();
    }

    public enum DoorType
    {
        Door,
        TimedDoor
    }

    [ClientRpc]
    private void RpcLogDoorState()
    {
        Debug.Log($"Door open: {doorOpenState}");
    }

}
