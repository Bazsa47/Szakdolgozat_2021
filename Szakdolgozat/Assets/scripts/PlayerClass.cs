using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class PlayerClass : Entity
{
    [SerializeField]
    private string playerName;
    [SerializeField]
    private float threat;

    public float Threat {
        get => threat; 
        set => threat = value; 
    }
    public string PlayerName{
        get => playerName;
        set => playerName = value;
    }

    private bool canTakeDmg = true;
    private float countdown = 2f;
    void Awake()
    {
        if (this.GetComponent<PhotonView>().IsMine)
        {
            this.GetComponent<PhotonView>().RPC("SetNames", RpcTarget.All, this.GetComponent<PhotonView>().ViewID,PhotonNetwork.LocalPlayer.NickName.ToString());
            this.PlayerName = PhotonNetwork.LocalPlayer.NickName.ToString();
        }
        InvokeRepeating("PlayerHealthRegen", 1f, 1f);

    }
    private void Update()
    {
        if (canTakeDmg == false)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0f)
            {
                canTakeDmg = true;
                countdown = 3f;
            }
        }
    }

    void PlayerHealthRegen()
    {
        GetComponent<PhotonView>().RPC("PlayerHealthRegenRPC", RpcTarget.All);
    }

    [PunRPC]
    public void PlayerHealthRegenRPC()
    {

        if (Hp < 250)
        {
            float newHp = 0;
            newHp = Hp + 1.25f;
            if (newHp > 250)
                newHp = 250;
            Hp = newHp;
            GetComponent<PhotonView>().RPC("ManageHpBars", RpcTarget.All, this.playerName, newHp);
        }

       
    }
    [PunRPC]
    public void SetNames(int viewID, string name)
    {
        PhotonView.Find(viewID).gameObject.GetComponent<PlayerClass>().PlayerName = name;
    }

    public override void TakeDmg(float newHp)
    {
        this.GetComponent<PhotonView>().RPC("ManageHpBars", RpcTarget.All, this.playerName, newHp);
        this.GetComponent<PhotonView>().RPC("TakeDmgRPC", RpcTarget.All, this.GetComponent<PhotonView>().ViewID, newHp);
    }

    [PunRPC]
    public void TakeDmgRPC(int viewID, float newHp)
    {
        if (canTakeDmg == true)
        {
            PhotonView.Find(viewID).gameObject.GetComponent<PlayerClass>().Hp = newHp;
            canTakeDmg = false;
        }
    }

    [PunRPC]
    public void ManageHpBars(string name, float newHp)
    {
        if(canTakeDmg)
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
           if(PhotonNetwork.PlayerList[i].NickName.ToString() == name)
           {
                string barName = "HpBar";
                if (i > 0) 
                    barName += " (" + i + ")";
                 GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                for (int j = 1; j <= players.Length; j++)
                {
                  Slider s = players[j-1].gameObject.transform.Find("Camera").transform.Find("Canvas").transform.Find("UI").transform.Find(barName).GetComponent<Slider>();
                        if (s != null)
                        {
                            s.value = newHp;
                        }
                }
                break;
           }
        }
    }
  

    public override void Die()
    {
        if (canTakeDmg)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < players.Length; i++)
            {
                players[i].GetComponent<PhotonView>().RPC("DieRpc", RpcTarget.All);
            }

        }
    }

    [PunRPC]
    public void DieRpc()
    {
        gameObject.transform.Find("Camera").transform.Find("Canvas").transform.Find("UI").transform.Find("DeathPanel").gameObject.SetActive(true);
        GetComponent<player_movement>().enabled = false;
        gameObject.transform.Find("Weapons").GetComponent<AttackController>().enabled = false;
        Cursor.visible = true;
    }

}
