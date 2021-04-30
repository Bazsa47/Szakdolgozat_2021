using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;

public class SetName : MonoBehaviour
{

    public TextMeshProUGUI name;
    void Start()
    {
        name.SetText(PhotonNetwork.LocalPlayer.NickName);       
    }

}
