using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class interactable_highlight : MonoBehaviour
{
    public float radius;

    private GameObject viewer_aim;

    /// <summary>
    /// this script temporary for the heart prefab
    /// </summary>
    float duration;
    bool guide_played = false;
    long[] beat = { 400, 600 };

    void Start()
    {
        
        viewer_aim = GameObject.Find("viewer_outer_ring"); // outer ring should be this name "viewer_outer_ring"
        
    }

    // Update is called once per frame
    void Update()
    {
        ///x,y distance check///
        Vector3 proj_this = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        Vector3 proj_aim = new Vector3(viewer_aim.transform.position.x, viewer_aim.transform.position.y, 0);

        float d = Vector3.Distance(proj_this, proj_aim);
        //Debug.Log(d);
        if (d < radius) highlighted();
        else
        {
            this.GetComponent<Outline>().OutlineWidth = 0f;
            guide_played = false;
            if (this.gameObject.name == "heart animated prefab")
            {
                this.GetComponent<AudioSource>().volume *= 0.98f;
                GameObject.Find("Explanation").GetComponent<Text>().text = "우리 몸의 내부는 \n 어떻게 생겼을까요?";
                GameObject.Find("Explanation Detailed").GetComponent<Text>().text = " ";
                
            }
        }
    }

    void highlighted()  ///////script when the gameobject is highlighted
    {
        this.GetComponent<Outline>().OutlineWidth = 2f;
        ////
        /// add what you want
        if(this.gameObject.name == "heart animated prefab")
        {
            GameObject.Find("Explanation").GetComponent<Text>().text = "심 장";
            GameObject.Find("Explanation Detailed").GetComponent<Text>().text = "우리 몸 곳곳에 피를 보내주는 역할을 합니다.";

            this.GetComponent<AudioSource>().volume = 1f;
            if (duration > 1000)
            {
                GameObject.Find("BLEcontroller").GetComponent<aarcall>().vibrate_phone(beat);
                duration = 0;
            }else
            {
                duration += Time.deltaTime * 1000;
            }

            if (!guide_played)
            {
                GameObject.Find("guide_sound").GetComponent<AudioSource>().Play();
                guide_played = true;
            }
            
        }else if(this.gameObject.name == "Crystal")
        {
            this.GetComponent<Outline>().OutlineWidth = 15f;
            if (!guide_played)
            {
                this.GetComponent<AudioSource>().Play();
                guide_played = true;
            }
        }


        ///
        ///

    }
}
