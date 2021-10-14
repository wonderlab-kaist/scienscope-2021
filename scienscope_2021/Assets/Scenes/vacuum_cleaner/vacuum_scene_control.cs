using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class vacuum_scene_control : MonoBehaviour
{
    public ParticleSystem clip, dust, fine_dust, dirt;
    public ParticleSystem filter_dust;
    public GameObject hepa_filter;
    public GameObject turbin;
    public Slider speed;


    private float turvin_gear = 0.5f;
    private float temperature;
    private float filtered;

    private float vacuum_speed_constant = 1.8f;

    private AudioSource vacuum_sound;
    private float vacuum_volume;


    // Start is called before the first frame update
    void Start()
    {
        clip.emissionRate = 0.0f;
        dust.emissionRate = 0.0f;
        dirt.emissionRate = 0.0f;
        fine_dust.emissionRate = 0.0f;

        filter_dust.emissionRate = 0.0f;

        vacuum_sound = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        speed.value = (int)(speed.value);
        turvin_gear = speed.value;

        // determining turvin speed with slider
        turbin.GetComponent<Animator>().speed = turvin_gear;

        // up particles speed with turvin speed
        clip.startSpeed = vacuum_speed_constant * turvin_gear;
        dust.startSpeed = vacuum_speed_constant * turvin_gear;
        fine_dust.startSpeed = vacuum_speed_constant * turvin_gear;
        dirt.startSpeed = vacuum_speed_constant * turvin_gear;

        // updating filter color
        Material filter_color = hepa_filter.GetComponent<Renderer>().material;
        if (filtered > 255) filtered = 255;
        filter_color.color = new Color((255 - filtered)/255f, (255 - filtered) / 255f, (255 - filtered) / 255f);

        //Debug.Log(filtered);

        // when the filter is done (max dirt)
        if (filtered > 200)
        {
            hepa_filter.transform.localPosition = new Vector3(Mathf.Sin(Time.time * 1000f) * 2 * (filtered) / 255f, Mathf.Sin(Time.time * 1000f) * 2 * (filtered) / 255f, 0);
            filter_dust.emissionRate = 8;
        }

        //control sound//
        if (!vacuum_sound.isPlaying && turvin_gear >= 1) vacuum_sound.Play();
        else if (vacuum_sound.isPlaying && turvin_gear == 0) vacuum_sound.Stop();

        if (vacuum_sound.isPlaying) vacuum_sound.pitch = 0.4f + turvin_gear * 0.3f;
    }

    public void particle_burst(int type)
    {
        if(turvin_gear >= 1f)
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
        
    }

    IEnumerator burst(ParticleSystem pc)
    {
        pc.emissionRate = 25f;

        while (pc.emissionRate > 0)
        {
            pc.emissionRate -= 0.1f;
            yield return new WaitForEndOfFrame();

        }

        if (pc.emissionRate < 0) pc.emissionRate = 0;
    }

    public void particle_filtered(int numb_particles)
    {
        filtered += numb_particles+1;
    }
}