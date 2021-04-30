using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject[] enemySpawnpoints;
    void Awake() 
    {
        int spawnPoint = Random.Range(0, GameSetup.GS.spawnPoints.Length);
        int index = 0;
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if (PhotonNetwork.PlayerList[i].IsLocal)
            {
                index = i;
                break;
            }
        }
        PhotonNetwork.Instantiate("Nexus", new Vector3(0.766728f, 2.7f, -21.98f), GameSetup.GS.spawnPoints[spawnPoint].rotation, 0);
        PhotonNetwork.Instantiate("PhotonNetworkPlayer", GameSetup.GS.spawnPoints[index].position, GameSetup.GS.spawnPoints[spawnPoint].rotation, 0);

        //PhotonNetwork.InstantiateSceneObject("Enemy", enemySpawnpoints[0].transform.position, GameSetup.GS.spawnPoints[spawnPoint].rotation, 0);
        //PhotonNetwork.InstantiateSceneObject("Enemy", enemySpawnpoints[1].transform.position, GameSetup.GS.spawnPoints[spawnPoint].rotation, 0);
        //PhotonNetwork.InstantiateSceneObject("Enemy", enemySpawnpoints[2].transform.position, GameSetup.GS.spawnPoints[spawnPoint].rotation, 0);
        //PhotonNetwork.InstantiateSceneObject("Enemy", enemySpawnpoints[3].transform.position, GameSetup.GS.spawnPoints[spawnPoint].rotation, 0);
        //PhotonNetwork.InstantiateSceneObject("Enemy", enemySpawnpoints[4].transform.position, GameSetup.GS.spawnPoints[spawnPoint].rotation, 0);
        //PhotonNetwork.InstantiateSceneObject("Enemy", enemySpawnpoints[5].transform.position, GameSetup.GS.spawnPoints[spawnPoint].rotation, 0);
        //PhotonNetwork.InstantiateSceneObject("Enemy", enemySpawnpoints[6].transform.position, GameSetup.GS.spawnPoints[spawnPoint].rotation, 0);
        //PhotonNetwork.InstantiateSceneObject("Enemy", enemySpawnpoints[7].transform.position, GameSetup.GS.spawnPoints[spawnPoint].rotation, 0);
        //PhotonNetwork.InstantiateSceneObject("Enemy", enemySpawnpoints[8].transform.position, GameSetup.GS.spawnPoints[spawnPoint].rotation, 0);
        //PhotonNetwork.InstantiateSceneObject("Enemy", enemySpawnpoints[9].transform.position, GameSetup.GS.spawnPoints[spawnPoint].rotation, 0);
    }
}
