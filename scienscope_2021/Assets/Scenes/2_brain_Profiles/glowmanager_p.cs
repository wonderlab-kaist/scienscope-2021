using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glowmanager_p : MonoBehaviour
{
    public Material original, glow;
    bool isGlowing = false;
    float Intensity_r;
    float Intensity_b;
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
        Intensity_r = emissiveColor.r;
        Intensity_b = emissiveColor.b;
        // Debug.Log(emissiveColor.r);
        Debug.Log(Intensity_r);
        Debug.Log(Intensity_b);
        //glow.color = Color.red;

    }

    void Update()
    {
        // Debug.Log(emissiveColor.r);
        // Debug.Log(Intensity_r);
        if (isGlowing==true)
        {
            // glow.color = Color.red;
            
            if (decreasing == true)
            {
                Intensity_r = Intensity_r - 0.005f;
                Intensity_b = Intensity_b - 0.01f;

                if (Intensity_r > 0)
                {
                    emissiveColor.r = Intensity_r;
                    emissiveColor.b = Intensity_b;
                    glow.color = emissiveColor;
                }
            
            }

            else if (decreasing == false)
            {
                    
                Intensity_r = Intensity_r + 0.005f;
                Intensity_b = Intensity_b + 0.01f;
                emissiveColor.r = Intensity_r;
                emissiveColor.b = Intensity_b;
                glow.color = emissiveColor;
            
            }
            if (Intensity_r > 0.5f)
            {
                decreasing = true;
                Intensity_b = 1.0f;
            }

            if (0.1 > Intensity_r)
            {
                decreasing = false;
                Intensity_r = 0.1f;
                Intensity_b = 0.1f;

            }

        // Debug.Log(emissiveColor.r);
        // Debug.Log(Intensity);
            
        }
    }


}
