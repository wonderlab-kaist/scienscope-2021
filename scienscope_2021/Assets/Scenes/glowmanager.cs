using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glowmanager : MonoBehaviour
{
    public Material original, glow;
    bool isGlowing = true;

    public void SetGlow()
    {
        if (isGlowing)
        {
            gameObject.GetComponent<MeshRenderer>().material = glow;
            isGlowing = false;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = original;
            isGlowing = true;
        }


        
    }
}
