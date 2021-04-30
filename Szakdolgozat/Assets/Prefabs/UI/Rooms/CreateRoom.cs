using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class CreateRoom : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TextMeshProUGUI roomName;
    public TMP_InputField playerName;
    public TMP_InputField roomNameInput;
    public TextMeshProUGUI emptyUsername;
    public TextMeshProUGUI emptyRoomname;

    private RoomsCanvases roomsCanvases;
    public void FirstInitialize(RoomsCanvases canvases)
    {
        roomsCanvases = canvases;
    }
    public void OnClick_CreateRoom()
    {
        if (!PhotonNetwork.IsConnected) 
            return;
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        options.IsOpen = true;
        options.IsVisible = true;
        if (IsUsernameCorrect(playerName.text))
        {
            PhotonNetwork.JoinOrCreateRoom(roomName.text, options, TypedLobby.Default);
        }

        if (playerName.text.Trim() == "" || playerName.text == null)
        {
            emptyUsername.gameObject.SetActive(true);
        }
        if (roomNameInput.text.Trim() == "" || roomNameInput.text == null)
        {
            emptyRoomname.gameObject.SetActive(true);
        }

    }
    public bool IsUsernameCorrect(string username)
    {
        return !(playerName.text.Length >= 11 || playerName.text == "" || playerName.text == null) && roomNameInput.text != null && roomNameInput.text != "";
    }

    private void Update()
    {
        if (!(playerName.text.Trim() == "" || playerName.text == null))
        {
            emptyUsername.gameObject.SetActive(false);
        }
        if (!(roomNameInput.text.Trim() == "" || roomNameInput.text == null))
        {
            emptyRoomname.gameObject.SetActive(false);
        }
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created room successfully");
        roomsCanvases.CurrentRoomCanvas.Show();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room Creation Failed");  
    }

}
