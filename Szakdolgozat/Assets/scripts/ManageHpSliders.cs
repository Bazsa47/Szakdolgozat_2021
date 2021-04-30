using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class ManageHpSliders : MonoBehaviour
{
    public List<GameObject> sliders = new List<GameObject>();
    void Awake()
    {

        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if (PhotonNetwork.PlayerList[i].IsLocal)
            {
                GetComponent<PhotonView>().RPC("RpcSetBar",RpcTarget.All,i);
                break;
            }
            else
            {
                sliders[i].SetActive(false);
            }
            
        }

    }

    public void PlayerDisconnected(int index)
    {
        sliders[index].GetComponent<Slider>().value = 0;
        sliders[index].transform.Find("Disconnected").gameObject.SetActive(true);
    }



    [PunRPC]
    public void RpcSetBar(int i)
    {
        sliders[i].SetActive(true);
        sliders[i].transform.Find("PlayerName").GetComponent<TextMeshProUGUI>().SetText(PhotonNetwork.PlayerList[i].NickName);
    }

}
