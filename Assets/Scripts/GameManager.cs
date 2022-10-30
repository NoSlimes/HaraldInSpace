using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<GameObject> players;

    private void Start()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);

       players = new List<GameObject>();
    }



}
