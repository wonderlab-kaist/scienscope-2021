using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class camera_movement : MonoBehaviour
{
    public float gain = 0.000009f;

    public Text raw_data; //debugging text
    public float[] f_raw_data; //parsed raw-data
    public Camera cam;
    public Transform rig;

    //public Transform rotate_tester;

    private float[] q; //Quaternion container (temporal)
    private Quaternion origin;
    private bool originated = false;

    // Start is called before the first frame update
    void Start()
    {
        q = new float[4];
        f_raw_data = new float[5];
    }

    // Update is called once per frame
    void Update()
    {
        string income = dataInput.getData();
        if (income != "") raw_data.text = income;
        if (income != "")
        {
            string[] data = income.Split(' ');

            if (data.Length == 5)
            {
                //Parsing all values into floats
                for(int i = 0; i < 5; i++)
                {
                    if (float.TryParse(data[i], out f_raw_data[i])) continue;
                    else continue;

                }
                //Displacement values
                Vector3 delta = new Vector3(-f_raw_data[4], f_raw_data[3], 0);

                //Quaternion for ratation
                for (int i = 0; i < 3; i++) q[i + 1] = f_raw_data[i];
                if(1 - Mathf.Pow(q[1], 2) - Mathf.Pow(q[2], 2) - Mathf.Pow(q[3], 2) > 0 && Mathf.Abs(q[1])<1 && Mathf.Abs(q[2]) < 1 && Mathf.Abs(q[3]) < 1)
                {
                    q[0] = Mathf.Sqrt(1 - Mathf.Pow(q[1], 2) - Mathf.Pow(q[2], 2) - Mathf.Pow(q[3], 2));

                    

                    //raw_data.text = "" + q[0] + " " + q[1] + " " + q[2] + " " + q[3];
                    Quaternion rot = new Quaternion(q[2], -q[1], -q[3], -q[0]);
                    //rot = rot * Quaternion.EulerRotation(0, 0, -45);
                    float angle = Quaternion.Angle((origin * rot), rig.rotation);

                    //raw_data.text = "" + angle+" "+rot;
                    if (!originated)
                    {
                        originated = true;
                        
                        origin = Quaternion.Inverse(rot);
                        //rotate_tester.rotation = rot *origin;
                    }
                    else if (angle < 20)
                    {
                        //rotate_tester.rotation = origin * rot;
                        rig.rotation = (origin * rot);
                    }
                    
                }

                cam.transform.localRotation = Quaternion.Euler(0, 0, rig.localRotation.eulerAngles.z);
                move_smooth(delta);
                //raw_data.text = "";
            }
            else if (income == "")
            {
                raw_data.text = "";
            }
        }
        
    }

    void move_smooth(Vector3 delta_distance)
    {
        StartCoroutine("moveSmooth", delta_distance * gain);

    }

    IEnumerator moveSmooth(Vector3 d)
    {
        for(int i = 0; i < 3; i++)
        {
            cam.transform.position -= d / 3f;
            yield return new WaitForSeconds(0.02f/3f);
        }

    }
}
