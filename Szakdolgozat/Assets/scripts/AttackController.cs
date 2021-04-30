using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public PlayerClass playerClass;
    public Animation[] anim;
    public Collider[] weaponCollider;
    public PhotonView PV;
    public Transform player;
    public PhotonView WeaponsPV;
    public float swordDmg;
    public float spearDmg;
    public float wandDmg;

    private int currentWeaponIndex = 0;
    public GameObject sword, spear, ranged;
    private void Update()
    {
        //switch weapon
        if (Input.GetKeyDown(KeyCode.Q) && !anim[currentWeaponIndex].isPlaying)
        {
            currentWeaponIndex += 1; //ezt kell elküldeni a többi playernek
            if (currentWeaponIndex > 2)
            {
                currentWeaponIndex = 0;
            }
            WeaponsPV.RPC("SwitchWeapon",RpcTarget.All, currentWeaponIndex, WeaponsPV.ViewID);
        }

        //basic attack
        if (Input.GetKeyDown(KeyCode.Mouse0) && !anim[currentWeaponIndex].isPlaying){
            anim[currentWeaponIndex].Play();
            if(currentWeaponIndex != 2)
                weaponCollider[currentWeaponIndex].enabled = true;
            else
            {
                GameObject fireball = PhotonNetwork.Instantiate("fireball",PhotonView.Find(WeaponsPV.ViewID).transform.Find("Wand_").transform.Find("Wand").transform.Find("Weapon-Wand").transform.Find("FireFromHere").transform.position, this.transform.rotation);
                fireball.transform.Find("Sphere").GetComponent<Projectile>().parent = gameObject.transform.parent.GetComponent<PhotonView>().ViewID;
            }
        }

        if (!anim[currentWeaponIndex].isPlaying){
            if (currentWeaponIndex != 2)
                weaponCollider[currentWeaponIndex].enabled = false;
        }
    }
    public enum Weapons { Sword = 0, Spear = 1, Ranged = 2 };
    void Start()
    {
       // PV = GetComponent<PhotonView>();
    }

    [PunRPC]
    void SwitchWeapon(int weaponID, int viewID)
    {      
        switch ((Weapons)(weaponID))
        {
            case Weapons.Sword:
                //sword.SetActive(true);
                //spear.SetActive(false);
                //ranged.SetActive(false);
                PhotonView.Find(viewID).gameObject.transform.Find("Wand_").gameObject.SetActive(false);
                PhotonView.Find(viewID).gameObject.transform.Find("Sword_").gameObject.SetActive(true);
                PhotonView.Find(viewID).transform.parent.GetComponent<PlayerClass>().Dmg = swordDmg;
                break;
            case Weapons.Spear:
                //sword.SetActive(false);
                //spear.SetActive(true);
                //ranged.SetActive(false);
                PhotonView.Find(viewID).gameObject.transform.Find("Sword_").gameObject.SetActive(false);
                PhotonView.Find(viewID).gameObject.transform.Find("Spear_").gameObject.SetActive(true);
                PhotonView.Find(viewID).transform.parent.GetComponent<PlayerClass>().Dmg = spearDmg;
                break;
            case Weapons.Ranged:
                //sword.SetActive(false);
                //spear.SetActive(false);
                //ranged.SetActive(true);

                PhotonView.Find(viewID).gameObject.transform.Find("Spear_").gameObject.SetActive(false);
                PhotonView.Find(viewID).gameObject.transform.Find("Wand_").gameObject.SetActive(true);
                PhotonView.Find(viewID).transform.parent.GetComponent<PlayerClass>().Dmg = wandDmg;
                break;
            default:
                break;
        }
 
    }

   

}
