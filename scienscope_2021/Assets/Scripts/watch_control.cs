using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class watch_control : MonoBehaviour
{
    public aarcall BLEcontroller;

    private bool scene_detected = false;

    // Start is called before the first frame update
    void Start()
    {
        Screen.sleepTimeout = 0;

    }

    // Update is called once per frame
    void Update()
    {
        stethoscope_data tmp = new stethoscope_data(dataInput.getData());

        if (tmp != null && Application.platform == RuntimePlatform.Android)
        {
            //heading.text = "rea";
            //Debug.Log(tmp);
            //scienscope_illust.transform.position = sc_illust_origin + new Vector3((255 - tmp.distance) * 0.00125f, 0, 0);


            if (!(tmp.tag_id[0] == 0 && tmp.tag_id[1] == 0 && tmp.tag_id[2] == 0 && tmp.tag_id[3] == 0) && !scene_detected)
            {
                //Instantiate(ps_effect, ps_origin).transform.localPosition = Vector3.zero;

                //explain.text = "잠시만 기다려주세요...";
                Debug.Log(System.BitConverter.ToString(tmp.tag_id).Replace("-", ""));
                string id = System.BitConverter.ToString(tmp.tag_id).Replace("-", "");
                address.SetLastRFID(id); //save RFID Address for load in next scene

                /// move on contents scenes ///
                scene_detected = true;
                SceneManager.LoadSceneAsync(1, LoadSceneMode.Single); ///i값을 원하는 scene의 build index로 대체


            }
        }

    }
}
