using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Manager/GameSettings")]
public class GameSettings : SingletonScriptableObject<MasterManager>
{
    [SerializeField]
    private string gameVersion = "0.0.0";
    [SerializeField]
    private string nickName = "Player";

    public string GameVersion { get => gameVersion;  }
    public string NickName { 
        get => nickName + Random.Range(0, 99999).ToString(); 
        set
        {
            nickName = value;
            PhotonNetwork.LocalPlayer.NickName = nickName;
        }
    
    }
}
