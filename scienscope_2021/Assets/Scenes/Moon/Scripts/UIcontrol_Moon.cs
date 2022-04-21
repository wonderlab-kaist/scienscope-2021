 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIcontrol_Moon : MonoBehaviour
{
    public GameObject Cam;
    public GameObject seatxt1;
    public GameObject seatxt2;
    public GameObject apollotxt1;
    public GameObject apollotxt2;
    public GameObject steptxt1;
    public GameObject steptxt2;
    private float distance;

    void Update()
    {
        VisibleTxt(seatxt1, 350);
        VisibleTxt(seatxt2, 350);
        VisibleTxt(apollotxt1 , 200);
        VisibleTxt(apollotxt2 , 200);
        VisibleTxt(steptxt1 , 200);
        VisibleTxt(steptxt2 , 200);
    }

    public void VisibleTxt(GameObject txt, float limit)
    {   
        distance = Vector3.Distance(txt.transform.position, Cam.transform.position);


        if (distance <= limit)
        {
            txt.SetActive(true);
        }
    }
}
