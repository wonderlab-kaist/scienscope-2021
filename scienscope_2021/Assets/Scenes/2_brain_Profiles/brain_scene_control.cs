using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brain_scene_control : MonoBehaviour
{
    public GameObject brain;
    public GameObject[] sub_brain;

    // Start is called before the first frame update
    void Start()
    {
        string RFID = address.GetLastRFID();
        sub_brain[3].GetComponent<glowmanager_p>().SetGlow();

        //if (RFID=="4B1C20AD")
        if (RFID == "04387B9A")
        {
            sub_brain[0].GetComponent<glowmanager_r>().SetGlow();
        }
        //if (RFID=="FD2F31F5")
        if (RFID == "043C7B9A")
        {
            brain.transform.Rotate(new Vector3(0, 90, 0));
            sub_brain[1].GetComponent<glowmanager_g>().SetGlow();
        }

        if (RFID == "2B0534AD")
        {
            brain.transform.Rotate(new Vector3(0, 180, 0));
            sub_brain[2].GetComponent<glowmanager_b>().SetGlow();
        }
    }

        // Update is called once per frame
    void Update()
    {
        
    }

}
