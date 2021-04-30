using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ChangeTarget : MonoBehaviour
{
    private EnemyClass ec;
    private List<PlayerThreat> threatList;
    void Start()
    {
        threatList = new List<PlayerThreat>();
        ec = GetComponent<EnemyClass>();

        //szedjük össze a lehetséges célpontokat
        

        //Nexus
        int viewId = GameObject.FindGameObjectWithTag("Nexus").gameObject.transform.parent.gameObject.GetComponent<PhotonView>().ViewID;
        float threat = GameObject.FindGameObjectWithTag("Nexus").gameObject.transform.parent.gameObject.GetComponent<Nexus>().threat;
        string playerName = "Nexus";
        threatList.Add(new PlayerThreat(viewId, threat, playerName));

        //Játékosok
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < targets.Length; i++)
        {
            viewId = targets[i].GetComponent<PhotonView>().ViewID;
            threat = 0;
            playerName = targets[i].GetComponent<PlayerClass>().PlayerName;
            PlayerThreat pt = new PlayerThreat(viewId,threat,playerName);
            threatList.Add(pt);
        }

        InvokeRepeating("ThreatFade",1f,1f);
    }

    public void ThreatFade()
    {
        for (int i = 0; i < threatList.Count; i++)
        {
            if (!threatList[i].playerName.Equals("Nexus"))
            {
                if (PhotonView.Find(threatList[i].viewId) != null)
                {
                float distance = Vector3.Distance(PhotonView.Find(threatList[i].viewId).gameObject.transform.position,this.transform.position);
                float newThreat = threatList[i].threat - 1f - distance/2f;
                if (newThreat <= 0)
                    newThreat = 0;
                this.gameObject.GetComponent<PhotonView>().RPC("SetThreat", RpcTarget.All, threatList[i].viewId, newThreat);
                }
            }          
        }
    }

    void Update()
    {
        ec.Target = ChooseMostDangerousPlayer();
       
    }

    public void GenerateThreat(int viewId, float distance, float weaponDmg)
    {
        float threat = 0;
        for (int i = 0; i < threatList.Count; i++)
        {
            if (threatList[i].viewId == viewId)
            {
                threat = threatList[i].threat;
            }
        }
        float newThreat = threat + (weaponDmg / (distance/1.5f));
        this.gameObject.GetComponent<PhotonView>().RPC("SetThreat",RpcTarget.All,viewId,newThreat);
    }

    [PunRPC]
    public void SetThreat(int viewId, float newThreat)
    {
        for (int i = 0; i < threatList.Count; i++)
        {
            if (threatList[i].viewId == viewId)
            {
                threatList[i].threat = newThreat;
                break;
            }
        }
    }
    public Transform ChooseMostDangerousPlayer()
    {
        float max = -1;
        int index = -1;
        for (int i = 0; i < threatList.Count; i++){
            if (threatList[i] != null && threatList[i].threat > max){
                max = threatList[i].threat;
                index = i;
            }
        }
        if (index != -1 && PhotonView.Find(threatList[index].viewId) != null){
           //if(threatList[index].threat != 50) Debug.Log(gameObject.name + " is targeting : " + threatList[index].playerName + ". Threat : " + threatList[index].threat);
            return PhotonView.Find(threatList[index].viewId).gameObject.transform;
        }
        else{
            return GameObject.FindGameObjectWithTag("Nexus").transform;
        }       
    }

    private class PlayerThreat
    {

        public int viewId;
        public float threat;
        public string playerName;

        public PlayerThreat(int viewId, float threat, string playerName)
        {
            this.viewId = viewId;
            this.threat = threat;
            this.playerName = playerName;
        }
    }
}
