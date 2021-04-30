using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class IncreaseWave : MonoBehaviour
{

    [PunRPC]
    public void IncreaseWaves(int wave)
    {
        gameObject.transform.Find("Camera").transform.Find("Canvas").transform.Find("UI").transform.Find("WaveCounter").GetComponent<TextMeshProUGUI>().SetText("Wave : " + wave);
    }
}
