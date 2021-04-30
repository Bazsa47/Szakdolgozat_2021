using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    public static GameSetup GS;

    public Transform[] spawnPoints;

    private void OnEnable()
    {
        Cursor.visible = false;
        if (GameSetup.GS == null)
        {
            GameSetup.GS = this;
        }
    }
   
}
