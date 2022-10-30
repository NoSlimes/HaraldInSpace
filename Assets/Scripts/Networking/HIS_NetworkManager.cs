using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HIS_NetworkManager : NetworkManager
{
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);

        NetworkPlayer player = conn.identity.GetComponent<NetworkPlayer>();

        player.SetDisplayName($"Player {numPlayers}");

        GameManager.instance.players.Add(player.gameObject);
    }
}
