using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class SteamLobby : MonoBehaviour
{

    [SerializeField] private GameObject buttons = null;

    protected Callback<LobbyCreated_t> lobbyCreated;

    private const string HostAddressKey = "HostAddress";

    private HIS_NetworkManager networkManager;

    void Start()
    {
        networkManager = GetComponent<HIS_NetworkManager>();

        if (!SteamManager.Initialized) { return; }

        lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
    }

    public void HostLobby()
    {
        buttons.SetActive(false);

        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, networkManager.maxConnections);
    }

    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        if (callback.m_eResult != EResult.k_EResultOK)
        {
            buttons.SetActive(true);
            return;
        }

        networkManager.StartHost();

        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey, SteamUser.GetSteamID().ToString());
    }

}
