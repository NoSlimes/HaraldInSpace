using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FollowerController : NetworkBehaviour
{
    public Transform followTarget;

    [Command]
    public void CMDSetFollowTarget(NetworkConnectionToClient conn)
    {
        followTarget = conn.identity.transform;
    }
}
