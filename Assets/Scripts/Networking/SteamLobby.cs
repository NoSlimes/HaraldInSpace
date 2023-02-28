using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using Mirror;

public class SteamLobby : NetworkBehaviour
{

    [SerializeField] private GameObject buttons = null;

    protected Callback<LobbyCreated_t> lobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> gameLobbyJoinRequested;
    protected Callback<LobbyEnter_t> lobbyEnter;

    private const string HostAddressKey = "HostAddress";

    void Start()
    {
        if (!SteamManager.Initialized) { return; }

        lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        gameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(OnGameLobbyJoinRequested);
        lobbyEnter = Callback<LobbyEnter_t>.Create(OnLobbyEnter);
    }

    public void HostLobby()
    {
        buttons.SetActive(false);

        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, HIS_NetworkManager.instance.maxConnections);
    }

    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        if (callback.m_eResult != EResult.k_EResultOK)
        {
            buttons.SetActive(true);
            return;
        }
        HostManager.instance.SetSteamTransport();
        HIS_NetworkManager.instance.StartHost();

        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey, SteamUser.GetSteamID().ToString());
    }


    private void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t callback)
    {
        HostManager.instance.SetSteamTransport();
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }

    private void OnLobbyEnter(LobbyEnter_t callback)
    {
        if (NetworkServer.active) { return; }

        string hostAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey);
        HIS_NetworkManager.instance.networkAddress = hostAddress;
        HIS_NetworkManager.instance.StartClient();
    }
}
/*
using System;
using System.Linq;
using Mirror;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;

public class SteamLobby : MonoBehaviour
{
    MultiplayerManager network;
    string hostAdressKey = "HostAdress";

    private static CSteamID lobbyID { get; set; }

    private void Start()
    {
        network = GetComponent<MultiplayerManager>();

        Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        Callback<GameLobbyJoinRequested_t>.Create(OnGameLobbyJoinRequested);
        Callback<LobbyEnter_t>.Create(OnLobbyEntered);

    }

    public void HostLobby()
    {
        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypePublic, network.maxConnections);
    }

    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        if (callback.m_eResult != EResult.k_EResultOK)
        {
            Debug.LogError(callback.m_eResult);
            return;
        }

        network.StartHost();

        lobbyID = new CSteamID(callback.m_ulSteamIDLobby);

        SteamMatchmaking.SetLobbyData(
            lobbyID,
            hostAdressKey,
            SteamUser.GetSteamID().ToString());
    }

    private static void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t callback)
    {
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }

    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        if (NetworkServer.active) return;

        string hostAdress = SteamMatchmaking.GetLobbyData(
            new CSteamID(callback.m_ulSteamIDLobby),
            hostAdressKey);

        network.networkAddress = hostAdress;
        network.StartClient();
    }
}*/