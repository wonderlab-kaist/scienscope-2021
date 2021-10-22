using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIcontrol_Moon : MonoBehaviour
{
    TextMeshProUGUI resourceText;
    

    // Update is called once per frame
    void Update()
    {
        //SetTime();
    }

    //void SetTime()
    //{
    //    if (remainTime <= 0)
    //    {
    //        remainTime = 0;
    //        return;
    //    }

    //    remainTime -= Time.deltaTime;
    //    int seconds = (int)remainTime;

    //    int min = seconds / 60;
    //    int sec = seconds % 60;
    //    int millisec = (int)((remainTime - seconds) * 100);
    //    string text = string.Format("TIME <mspace=18>{0:00}</mspace>:<mspace=18>{1:00}</mspace>:<mspace=18>{2:00}</mspace>", min, sec, millisec);
    //    timeText.text = text;
    //}
}
