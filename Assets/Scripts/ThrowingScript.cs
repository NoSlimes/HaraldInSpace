using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingScript : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject origin;
    [SerializeField] private float launchVelocity = 700f;
    [SerializeField] private float maxAccuracyOff = 0.1f;
    [SerializeField] private float minAccuracyOff = -0.1f;

    void Update()
    { 
        if (Input .GetButtonDown("Fire1"))
        {
            GameObject ball = Instantiate(projectile, origin.transform.position, origin.transform.rotation);
            ball.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, launchVelocity, 0));
             }

         }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
        
    }

