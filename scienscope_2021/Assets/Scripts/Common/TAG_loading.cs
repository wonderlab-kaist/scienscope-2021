using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TAG_loading : MonoBehaviour 
{   //태그 인식되면, 컨텐츠 로딩하는 스크립트

    public Text heading;
    public Text explain;
    
    void Start()
    {
        address.SetLastRFID("");
    }

    // Update is called once per frame
    void Update()
    {

        string tmp = dataInput.getData();
        if(tmp!=null&&Application.platform == RuntimePlatform.Android)
        {
            heading.text = tmp;
            if (tmp.Contains("rfT")&&tmp.Contains("rfID:B06DE832"))
            {

                //heading.text = "전시물 안으로 가는중";
                explain.text = "잠시만 기다려주세요...";
                
                SceneManager.LoadScene(1, LoadSceneMode.Single);
                address.SetLastRFID(tmp.Split(':')[1]);

            }
        }

        int target_scene_num = address.GetCurrentSceneNumber();//현재씬번호 받아오기
        if (target_scene_num != -1) //에러값이 아니면
        {
            heading.text = "전시물 안으로 가는중";
            explain.text = "잠시만 기다려주세요...";
            //씬전환
            
        }
    }
}
