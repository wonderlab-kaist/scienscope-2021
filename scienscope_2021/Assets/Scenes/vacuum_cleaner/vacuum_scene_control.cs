using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vacuum_scene_control : MonoBehaviour
{
    public ParticleSystem clip, dust, fine_dust, dirt;
    public GameObject hepa_filter;
    public GameObject turbin;

    int turvin_gear = 1;
    private float temperature;
    private float filtered;


    // Start is called before the first frame update
    void Start()
    {
        clip.emissionRate = 0.0f;
        dust.emissionRate = 0.0f;
        dirt.emissionRate = 0.0f;
        fine_dust.emissionRate = 0.0f;
    }
    
    void Update()
    {
        
    }

    public void particle_burst(int type)
    {
        switch (type)
        {
            case 0:
                StartCoroutine("burst", clip);
                break;
            case 1:
                StartCoroutine("burst", dirt);
                break;
            case 2:
                StartCoroutine("burst", dust);
                break;
            case 3:
                StartCoroutine("burst", fine_dust);
                break;
        }
        
    }

    IEnumerator burst(ParticleSystem pc)
    {
        Debug.Log("burst");
        pc.emissionRate = 15f;

        while (pc.emissionRate > 0)
        {
            pc.emissionRate -= 0.1f;
            yield return new WaitForEndOfFrame();

        }

        if (pc.emissionRate < 0) pc.emissionRate = 0;
    }
}
