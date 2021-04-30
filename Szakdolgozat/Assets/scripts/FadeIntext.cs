using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIntext : MonoBehaviour
{
    public void FadeIn()
    {
       GetComponent<Animation>().Play("fade_in_text");
    }

}
