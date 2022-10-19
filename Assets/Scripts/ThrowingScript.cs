using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingScript : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject origin;
    [SerializeField] private float launchVelocity = 200f;
    [SerializeField] private float badAccuracy = 100f;

    void Update()
    { 
        if (Input .GetButtonDown("Fire1"))
        {
            GameObject ball = Instantiate(projectile, origin.transform.position, origin.transform.rotation);
            ball.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(Random.Range(-badAccuracy, badAccuracy), 1, launchVelocity));
             }

         }
    // Start is called before the first frame update
    void Start()
    {
     
        
    }

    // Update is called once per frame
   

}

