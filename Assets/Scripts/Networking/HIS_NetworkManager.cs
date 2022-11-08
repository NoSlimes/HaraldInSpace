using Mirror;

public class HIS_NetworkManager : NetworkManager
{
    public static HIS_NetworkManager instance;
    public override void Start()
    {
        base.Start();

        instance = this;
    }
    public void setTransport(Transport newTransport)
    {
        transport = newTransport;
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);

        NetworkPlayer player = conn.identity.GetComponent<NetworkPlayer>();

        //player.SetDisplayName($"Player {numPlayers}");

        GameManager.players.Add(player.gameObject);
    }

    public override void ServerChangeScene(string newSceneName)
    {
        base.ServerChangeScene(newSceneName);

    }
}
