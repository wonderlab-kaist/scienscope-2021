using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GetRFIDAddress : MonoBehaviour
{
    public GameObject cam1;
    public GameObject cam2;
    public GameObject cam3;

    public GameObject brain1;
    public GameObject brain2;
    public GameObject brain3;

    public Canvas canvas;

    private string RFID;


    //public void GetRFIDScene2(){
    void Start()
    {
        brain2.GetComponent<glowmanager>().SetGlow();
        RFID = SaveRFIDAddress.id;

        if (RFID=="4B1C20AD")
        {
            // camera1.enabled = true;
            // camera2.enabled= false;
            // camera3.enabled= false;
            cam1.SetActive(true);
            cam2.SetActive(false);
            cam3.SetActive(false);
            canvas.worldCamera = cam1.GetComponent<Camera>();
            brain1.GetComponent<glowmanager>().SetGlow();
            
        }

        if (RFID=="FD2F31F5")
        {   

            cam2.SetActive(true);
            cam1.SetActive(false);
            cam3.SetActive(false);
            canvas.worldCamera = cam2.GetComponent<Camera>();
            brain2.GetComponent<glowmanager>().SetGlow();
            // camera1.enabled= false;
            // camera3.enabled= false;
            // camera2.enabled = true;
            //anvas.worldCamera = camera2;

            
        }

        if (RFID=="2B0534AD")
        {
            cam3.SetActive(true);
            cam1.SetActive(false);
            cam2.SetActive(false);
            canvas.worldCamera = cam3.GetComponent<Camera>();
            brain3.GetComponent<glowmanager>().SetGlow();
        }

    }
}
