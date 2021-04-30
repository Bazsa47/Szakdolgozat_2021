using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;

public class SetPlayerName : MonoBehaviour
{
    public GameSettings gm;
    public TMP_InputField names;
    public GameObject errorMsg;
    public void PlayerNameChanged()
    {
        if (names.text.Length < 11)
        {
            errorMsg.SetActive(false);
            gm.NickName = names.text;
        }
        else
        {
            errorMsg.SetActive(true);
        }
    }
}
