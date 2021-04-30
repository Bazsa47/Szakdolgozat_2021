using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Cinemachine;

public class PhotonPlayer : MonoBehaviour
{
    private PhotonView PV;
    public GameObject myAvatar;
    public GameObject photonNetworkPlayer;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
        int spawnPoint =  Random.Range(0, GameSetup.GS.spawnPoints.Length);
        int current_index = 0;
        if (PV.IsMine)
        {
            myAvatar = PhotonNetwork.Instantiate("PlayerAvatar", photonNetworkPlayer.transform.position, GameSetup.GS.spawnPoints[spawnPoint].rotation, 0);
            myAvatar.GetComponent<player_movement>().enabled = false;
            myAvatar.transform.Find("Camera").gameObject.AddComponent<Camera>();
            myAvatar.transform.Find("Camera").gameObject.AddComponent<CinemachineBrain>();

            GameObject cmfl = new GameObject();
            cmfl.transform.parent = myAvatar.transform;
            cmfl.AddComponent<CinemachineFreeLook>();
            CinemachineFreeLook camcontroller = cmfl.GetComponent<CinemachineFreeLook>();
            camcontroller.LookAt = myAvatar.transform;
            camcontroller.Follow = myAvatar.transform;
            camcontroller.m_BindingMode = CinemachineTransposer.BindingMode.WorldSpace;
            camcontroller.m_XAxis.m_InvertInput = false;
            camcontroller.m_YAxis.m_InvertInput = true;
            //TOP
            camcontroller.m_Orbits[0].m_Height = 4;
            camcontroller.m_Orbits[0].m_Radius = 7.5f;
            //MIDDLE
            camcontroller.m_Orbits[1].m_Height = 2;
            camcontroller.m_Orbits[1].m_Radius = 7.5f;
            //BOTTOM
            camcontroller.m_Orbits[2].m_Height = -1f;
            camcontroller.m_Orbits[2].m_Radius = 5;

            cmfl.AddComponent<CinemachineCollider>();
            cmfl.GetComponent<CinemachineCollider>().m_CollideAgainst = LayerMask.GetMask("obstacle");
            myAvatar.GetComponent<player_movement>().enabled = true;

        }
        
    }
}
