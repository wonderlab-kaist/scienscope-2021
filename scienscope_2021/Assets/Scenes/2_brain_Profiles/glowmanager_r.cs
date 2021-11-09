using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glowmanager_r : MonoBehaviour
{
    public Material original, glow;
    bool isGlowing = false;
    float Intensity;
    Color emissiveColor;
    bool decreasing = true;
    //public GameObject brain;

    public void SetGlow()
    {
        if (isGlowing==false)
        {
            gameObject.GetComponent<MeshRenderer>().material = glow;
            isGlowing = true;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = original;
            isGlowing = false;
        }


        
    }

    void Start()
    {
        emissiveColor = glow.color;
        Intensity = emissiveColor.r;
        Debug.Log(emissiveColor.r);
        Debug.Log(Intensity);
        //glow.color = Color.red;

    }

    void Update()
    {
        Debug.Log(emissiveColor.r);
        Debug.Log(Intensity);
        if (isGlowing==true)
        {
            // glow.color = Color.red;
            
            if (decreasing == true)
            {
                Intensity = Intensity - 0.01f;
                if (Intensity > 0)
                {
                    emissiveColor.r = Intensity;
                    glow.color = emissiveColor;
                }
            
            }

            else if (decreasing == false)
            {
                    
                Intensity = Intensity + 0.02f;
                emissiveColor.r = Intensity;
                glow.color = emissiveColor;
            
            }
            if (Intensity > 2.7f)
            {
                decreasing = true;
            }

            if (0.1 > Intensity)
            {
                decreasing = false;
                Intensity = 0.1f;

            }

        // Debug.Log(emissiveColor.r);
        // Debug.Log(Intensity);
            
        }
    }


}
