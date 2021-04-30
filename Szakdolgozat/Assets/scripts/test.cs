using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class test : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public GameObject loadingScreen;
    void Start()
    {
        if (PhotonNetwork.IsConnected)
            OnConnectedToMaster();
        if (!PhotonNetwork.IsConnected)
        {
            loadingScreen.SetActive(true);
            print("connecting to server");
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.NickName = MasterManager.GameSettings.NickName;
            PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }

    }

    public override void OnConnectedToMaster()
    {
        loadingScreen.SetActive(false);
        print("connected to master server");
        print(PhotonNetwork.LocalPlayer.NickName);
        if(!PhotonNetwork.InLobby) PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print(cause.ToString());
    }
}
