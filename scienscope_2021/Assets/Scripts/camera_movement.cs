using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class camera_movement : MonoBehaviour
{
    public Text raw_data;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string income = dataInput.getData();
        if (income != "") raw_data.text = income;

        string[] data = raw_data.text.Split(' ');

        if(data.Length > 3)
        {
            Vector3 delta = new Vector3(float.Parse(data[4]), float.Parse(data[3]), 0);
            move_smooth(delta);
            //raw_data.text = "";
        }else if(income == "")
        {
            raw_data.text = "";
        }
    }


    void move_smooth(Vector3 delta_distance)
    {
        StartCoroutine("moveSmooth", delta_distance * 0.000005f);

    }

    IEnumerator moveSmooth(Vector3 d)
    {
        for(int i = 0; i < 4; i++)
        {
            cam.transform.position -= d / 4f;
            yield return new WaitForSeconds(0.02f/4);
        }

    }
}
