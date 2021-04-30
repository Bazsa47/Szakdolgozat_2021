using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testEnemySlain : MonoBehaviour
{

    public Enemy enemy;
    public PhotonView pv;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("weapon"))
        {
            if (!pv.IsMine)
            {
                pv.RequestOwnership();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("weapon"))
        {
            if (pv.IsMine)
            {
                int newHp = enemy.hp - 35;
                if (newHp <= 0)
                {
                    PhotonNetwork.Destroy(this.gameObject);
                }
                else
                {
                    pv.RPC("DmgEnemy", RpcTarget.All, newHp);

                }

            }
                      
        }
    }

    [PunRPC]
    public void DmgEnemy(int newHp)
    {
            enemy.hp = newHp;
               
            //kiküldi minden kliensnek, de csak az tudja elpusztítani akié, így a többiek hibát kapnak.

    }

}
