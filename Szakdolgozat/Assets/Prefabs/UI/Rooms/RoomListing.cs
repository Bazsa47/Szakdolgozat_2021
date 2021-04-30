using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class RoomListing : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    private string playerName;
    private GameObject errorMsg;
    public RoomInfo RoomInfo { get; private set; }

    private void Start()
    {
        
        errorMsg = gameObject.transform.parent.gameObject.transform.parent.transform.parent.transform.parent.transform.parent.transform.Find("PlayernNameInput").gameObject.transform.Find("usernameEmptyErrormsg (1)").GetComponent<TextMeshProUGUI>().gameObject;
    }
    public void SetRoomInfo(RoomInfo roomInfo)
    {
        RoomInfo = roomInfo;
        text.text = roomInfo.Name + " (" + roomInfo.PlayerCount+")";
    }

    public void OnClick_Button()
    {
        playerName = gameObject.transform.parent.gameObject.transform.parent.transform.parent.transform.parent.transform.parent.transform.Find("PlayernNameInput").gameObject.transform.Find("NameInput").GetComponent<TMP_InputField>().text;
        if (IsUsernameCorrect(playerName))
            PhotonNetwork.JoinRoom(RoomInfo.Name);
        else
            errorMsg.SetActive(true);
    }

    public bool IsUsernameCorrect(string username)
    {
        return !(username.Length >= 11 || username == "" || username == null);
    }
}
