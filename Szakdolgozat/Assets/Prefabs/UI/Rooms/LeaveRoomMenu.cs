using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveRoomMenu : MonoBehaviour
{
    private RoomsCanvases roomsCanvases;
    public void OnClick_LeaveRoom()
    {
        PhotonNetwork.LeaveLobby();
        PhotonNetwork.LeaveRoom(false);
        roomsCanvases.CurrentRoomCanvas.Hide();
    }

    public void FirstInitialize(RoomsCanvases canvases)
    {
        roomsCanvases = canvases;

    }
}
