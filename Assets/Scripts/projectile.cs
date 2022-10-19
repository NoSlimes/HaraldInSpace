using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "Player" || collision.transform.tag != "projectile")
            Destroy(this.gameObject);
    }
}
