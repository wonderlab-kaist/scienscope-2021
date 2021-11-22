using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class filter_colliding : MonoBehaviour
{
    public vacuum_scene_control vc;
    ParticleSystem ps;
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleCollision(GameObject other)
    {
        Debug.Log("asj");

    }

    void OnParticleTrigger()
    {
        int numInside = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        //Debug.Log(numEnter);

        if (numEnter > 0)
        {
            vc.particle_filtered(numEnter);
        }
    }
}
