using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : Entity
{
    [SerializeField]
    private Transform target;
    public Transform Target {
        get => target;
        set => target = value;
    }
    public override void Die()
    {
        PhotonView pv = GetComponent<PhotonView>();
        pv.RPC("DieRPC", RpcTarget.All, pv.ViewID);
    }

    [PunRPC]
    public void DieRPC(int viewID)
    {
        if (PhotonView.Find(viewID).IsMine)
        {
            PhotonNetwork.Destroy(PhotonView.Find(viewID).gameObject);
        }


        GameObject.FindGameObjectWithTag("wavemanager").GetComponent<WaveManager>().enemyNum--;
        if (GameObject.FindGameObjectWithTag("wavemanager").GetComponent<WaveManager>().enemyNum <= 0)
        {
            GameObject.FindGameObjectWithTag("wavemanager").GetComponent<WaveManager>().StartNextWave();
        }
    }

    public override void TakeDmg(float newHp)
    {
        this.GetComponent<PhotonView>().RPC("TakeDmgRPC", RpcTarget.All, this.GetComponent<PhotonView>().ViewID, newHp);
    }

    [PunRPC]
    public void TakeDmgRPC(int viewID, float newHp)
    {
        PhotonView.Find(viewID).gameObject.GetComponent<EnemyClass>().Hp = newHp;
    }

    void Awake()
    {
        this.Target = GameObject.FindGameObjectWithTag("Nexus").transform;
    }

}
