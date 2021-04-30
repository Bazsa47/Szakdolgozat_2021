using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Realtime;

public class PlayerListing : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    public Player Player { get; private set; }




    public void SetPlayerInfo(Player player)
    {
        this.Player = player;
        text.text = player.NickName; 
    }
}
