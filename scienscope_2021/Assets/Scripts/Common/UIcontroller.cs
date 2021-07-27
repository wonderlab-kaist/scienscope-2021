using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIcontroller : MonoBehaviour
{
    public GameObject mainBtn; //connect Btn
    public GameObject mainusBtn;
    public GameObject plusBtn;
    public GameObject headingTxt;
    public GameObject arrow;
    public GameObject gif;

    public Sprite normalSprite;
    public Sprite errorSprite; 
    public int mainNum; //scienscope number
    public Text mainTxt;
    public Text explainTxt;



    private bool connected = false; //연결상태확인


    public void ClickWhat() //plus minus Button
    {       
        GameObject clickBtn = EventSystem.current.currentSelectedGameObject;
        switch (clickBtn.name)
        {
            case "btn_minus":
                if (mainNum > 1)
                {
                    mainNum -= 1;
                    mainTxt.GetComponent<Text>().text = mainNum.ToString();
                }
                break;

            case "btn_plus":
                if (mainNum < 9)
                {
                    mainNum += 1;
                    mainTxt.GetComponent<Text>().text = mainNum.ToString();
                }
                break;
        }
    }

    public void mainBtnPush()
    {
        
        switch (mainTxt.GetComponent<Text>().text)
        {
            case "1":
                address.BTaddress = "C0:71:93:E7:4E:9C";
                break;
            case "2":
                address.BTaddress = "CF:8E:09:65:A2:2A";
                break;
            case "3":
                address.BTaddress = "CE:5F:E8:39:1B:7F";
                break;
            case "4":
                address.BTaddress = "E0:D9:98:07:E5:26";
                break;
            case "5":
                address.BTaddress = "E0:3F:F8:C2:E6:0E";
                connected = true;
                break;
        }
        GameObject.Find("BLEcontroller").GetComponent<aarcall>().connect();

        if (connected) //연결성공
        {
            mainBtn.SetActive(false);
            mainusBtn.SetActive(false);
            plusBtn.SetActive(false);
            arrow.SetActive(false);
            headingTxt.SetActive(true);
            gif.SetActive(true);
            explainTxt.text = "전시물 내부를 보고 싶다면,"+"\n"+ "태그를 찾아 핸드폰을 갖다 대보세요";

        }
        else //연결실패
        {
            mainBtn.GetComponent<Image>().sprite = errorSprite;
            mainTxt.GetComponent<Text>().text = "";
            Invoke("HideImage", 2);
        }
    }

    void HideImage()
    {
        mainBtn.GetComponent<Image>().sprite = normalSprite;
        mainTxt.GetComponent<Text>().text = mainNum.ToString();
    }
}
