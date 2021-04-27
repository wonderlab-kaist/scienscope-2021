﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TAG_loading : MonoBehaviour 
{   //태그 인식되면, 컨텐츠 로딩하는 스크립트

    public Text loading;
    public Text loading2;
    
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
            if (tmp.Split(':')[0] == "R")
            {
                address.SetLastRFID(tmp.Split(':')[1]);

            }
        }

        int target_scene_num = address.GetCurrentSceneNumber();//현재씬번호 받아오기
        if (target_scene_num != -1) //에러값이 아니면
        {
            loading.text = "바닷속 니모와 친구들을 만나볼까요?";
            loading2.text = "잠시만 기다려주세요...";
            //씬전환
            SceneManager.LoadScene("1_play");
        }
    }
}