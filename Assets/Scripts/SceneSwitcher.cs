using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
public class SceneSwitcher : NetworkBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(2); 
    }
}
