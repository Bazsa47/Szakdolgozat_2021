using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageGate : MonoBehaviour
{
    string openGate;
    string closeGate;
    Animation anim;
   public void CloseLeftGate()
   {
        anim.Play("gate_close");
   }

    public void OpenLeftGate()
    {
        anim.Play("gate_open");
    }

    public void CloseRightGate()
    {
        anim.Play("gate_close_right");
    }

    public void OpenRightGate()
    {
        anim.Play("gate_open_right");
    }

    void Awake()
    {
        anim = GetComponent<Animation>();       
    }
}
