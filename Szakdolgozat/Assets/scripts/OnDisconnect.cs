using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OnDisconnect : MonoBehaviour
{
    private void OnApplicationQuit()
    {       
        int index = -1;

        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if (PhotonNetwork.PlayerList[i].IsLocal)
            {
                index = i;
                break;
            }
        }

        GetComponent<PhotonView>().RPC("PlayerGone", RpcTarget.All,index);
    }

   

    [PunRPC]
    public void PlayerGone(int index)
    {
        Debug.Log(PhotonNetwork.PlayerList[index].NickName + " left");
        gameObject.transform.Find("Camera").transform.Find("Canvas").transform.Find("UI").GetComponent<ManageHpSliders>().PlayerDisconnected(index);
    }

    private void Update()
    {
    }

   

   
}
