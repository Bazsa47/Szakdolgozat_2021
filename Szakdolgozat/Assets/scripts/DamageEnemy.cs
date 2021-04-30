using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour, IPunOwnershipCallbacks
{
    private Enemy enemy;
    private PhotonView pv;
    public PhotonView myPV;

    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        PhotonNetwork.Destroy(targetView.gameObject);
    }

    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        PhotonNetwork.Destroy(targetView.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            enemy = other.gameObject.GetComponent<Enemy>();
            pv = other.gameObject.GetComponent<PhotonView>();
            if (!pv.IsMine)
            {
                pv.RequestOwnership();
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            enemy = other.gameObject.GetComponent<Enemy>();
            int newHp = enemy.hp - 35;
            
            myPV.RPC("DmgEnemy",RpcTarget.All, newHp,enemy);
            if (enemy.hp <= 0)
                PhotonNetwork.Destroy(other.gameObject);

        }
    }

    [PunRPC]
    public void DmgEnemy(int newHp, Enemy enemy)
    {
        enemy.hp = newHp;
    }
}
