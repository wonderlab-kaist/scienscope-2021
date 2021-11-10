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
    //public Slider speed;

    public GameObject warning_panel;
    public GameObject change_filter_button;
    public Text power;

    public GameObject guide_box;

    /// <summary>
    /// Small guide paragraph
    ///
    /// </summary>
    private string clip_explanation = "클립과 같이 큰 이물질은 보이는 바와 같이 더스트백에 걸러집니다.";
    private string dirt_explanation = "미세한 먼지들은 더스트백을 통과해서 터빈을 지나, 정화 필터에서 걸러집니다.";
    private string dust_explanation = "머리카락이나 솜과 같은 섬유질들은 더스트백에서 걸러집니다.";
    private string finedust_explanation = "초미세먼지들은 정화 필터에서도 완전히 걸러지지 않습니다. 우리가 물걸레질을 해야하는 이유지요.";

    private string turvin_explanation = "가운데 터빈이 돌며 발생하는 흡기에 의해 이물질이 더스트백과 정화 필터에 걸러집니다. 더스트백과 정화 필터는 주기적인 교체가 필요합니다.";


    private float turvin_gear = 0.0f;
    private float temperature;
    private float filtered;

    private float vacuum_speed_constant = 1.8f;
    private long[] viberate_pattern = { 1000, 500, 1000, 500, 1000, 500 };
    private long[] button_clicked = { 200, 100 };
    private float duration = 3.0f;

    private AudioSource vacuum_sound;
    private float vacuum_volume;
    private GameObject lastHit;
    

    // Start is called before the first frame update
    void Start()
    {
        clip.emissionRate = 0.0f;
        dust.emissionRate = 0.0f;
        dirt.emissionRate = 0.0f;
        fine_dust.emissionRate = 0.0f;

        filter_dust.emissionRate = 0.0f;

        vacuum_sound = GetComponent<AudioSource>();
        warning_panel.SetActive(false);
        guide_box.SetActive(false);
    }
    
    void Update()
    {
        //if(Input.anyKeyDown) hepa_filter.GetComponent<Animator>().Play("filter_change");

        // determining turvin speed with slider
        turbin.GetComponent<Animator>().speed = turvin_gear;
        if (power.text == "0" && turvin_gear == 1) pop_up_guide(-1);
        power.text = turvin_gear.ToString();

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

            if (filter_dust.emissionRate < 8)
            {
                StartCoroutine("warning_message");
                filter_dust.emissionRate = 8;
                GameObject.Find("BLEcontroller").GetComponent<aarcall>().vibrate_phone(viberate_pattern);
            }

            RaycastHit hit;

            if(Physics.Raycast(GameObject.Find("viewer_outer_ring").transform.position, Vector3.forward, out hit, Mathf.Infinity))
            {
                //Debug.Log("did");
                lastHit = hit.transform.gameObject;
                if (lastHit.GetComponent<Outline>() != null)
                {
                    lastHit.GetComponent<Outline>().OutlineWidth = 4.0f;
                    warning_panel.SetActive(false);
                    change_filter_button.SetActive(true);
                }
                
            }
            else if(lastHit != null && lastHit.GetComponent<Outline>() != null)
            {
                lastHit.GetComponent<Outline>().OutlineWidth = 0f;
                lastHit = null;
                warning_panel.SetActive(true);
                change_filter_button.SetActive(false);
            }
        }

        //control sound//
        if (!vacuum_sound.isPlaying && turvin_gear >= 1) vacuum_sound.Play();
        else if (vacuum_sound.isPlaying && turvin_gear == 0) vacuum_sound.Stop();

        if (vacuum_sound.isPlaying) vacuum_sound.pitch = 0.4f + turvin_gear * 0.3f;
    }

    public void add_turvin(int btn)
    {
        GameObject.Find("BLEcontroller").GetComponent<aarcall>().vibrate_phone(button_clicked);
        turvin_gear += btn;
        if (turvin_gear < 0) turvin_gear = 0;
        else if (turvin_gear > 3) turvin_gear = 3;
    }

    public void particle_burst(int type)
    {
        GameObject.Find("BLEcontroller").GetComponent<aarcall>().vibrate_phone(button_clicked);
        if (turvin_gear >= 1f)
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

    IEnumerator warning_message()
    {
        for(int i=0;i<5;i++)
        {
            warning_panel.active = !warning_panel.active;

            yield return new WaitForSeconds(.5f);
        }
    }

    public void change_filter()
    {
        filtered = 0;
        lastHit.GetComponent<Outline>().OutlineWidth = 0f;
        lastHit = null;
        warning_panel.SetActive(false);
        change_filter_button.SetActive(false);
        filter_dust.emissionRate = 0;
        hepa_filter.GetComponent<Animator>().Play("filter_change");
        hepa_filter.GetComponent<AudioSource>().Play();
    }

    public void pop_up_guide(int index)
    {
        switch (index)
        {
            case 0:
                guide_box.transform.GetChild(0).gameObject.GetComponent<Text>().text = clip_explanation;
                break;
            case 1:
                guide_box.transform.GetChild(0).gameObject.GetComponent<Text>().text = dirt_explanation;
                break;
            case 2:
                guide_box.transform.GetChild(0).gameObject.GetComponent<Text>().text = dust_explanation;
                break;
            case 3:
                guide_box.transform.GetChild(0).gameObject.GetComponent<Text>().text = finedust_explanation;
                break;
            case -1:
                
                guide_box.transform.GetChild(0).gameObject.GetComponent<Text>().text = turvin_explanation;
                break;
        }

        if (!guide_box.active) StartCoroutine("pop_up", duration);
        else
        {
            StopCoroutine("pop_up");
            StartCoroutine("pop_up", duration);
        }
    }

    IEnumerator pop_up(float duration)
    {
        guide_box.SetActive(true);
        yield return new WaitForSeconds(duration);
        guide_box.SetActive(false);
    }
}
