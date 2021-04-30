using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class DeathScreenmanager : MonoBehaviour
{
    public PhotonView pv;
    public Button retryButton;

    private void Start()
    {
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            retryButton.interactable = true;
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        PhotonNetwork.LoadLevel(0);
        if (PhotonNetwork.InLobby) PhotonNetwork.LeaveLobby();
        if(PhotonNetwork.InRoom) PhotonNetwork.LeaveRoom();

    }

    public void Retry()
    {
        if (PhotonNetwork.LocalPlayer.IsMasterClient) { 

            PhotonNetwork.AutomaticallySyncScene = true;
           pv.RPC("RetryRPC",RpcTarget.All);
           
        }
    }

    [PunRPC]
    public void RetryRPC()
    {
        PhotonNetwork.LoadLevel("Arena");
    }
}
