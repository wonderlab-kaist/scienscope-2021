using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rfid_monitor : MonoBehaviour
{
    public Text monitor;


    private stethoscope_data data;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        stethoscope_data tmp = new stethoscope_data(dataInput.getData());
        show_quat_data(tmp);

        if (tmp != null && Application.platform == RuntimePlatform.Android)
        {
            //Debug.Log(tmp);
            if (!(tmp.tag_id[0] == 0 && tmp.tag_id[1] == 0 && tmp.tag_id[2] == 0 && tmp.tag_id[3] == 0))
            {
                Debug.Log(System.BitConverter.ToString(tmp.tag_id).Replace("-", ""));
                monitor.text = System.BitConverter.ToString(tmp.tag_id).Replace("-", "");
                //Debug.Log(tmp.tag_id[0].ToString('X2'));
                /// move on contents scenes ///
                //scene_detected = true;
                //SceneManager.LoadSceneAsync(1, LoadSceneMode.Single); ///i값을 원하는 scene의 build index로 대체


            }
        }
    }

    public void show_quat_data(stethoscope_data data)
    {
        string quat;
        quat = "" + data.q[0]/ 1073741824f + "  " +data.q[1]/ 1073741824f + "  "+ data.q[2]/ 1073741824f;
        Debug.Log(quat);
    }
}
