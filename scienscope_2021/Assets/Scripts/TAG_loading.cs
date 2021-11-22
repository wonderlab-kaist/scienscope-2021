using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TAG_loading : MonoBehaviour 
{   //태그 인식되면, 컨텐츠 로딩하는 스크립트

    /// <summary>
    /// Inspector 창에서 RFID_address에 원하는 RFID 태그의 주소값을 입력해주세요.
    /// 해당 RFID 태그 주소의 index + 1의 scene id로 자동 대응되어 넘어갑니다.
    /// 혹은 아래 update 구분의 아래에, 직접 tag id와 scene builid index를 대응시켜 주세요.
    ///
    /// </summary>
    public Text heading;
    public Text explain;

    public string[] RFID_address;

    public GameObject scienscope_illust;
    public ParticleSystem ps_effect;
    public Transform ps_origin;

    private bool scene_detected = false;
    private Vector3 sc_illust_origin;
    
    void Start()
    {
        address.SetLastRFID("");
        sc_illust_origin = scienscope_illust.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        stethoscope_data tmp = new stethoscope_data(dataInput.getData());

        if (tmp!=null&&Application.platform == RuntimePlatform.Android)
        {
            //heading.text = "rea";
            //Debug.Log(tmp);
            scienscope_illust.transform.position = sc_illust_origin + new Vector3((255-tmp.distance)*0.00125f, 0, 0);


            if (!(tmp.tag_id[0] == 0&& tmp.tag_id[1] == 0&& tmp.tag_id[2] == 0&& tmp.tag_id[3] == 0) && !scene_detected)
            {
                Instantiate(ps_effect, ps_origin).transform.localPosition = Vector3.zero;

                explain.text = "잠시만 기다려주세요...";
                Debug.Log(System.BitConverter.ToString(tmp.tag_id).Replace("-",""));
                string id = System.BitConverter.ToString(tmp.tag_id).Replace("-", "");
                address.SetLastRFID(id); //save RFID Address for load in next scene

                /// move on contents scenes ///
                scene_detected = true;
                SceneManager.LoadSceneAsync(1, LoadSceneMode.Single); ///i값을 원하는 scene의 build index로 대체
                
                
            }
        }

        
    }
}
