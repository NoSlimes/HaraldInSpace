using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatScript : MonoBehaviour
{
    [SerializeField]private PlayerStats playerStats;

    private void Eat()
    {
        playerStats.HP = 0;//n�got
       
 
   
    }
     void Update()
    {
        if (Input.GetButtonDown("Fire2")) 
        {
            playerStats.HP++;

            Debug.Log("Health");


        }

    }

}
