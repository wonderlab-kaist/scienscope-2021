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


    
    void Start()
    {
        address.SetLastRFID("");
    }

    // Update is called once per frame
    void Update()
    {
        stethoscope_data tmp = new stethoscope_data(dataInput.getData());
        //string tmp = dataInput.getData();
        //Debug.Log(address.BTaddress);

        if (tmp!=null&&Application.platform == RuntimePlatform.Android)
        {
            heading.text = "rea";
            //Debug.Log(tmp);
            if (!(tmp.tag_id[0] == 0&& tmp.tag_id[1] == 0&& tmp.tag_id[2] == 0&& tmp.tag_id[3] == 0))
            {
                explain.text = "잠시만 기다려주세요...";
                Debug.Log(tmp);
                /// move on contents scenes ///
                SceneManager.LoadSceneAsync("DemoScene", LoadSceneMode.Single); ///i값을 원하는 scene의 build index로 대체
                    
                
            }
        }

        /*int target_scene_num = address.GetCurrentSceneNumber();//현재씬번호 받아오기
        if (target_scene_num != -1) //에러값이 아니면
        {
            heading.text = "전시물 안으로 가는중";
            explain.text = "잠시만 기다려주세요...";
            //씬전환
            
        }*/
    }
}
