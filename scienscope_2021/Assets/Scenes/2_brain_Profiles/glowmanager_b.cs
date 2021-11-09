using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glowmanager_b : MonoBehaviour
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
    public void SetGlow(bool _isGlowing)
    {
        isGlowing = _isGlowing;
        if (isGlowing == true)
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
        Intensity = emissiveColor.b;
        // Debug.Log(emissiveColor.g);
        // Debug.Log(Intensity);
        //glow.color = Color.red;

    }

    void Update()
    {
        if (isGlowing==true)
        {
            // glow.color = Color.red;
            
            if (decreasing == true)
            {
                Intensity = Intensity - 0.02f;
                if (Intensity>0)
                {
                    emissiveColor.b = Intensity;
                    glow.color = emissiveColor;
                }
            
            }

            else if (decreasing == false)
            {
                    
                Intensity = Intensity + 0.02f;
                emissiveColor.b = Intensity;
                glow.color = emissiveColor;
            
            }
            if (Intensity > 3.5f)
            {
                decreasing = true;
            }

            if (0.1 > Intensity)
            {
                decreasing = false;
                Intensity = 0.1f;

            }


            
        }
    }


}
