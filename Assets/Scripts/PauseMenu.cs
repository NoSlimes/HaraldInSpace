using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuHUD : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu, SettingsMenu;

    private void Start()
    {
        PauseMenu.SetActive(true);
        SettingsMenu.SetActive(false);
    }

    public void SettingsButton()
    {
        PauseMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    public void DisconnectButton()
    {

    }

    public void BackButton()
    {
        if (PauseMenu.activeInHierarchy)
        {
            PauseMenu.SetActive(false);

            return;
        }
        else if (SettingsMenu.activeInHierarchy)
        {
            SettingsMenu.SetActive(false);
            PauseMenu.SetActive(false);

            return;
        }

        
    }
}
