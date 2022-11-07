using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;   

public class HostManager : MonoBehaviour
{
    public static HostManager instance;

    [SerializeField] private Transport steamTransport, telepathyTransport;

    private void Start()
    {
        instance = this;
    }

    public void SetSteamTransport()
    {
        HIS_NetworkManager.instance.setTransport(steamTransport);
    }

    public void SetTelepathyTransport()
    {
        HIS_NetworkManager.instance.setTransport(telepathyTransport);
    }

}
