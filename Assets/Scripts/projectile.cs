using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Projectile : NetworkBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "Player" && collision.transform.tag != "Projectile")
            
            CmdDestroyProjectile();
    }

    [Command(requiresAuthority = false)]
    private void CmdDestroyProjectile()
    {
        NetworkServer.Destroy(gameObject);
    }


}
