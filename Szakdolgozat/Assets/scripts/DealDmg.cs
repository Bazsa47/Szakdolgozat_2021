using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DealDmg : MonoBehaviour
{
    public PlayerClass playerClass;
    private float aggroDistance = 15f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            float hp = other.gameObject.GetComponent<EnemyClass>().Hp;
            float newHp = hp - playerClass.Dmg;
            if (newHp <= 0f)
            {
                Collider[] colliders = Physics.OverlapSphere(other.transform.position, aggroDistance);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject.CompareTag("enemy"))
                    {
                        float distance = Vector3.Distance(playerClass.gameObject.transform.position, colliders[i].gameObject.transform.position);
                        colliders[i].gameObject.GetComponent<ChangeTarget>().GenerateThreat(playerClass.gameObject.GetComponent<PhotonView>().ViewID, distance, playerClass.Dmg);
                    }
                }
                other.gameObject.GetComponent<EnemyClass>().Die();
            }
                
            else
            {
                other.GetComponent<EnemyClass>().TakeDmg(newHp);

                Collider[] colliders = Physics.OverlapSphere(other.transform.position, aggroDistance);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject.CompareTag("enemy"))
                    {
                        float distance = Vector3.Distance(playerClass.gameObject.transform.position, colliders[i].gameObject.transform.position);
                        colliders[i].gameObject.GetComponent<ChangeTarget>().GenerateThreat(playerClass.gameObject.GetComponent<PhotonView>().ViewID, distance, playerClass.Dmg);
                    }
                }
            }
                                                  
        }
    }

    void Start()
    {
        //ez az előző kód else ágába megy!
       
    }
}
