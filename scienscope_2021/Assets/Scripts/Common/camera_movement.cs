using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class camera_movement : MonoBehaviour
{
    public float gain;

    public Text raw_data; //debugging text, monitoring raw data from module
    private float[] f_raw_data; //parsed raw-data
    public Camera cam;
    public Transform rig;
    public bool use_gravity; // checking for calibrating by gravity from mobile device data

    //public Transform rotate_tester;

    private float[] q; //Quaternion container (temporal)
    private Quaternion origin;
    private bool originated = false;
    private int reset_count = 0;
    private int goback_count = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(use_gravity) Input.gyro.enabled = true;

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

                    float angle = Quaternion.Angle((origin * rot), rig.rotation);

                    //raw_data.text = "" + angle+" "+rot;
                    if (!originated && !use_gravity)
                    {
                        originated = true;
                        
                        origin = Quaternion.Inverse(rot);
                        //rotate_tester.rotation = rot *origin;
                    }else if (use_gravity)
                    {
                        ///Gravity Indicator, Rotation///
                        Vector3 gdir = Input.gyro.gravity;
                        origin = Quaternion.EulerAngles(0, 0, -Mathf.Atan(gdir.y / gdir.x));
                        
                        if (gdir.x > 0) origin = origin * Quaternion.Euler(0, 0, -90);
                        else origin = origin * Quaternion.Euler(0, 0, 90);
                        Debug.Log(origin);

                        origin = origin * Quaternion.Inverse(rot);
                    }

                    if (angle < 20)
                    {
                        //rotate_tester.rotation = origin * rot;
                        rig.rotation = (origin * rot);
                    }else if (angle >= 20)
                    {
                        reset_count++;
                    }

                    if(reset_count > 50)
                    {
                        rig.rotation = (origin * rot);
                        reset_count = 0;
                    }
                    
                }
                
                rotate_smooth(new Vector3(0, 0, rig.localEulerAngles.z));
               
                delta = cam.transform.localRotation * delta;
                move_smooth(delta);
            }else if (data.Length <= 2)
            {
                int distance = int.Parse(data[0]);

                if (distance > 100) SceneManager.LoadScene(0, LoadSceneMode.Single);
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

    void rotate_smooth(Vector3 delta_angle)
    {
        StartCoroutine("rotateSmooth", delta_angle);
        
    }

    IEnumerator moveSmooth(Vector3 d)
    {
        for(int i = 0; i < 3; i++)
        {
            cam.transform.position -= d / 3f;
            yield return new WaitForSeconds(0.02f/3f);
        }
    }

    IEnumerator rotateSmooth(Vector3 d)
    {
        Quaternion start = cam.transform.localRotation;
        Quaternion end = Quaternion.Euler(0, 0, d.z);
        
        for (int i = 0; i < 3; i++)
        {
            //cam.transform.localRotation = cam.transform.localRotation * Quaternion.Euler(0, 0, d.z / 3f);
            cam.transform.localRotation = Quaternion.Slerp(start, end, (float)(1f / 3f * (i+1)));
            //Debug.Log(end);
            yield return new WaitForSeconds(0.01f);
        }
        //cam.transform.localEulerAngles = new Vector3(0,0,rig.localEulerAngles.z);
    }
}
