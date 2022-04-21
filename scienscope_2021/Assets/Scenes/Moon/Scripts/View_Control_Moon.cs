using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class View_Control_Moon : MonoBehaviour
{
    public Camera mainCam;
    public Camera topCam;


    public int NowCam = 0;

    public void changeView()
    {
        NowCam++;
        if (NowCam >= 2)
        {
            NowCam = 0;
        }
        if(NowCam == 1)
        {
            topCam.depth = 0;
            topCam.rect = new Rect(0, 0, 1, 1);
            mainCam.depth = 1;
            mainCam.rect = new Rect(-0.005f, 0.73f, 0.26f, 0.23f);

        }
        if(NowCam == 0)
        {
            mainCam.depth = 0;
            mainCam.rect = new Rect(0, 0, 1, 1);
            topCam.depth = 1;
            topCam.rect = new Rect(-0.005f, 0.73f, 0.26f, 0.23f);


        }
        

    }
    
}
