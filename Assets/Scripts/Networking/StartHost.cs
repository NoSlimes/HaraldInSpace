using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartHost : MonoBehaviour
{
    private SteamLobby steamLobby;
    public static StartHost instance;
    [SerializeField] GameObject buttons = null;
    void Start()
    {
        steamLobby = GetComponent<SteamLobby>();
        instance = this;
    }

    public void HostTelepathy()
    {
        buttons.SetActive(false);
        HIS_NetworkManager.instance.StartHost();
    }

    public void HostSteamLobby()
    {
        steamLobby.HostLobby();
    }
}
