using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DealDmgFireball : MonoBehaviour
{
    private int parentId;

    private void Start()
    {
        parentId = GetComponent<Projectile>().parent;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            GetComponent<PhotonView>().RPC("DestroyFireball",RpcTarget.All, GetComponent<PhotonView>().ViewID);
            float hp = other.gameObject.GetComponent<EnemyClass>().Hp;
            float newHp = hp - 30;
            if (newHp <= 0f)
                other.gameObject.GetComponent<EnemyClass>().Die();
            else
            {
                other.GetComponent<EnemyClass>().TakeDmg(newHp);
                Collider[] colliders = Physics.OverlapSphere(other.transform.position, 10f);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject.CompareTag("enemy"))
                    {
                        float distance = Vector3.Distance(gameObject.transform.position, colliders[i].gameObject.transform.position);
                        colliders[i].gameObject.GetComponent<ChangeTarget>().GenerateThreat(PhotonView.Find(parentId).gameObject.GetComponent<PhotonView>().ViewID, distance, PhotonView.Find(parentId).GetComponent<PlayerClass>().Dmg);
                    }
                }
                // other.GetComponent<ChangeTarget>().GenerateThreat(gameObject.GetComponent<Projectile>().parent);
            }
 
        }
        else if(!other.CompareTag("Player"))
        {
            GameObject explosion = PhotonNetwork.Instantiate("Explosion", this.gameObject.transform.position, Quaternion.identity);
            GetComponent<PhotonView>().RPC("DestroyFireball", RpcTarget.All, GetComponent<PhotonView>().ViewID);
        }
    }

    [PunRPC]
    public void DestroyFireball(int viewId)
    {
        if (PhotonView.Find(viewId).IsMine)
        {
            GameObject explosion = PhotonNetwork.Instantiate("Explosion", this.gameObject.transform.position, Quaternion.identity);
            PhotonNetwork.Destroy(PhotonView.Find(viewId).transform.parent.gameObject);          
        }
    }
}
