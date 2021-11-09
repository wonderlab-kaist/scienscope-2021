using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glowmanager_g : MonoBehaviour
{
    public Material original, glow;
    bool isGlowing = false;
    float Intensity;
    Color emissiveColor;
    bool decreasing = true;
    //public GameObject brain;
    public mic_input mic;
    

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
        if (isGlowing)
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
        Intensity = emissiveColor.g;
        //mic = GameObject.Find("scene_control").GetComponent<mic_input>();
        //if (mic == null) GameObject.Destroy(this.gameObject);
;        //Debug.Log(emissiveColor);
        // Debug.Log(Intensity);
        //glow.color = Color.red;

    }

    void Update()
    {
        if (isGlowing==true)
        {
            Intensity = mic.getLoudness() * 30f;
            if (Intensity < 0.3f) Intensity = 0;
            emissiveColor.g = Intensity;
            glow.color = emissiveColor;
            // glow.color = Color.red;
            /*
            if (decreasing == true)
            {
                Intensity = Intensity - 0.01f;
                if (Intensity>0)
                {
                    emissiveColor.g = Intensity;
                    glow.color = emissiveColor;
                }
            
            }

            else if (decreasing == false)
            {
                    
                Intensity = Intensity + 0.02f;
                emissiveColor.g = Intensity;
                glow.color = emissiveColor;
            
            }
            if (Intensity > 2.7f)
            {
                decreasing = true;
            }

            if (0.5 > Intensity)
            {
                decreasing = false;
                Intensity = 0.5f;

            }


            */
        }
    }


}
