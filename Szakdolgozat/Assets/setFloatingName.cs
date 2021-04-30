using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class setFloatingName : MonoBehaviour
{
    public TextMeshPro name;
    void Start()
    {
        name.SetText(transform.parent.GetComponent<PlayerClass>().PlayerName);

     
    }

}
