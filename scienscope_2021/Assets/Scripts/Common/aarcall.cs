using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class aarcall : MonoBehaviour
{
    public Text deb; //디버깅
    private AndroidJavaObject javaClassInstance;
    AndroidJavaClass jc;

    //버튼UI
    //public GameObject plugged;
    //public GameObject unplugged;

    public GameObject Btn1;
    public Sprite newsprite;

    private AndroidJavaObject plugin;
    private bool listening = false; //연결상태판단

    public readonly Dictionary<string, string> BTAdresses = new Dictionary<string, string>
    {
        // Order does not matter
        // { DEVICE_ID, BT_ADDRESS },
        { "52b462b3e6155528701ef574f80567cd", "C0:71:93:E7:4E:9C" }, // Labeled "1"
        { "02bd87c91839f460f47aab7416e79bb0", "CF:8E:09:65:A2:2A" }, // Labeled "2"
        { "7f43df7cd3722a08c97180f49c7315ed", "CE:5F:E8:39:1B:7F" }, // Labeled "3"
        { "3f45d020fe1e68e46318065ccfe8c456", "D4:51:6E:11:D5:B9" },
        { "b2f388211244904efb04d657dce3855a", "E0:3F:F8:C2:E6:0E" },
        { "cceb48d01d5382b20e9a403c2f75ee76", "E0:D9:98:07:E5:26" }, // Labeled "4"
    };


    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        address.BTaddress = ""; // bluetooth address 가져오는 건 수정 예정
        Btn1 = GetComponent<GameObject>();

        if (Application.platform == RuntimePlatform.Android)
        {
            plugin = new AndroidJavaObject("com.beom.myble_v10.MainActivity");

            AndroidJavaClass androidJC = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = androidJC.GetStatic<AndroidJavaObject>("currentActivity");
            jc = new AndroidJavaClass("com.beom.myble_v10.MainActivity");

            if (jc != null)
            {
                javaClassInstance = jc.CallStatic<AndroidJavaObject>("instance");
                javaClassInstance.Call("setContext", jo);
                javaClassInstance.Call("service_init", jo, address.BTaddress);
                listening = true;
            }
        }
        dataInput.initialize(); //datainput class (List 생성)
        Debug.Log("#1.aarcall start");
    }

    public void ClickWhat()
    {
        GameObject clickBtn = EventSystem.current.currentSelectedGameObject;
        // Btn1.GetComponent<Image>().sprite = newsprite;
        switch (clickBtn.name)
        {
            case "Btn1":
                address.BTaddress = "C0:71:93:E7:4E:9C";
                deb.text = "1번" + address.BTaddress;
                break;
            case "Btn2":
                address.BTaddress = "CF:8E:09:65:A2:2A";
                deb.text = "2번" + address.BTaddress;
                break;
            case "Btn3":
                address.BTaddress = "CE:5F:E8:39:1B:7F";
                deb.text = "3번" + address.BTaddress;
                break;
        }
        connect();

    }

    public void connect()
    {
        if (Application.platform != RuntimePlatform.Android) return; //안드로이드 플랫폼 아닐시 반환
        AndroidJavaClass androidJC = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = androidJC.GetStatic<AndroidJavaObject>("currentActivity");
        if (jc != null)
        {
            javaClassInstance = jc.CallStatic<AndroidJavaObject>("instance");
            javaClassInstance.Call("setContext", jo);
            javaClassInstance.Call("reconnect", jo, address.BTaddress);
            listening = true;
        }

        if (listening == true)
        {
            //deb.text = "연결에 성공했습니다!\n 전시물에서 태그를 찾아보세요";
        }
        else
        {
            //deb.text = "연결에 실패했습니다!\n 청진기 번호를 다시 확인해주세요";
        }
        //unplugged.SetActive(false);
        //plugged.SetActive(true);

        Debug.Log("#1.connected");
    }


    public void disconnect()
    {
        if (Application.platform != RuntimePlatform.Android) return;
        AndroidJavaClass androidJC = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = androidJC.GetStatic<AndroidJavaObject>("currentActivity");
        if (jc != null)
        {
            javaClassInstance = jc.CallStatic<AndroidJavaObject>("instance");
            javaClassInstance.Call("setContext", jo);
            javaClassInstance.Call("service_end");
            listening = false;
        }

        //unplugged.SetActive(true);
        //plugged.SetActive(false);

        Debug.Log("#1.disconnected");

    }


    // Update is called once per frame
    void Update()
    {
        if (listening && Application.platform == RuntimePlatform.Android) //연결되있고, 안드로이드 플랫폼이면,
        {
            string tmp;
            tmp = javaClassInstance.Call<string>("getData");
            if (tmp.Split(':')[0] == "R")
            {
                deb.text = "태그인식완료";
            }

            dataInput.data_in.Add(tmp); //List에 data입력 시작
            Debug.Log("#1.Data input start");
        }
    }

    public void makeToastMessage(string message)
    {
        if (Application.platform != RuntimePlatform.Android) return;
        AndroidJavaClass androidJC = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = androidJC.GetStatic<AndroidJavaObject>("currentActivity");
        javaClassInstance.Call("makeToastMessage", jo, message);
    }

    // Vibration 
    public void vibrate_phone()
    {
        if (Application.platform != RuntimePlatform.Android) return;
        AndroidJavaClass androidJC = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = androidJC.GetStatic<AndroidJavaObject>("currentActivity");

        javaClassInstance.Call("vibrate", jo);
    }

    public void vibrate_phone(long[] pattern)
    {
        if (Application.platform != RuntimePlatform.Android) return;
        AndroidJavaClass androidJC = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = androidJC.GetStatic<AndroidJavaObject>("currentActivity");

        javaClassInstance.Call("vibrate", jo, pattern);
    }

    public bool setListening(bool _listening)
    {
        listening = _listening;
        return listening;
    }

}
