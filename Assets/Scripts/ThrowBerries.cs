using Mirror;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ThrowBerries : NetworkBehaviour
{
    [SerializeField] private GameObject projectile;
    [HideInInspector] public GameObject origin;
    [SerializeField] private float launchVelocity = 200f;
    [SerializeField] private float badAccuracy = 100f;

    private void Start()
    {

    }

    void Update()
    {

        Debug.Log("islocalplayer" + isLocalPlayer);
        if(!hasAuthority) { return; }

        if (Input.GetButtonDown("Fire1"))
        {
            CmdSpawnProjectile(origin.transform.position, origin.transform.rotation, badAccuracy);
        }
    }

    [Command]
    void CmdSpawnProjectile(Vector3 pos, quaternion rotation, float accurracyOffset)
    {
        GameObject berriesProjectile = Instantiate(projectile, pos, rotation);
        berriesProjectile.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(UnityEngine.Random.Range(-accurracyOffset, accurracyOffset), 1, launchVelocity));
        NetworkServer.Spawn(berriesProjectile);
    }

}

