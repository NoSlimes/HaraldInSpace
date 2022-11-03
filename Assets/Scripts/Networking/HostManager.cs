using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;   

public class HostManager : MonoBehaviour
{
    public static HostManager instance;

    [SerializeField] private Transport steamTransport, telepathyTransport;

    private HIS_NetworkManager networkManager;


    private void Start()
    {
        instance = this;
        networkManager = GetComponent<HIS_NetworkManager>();
    }

    public void setSteamTransport()
    {
       // networkManager.
    }

    public void setTelepathyTransport()
    {

    }

}
