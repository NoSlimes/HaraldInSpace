using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using Steamworks;
using Unity.VisualScripting;

public class NetworkPlayer : NetworkBehaviour
{
    [SerializeField] private TMP_Text displayNameText;

    [SyncVar(hook = nameof(HandleDisplayNameUpdated))][SerializeField] private string displayName = "No";

    private void Start()
    {
        SetMyName();
    }

    #region Server

    [Server]
    public void SetDisplayName(string newDisplayName)
    {
            displayName = newDisplayName;
    }

    [Command]
    private void CmdSetDisplayName(string newDisplayName)
    {
        if (!hasAuthority) { return; }
        if (displayName.Length > 20 || displayName.Length <= 3) { return; }

        RpcLogNewName(newDisplayName);

        SetDisplayName(newDisplayName);
    }

    #endregion

    #region Client

    private void HandleDisplayNameUpdated(string oldText, string newText)
    {
        displayNameText.text = displayName;
    }


    public void SetMyName()
    {
        if(!hasAuthority) { return; }
        if (SteamManager.Initialized)
        {
            CmdSetDisplayName(SteamFriends.GetPersonaName());
        }
        else
        {
            CmdSetDisplayName("No Name");
        }
    }

    [ClientRpc]
    private void RpcLogNewName(string newDisplayName)
    {
        Debug.Log(newDisplayName);
    }

    #endregion

}
