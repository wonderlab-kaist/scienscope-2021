using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Find_nemo : MonoBehaviour
{
    public Transform displayPart;
    public Transform[] anchor;

    public GameObject bgImage; //배경이미지


    // Start is called before the first frame update
    void Start()
    {
        if (address.GetCurrentSceneNumber() > 0)
        {
            bgImage.transform.position += displayPart.position - anchor[address.GetCurrentSceneNumber() - 1].position;
        }

        Input.gyro.enabled = true; //기기 gyro 기능활성화
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
