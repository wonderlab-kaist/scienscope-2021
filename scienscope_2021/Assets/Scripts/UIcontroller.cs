using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIcontroller : MonoBehaviour
{
    public GameObject mainBtn; //connect Btn
    public GameObject mainusBtn;
    public GameObject plusBtn;
    public GameObject headingTxt;
    public GameObject arrow;
    public GameObject gif;
    public GameObject animation;

    public Sprite normalSprite;
    public Sprite errorSprite; 
    public int mainNum; //scienscope number
    public Text mainTxt;
    public Text explainTxt;


    public string[] address_BLE;


    private bool connected = false; //연결상태확인

    void Start()
    {
        if (address.BTaddress != "")
        {
            mainBtn.SetActive(false);
            mainusBtn.SetActive(false);
            plusBtn.SetActive(false);
            arrow.SetActive(false);

            //headingTxt.SetActive(true);
            gif.SetActive(true);
            explainTxt.text = "전시물 내부를 보고 싶다면," + "\n" + "태그를 찾아 핸드폰을 갖다 대보세요";
            GameObject.Find("BLEcontroller").GetComponent<aarcall>().setListening(true);
        }
    }


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
        
       /* switch (mainTxt.GetComponent<Text>().text)
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
                address.BTaddress = "E2:39:AF:10:0A:73";
                connected = true;
                break;
        }*/

        address.BTaddress = address_BLE[int.Parse(mainTxt.GetComponent<Text>().text)];
        connected = true;

        GameObject.Find("BLEcontroller").GetComponent<aarcall>().connect();
        Debug.Log(dataInput.isConnected());

        if (connected) //연결성공
        {
            SceneManager.LoadScene("1_RFID_waiting", LoadSceneMode.Single); ///// Move on to RFID waiting scene

            mainBtn.SetActive(false);
            mainusBtn.SetActive(false);
            plusBtn.SetActive(false);
            arrow.SetActive(false);
            //headingTxt.SetActive(true);
            gif.SetActive(true);
            //animation.SetActive(true);
            explainTxt.text = "전시물에 부착된 태그를 찾아"+"\n"+ "핸드폰을 갖다 대보세요";

        }
        else //연결실패
        {
            mainBtn.GetComponent<Image>().sprite = errorSprite;
            mainTxt.GetComponent<Text>().text = "";
            Invoke("HideImage", 1);
        }
    }

    void HideImage()
    {
        mainBtn.GetComponent<Image>().sprite = normalSprite;
        mainTxt.GetComponent<Text>().text = mainNum.ToString();
    }
}
