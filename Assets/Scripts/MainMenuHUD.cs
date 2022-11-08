using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TheraBytes.BetterUi;
using Steamworks;

public class MainMenuHUD : MonoBehaviour
{
    [SerializeField]private GameObject MainMenu, PlayMenu, HostMenu, JoinMenu, SettingsMenu;
    [SerializeField] private BetterTextMeshProInputField addressField;

    private void Start()
    {
        MainMenu.SetActive(true);
        PlayMenu.SetActive(false);
        HostMenu.SetActive(false);
        JoinMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        FindObjectOfType<AudioManager>().Play("MainMenu");
    }

    public void SettingsButton()
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    public void PlayButton()
    {
        MainMenu.SetActive(false);
        PlayMenu.SetActive(true);
    }

    public void HostButton()
    {
        PlayMenu.SetActive(false);
        HostMenu.SetActive(true);
    }

    public void JoinButton()
    {
        PlayMenu.SetActive(false);
        JoinMenu.SetActive(true);
    }

    public void JoinTelepathyButton()
    {
        HIS_NetworkManager.instance.StartClient();
    }

    public void JoinSteamButton()
    {
        SteamFriends.ActivateGameOverlay("friends");
    }

    public void IPOnValueChanged()
    {
        HIS_NetworkManager.instance.networkAddress = addressField.text;
    }

    public void HostTelepathyButton()
    {
        StartCoroutine(StartHost.instance.HostTelepathy());
    }

    public void HostSteamButton()
    {
        StartCoroutine(StartHost.instance.HostSteamLobby());
    }

    public void BackButton()
    {
        if (PlayMenu.activeInHierarchy)
        {
            PlayMenu.SetActive(false);
            MainMenu.SetActive(true);

            return;
        }
        else if (SettingsMenu.activeInHierarchy)
        {
            SettingsMenu.SetActive(false);
            MainMenu.SetActive(true);

            return;
        }
        else if (JoinMenu.activeInHierarchy)
        {
            JoinMenu.SetActive(false);
            PlayMenu.SetActive(true);

            return;
        }
        else if (HostMenu.activeInHierarchy)
        {
            HostMenu.SetActive(false);
            PlayMenu.SetActive(true);

            return;
        }

    }

    public void ExitButton()
    {
        Application.Quit();

        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #endif

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackButton();
        }
    }
}
