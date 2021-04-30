using Photon.Pun;
using TMPro;
using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private int wave = 0;
    private float multiplierByPlayerNum = 1;
    public int enemyNum;
    [SerializeField] private int maxEnemy;
    public GameObject[] enemySpawnpoints;
    public float waitBetweenWaves;
    public GameObject gateLeft;
    public GameObject gateRight;
    public float baseHP = 100, baseDmg = 10;

    void Start()
    {
        enemyNum = 5;
       for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++){
            multiplierByPlayerNum += 0.25f;
       }
       StartCoroutine("StartNewWave");        

    }


    public void StartNextWave()
    {     
       
        StartCoroutine("StartNewWave");
    }
    public IEnumerator StartNewWave()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        //kiírjuk a canvasra (ha nem az első wave) h wave cleared (fadein, fadeout) TODO
        if (wave >= 1)
        {
            for (int i = 0; i < players.Length; i++)
            {
                players[i].transform.Find("Camera").transform.Find("Canvas").transform.Find("UI").transform.Find("cleared").GetComponent<FadeIntext>().FadeIn();
            }
        }

        //ha új wave, akkor várunk egy kicsit, pár mp 
        yield return new WaitForSeconds(waitBetweenWaves);

        //növeljük a hullám számlálót.
        wave++; 

        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<PhotonView>().RPC("IncreaseWaves", RpcTarget.All, wave);
        }

    
        //lespawnoljuk az új enemiket
        SpawnEnemies();

        //kinyitjuk az ajtót 
        gateLeft.GetComponent<ManageGate>().OpenLeftGate();
        gateRight.GetComponent<ManageGate>().OpenRightGate();

        //várunk egy kicsit, és becsukjuk az ajtót
        StartCoroutine("WaitForDoor");

    }  

    public void SpawnEnemies()
    {
        enemyNum = maxEnemy;
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            for (int i = 0; i < maxEnemy; i++)
            {
                GameObject enemy = PhotonNetwork.InstantiateSceneObject("Enemy", enemySpawnpoints[i].transform.position, Quaternion.identity, 0);
                enemy.gameObject.GetComponent<EnemyClass>().Hp = baseHP + wave * 5f * multiplierByPlayerNum;
                enemy.gameObject.GetComponent<EnemyClass>().Dmg = baseDmg + wave * 0.025f;
                Debug.Log(baseDmg +" + "+ wave +" * "+ 0.025f + " = " +enemy.gameObject.GetComponent<EnemyClass>().Dmg);
            }
            if (wave % 2 == 0  && maxEnemy <= 40) 
                maxEnemy++;
        }
    }

    [PunRPC]
    public void IncreaseWaves(int wave)
    {
        gameObject.transform.Find("Camera").transform.Find("Canvas").transform.Find("UI").transform.Find("WaveCounter").GetComponent<TextMeshProUGUI>().SetText(wave.ToString());
    }

    public IEnumerator WaitForDoor()
    {
        yield return new WaitForSeconds(10);

        gateLeft.GetComponent<ManageGate>().CloseLeftGate();
        gateRight.GetComponent<ManageGate>().CloseRightGate();
    }




}
