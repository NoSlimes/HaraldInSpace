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

    public IEnumerator HostTelepathy()
    {
        buttons.SetActive(false);
        HostManager.instance.SetTelepathyTransport();
        yield return new WaitForEndOfFrame();
        HIS_NetworkManager.instance.StartHost();
    }

    public IEnumerator HostSteamLobby()
    {
        HostManager.instance.SetSteamTransport();
        yield return new WaitForEndOfFrame();
        steamLobby.HostLobby();
    }
}
