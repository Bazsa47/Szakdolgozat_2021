using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MainMenuButtons : MonoBehaviour
{

    public GameObject buttonsPanel;
    public GameObject howToPlayPanel;

    public void ExitGame()
    {
        Application.Quit();
    }

    public void FindMatch()
    {
        PhotonNetwork.LoadLevel(1);   
    }

    public void HowToPlay()
    {
        buttonsPanel.gameObject.SetActive(false);
        howToPlayPanel.gameObject.SetActive(true);
    }

    public void BackToMainMenu()
    {
        howToPlayPanel.gameObject.SetActive(false);
        buttonsPanel.gameObject.SetActive(true);
    }
}
