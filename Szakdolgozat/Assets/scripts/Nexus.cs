using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Nexus : MonoBehaviour
{
    [SerializeField]
    private float hp;
    public float maxHp = 1000;
    public float healthRegenValue = 10f;

    public float threat;

    void Start()
    {
        InvokeRepeating("HealthRegen", 1f, 1f);
    }
    void HealthRegen()
    {
        if (GetComponent<Nexus>().hp < maxHp)
        {
            float newHp = GetComponent<Nexus>().hp + healthRegenValue;
            if (newHp > maxHp)
                newHp = maxHp;
            this.GetComponent<Nexus>().hp = newHp;
            GetComponent<PhotonView>().RPC("RegenerateHealth", RpcTarget.All, newHp);
        }
       
    }

    [PunRPC]
    public void RegenerateHealth(float newHp)
    {       
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            if(IsLocalPlayer(players[i]))
            {
                Slider s = players[i].gameObject.transform.Find("Camera").transform.Find("Canvas").transform.Find("UI").transform.Find("NexusHpBar").GetComponent<Slider>();
                if (s != null)
                {
                    s.value = newHp;
                }
            }
            
        }
    }

    public void TakeDmg(float dmg)
    {
        float newhp = hp - dmg;
        if(hp <=0)
        {
            GetComponent<PhotonView>().RPC("GameOver", RpcTarget.All);
        }
        else
        {
            GetComponent<PhotonView>().RPC("TakeNexusDmgRPC", RpcTarget.All, newhp, GetComponent<PhotonView>().ViewID);
        }
    }

    [PunRPC]
    public void TakeNexusDmgRPC(float newhp, int viewId)
    {
        PhotonView.Find(viewId).GetComponent<Nexus>().hp = newhp;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int j = 1; j <= players.Length; j++)
        {
            Slider s = players[j - 1].gameObject.transform.Find("Camera").transform.Find("Canvas").transform.Find("UI").transform.Find("NexusHpBar").GetComponent<Slider>();
            if (s != null)
            {
                s.value = newhp;
            }
        }
    }

    [PunRPC]
    public void GameOver()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            if (IsLocalPlayer(players[i]))
                players[i].GetComponent<PhotonView>().RPC("DieRpc", RpcTarget.All);
            break;
        }
    }

    public bool IsLocalPlayer(GameObject g)
    {
        return (g.GetComponent<PlayerClass>() != null && g.GetComponent<PlayerClass>().PlayerName == PhotonNetwork.LocalPlayer.NickName) ? true : false;
    }

}
