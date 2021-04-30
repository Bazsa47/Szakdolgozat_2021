using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisintestscript : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision Entered: " + other.other.name);
    }

    private void OnCollisionStay(Collision other)
    {
        Debug.Log("Collision is oocuring: " + other.other.name);
    }

    private void OnCollisionExit(Collision other)
    {
        Debug.Log("Collision exited: " + other.other.name);
    }
}
