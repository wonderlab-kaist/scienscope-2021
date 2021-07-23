using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class aarcall : MonoBehaviour
{
    //public Text deb; //디버깅
    private AndroidJavaObject javaClassInstance;
    AndroidJavaClass jc;

    public GameObject mainBtn;
    public Sprite newsprite;
    public int mainNum;
    public Text mainTxt;

    private AndroidJavaObject plugin;
    private bool listening = false; 


    public readonly Dictionary<string, string> BTAdresses = new Dictionary<string, string>
    {
        // Order does not matter
        // { DEVICE_ID, BT_ADDRESS },
        { "52b462b3e6155528701ef574f80567cd", "C0:71:93:E7:4E:9C" }, // Labeled "1"
        { "02bd87c91839f460f47aab7416e79bb0", "CF:8E:09:65:A2:2A" }, // Labeled "2"
        { "7f43df7cd3722a08c97180f49c7315ed", "CE:5F:E8:39:1B:7F" }, // Labeled "3"
        { "3f45d020fe1e68e46318065ccfe8c456", "D4:51:6E:11:D5:B9" },
        { "cceb48d01d5382b20e9a403c2f75ee76", "E0:D9:98:07:E5:26" }, // Labeled "4"
        { "b2f388211244904efb04d657dce3855a", "E0:3F:F8:C2:E6:0E" }, // Labeled "5"
    };


    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        dataInput.initialize(); //datainput class
        address.BTaddress = "E2:39:AF:10:0A:73"; // Default Bluetooth Address
        mainBtn = GetComponent<GameObject>();

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
                //connected = javaClassInstance.Call<bool>("reconnect", jo, address.BTaddress);
                
                listening = true;
            }
        }
        Debug.Log("#1.aarcall start");
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
                //mainBtn.GetComponent<Image>().sprite = newsprite;
                break;

            case "btn_plus":
                mainNum += 1;
                mainTxt.GetComponent<Text>().text = mainNum.ToString();
                break;
        }
    }

    public void connect()
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
                break;
        }

        Debug.Log("블루투스주소 잘 들어갔나?" + address.BTaddress);

        if (Application.platform != RuntimePlatform.Android) return; 
        AndroidJavaClass androidJC = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = androidJC.GetStatic<AndroidJavaObject>("currentActivity");

        bool connected = false;

        if (jc != null)
        {
            javaClassInstance = jc.CallStatic<AndroidJavaObject>("instance");
            javaClassInstance.Call("setContext", jo);
            connected = javaClassInstance.Call<bool>("reconnect", jo, address.BTaddress);
            Debug.Log(connected);
            listening = true;
        }

        
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

        Debug.Log("#1.disconnected");

    }


    // Update is called once per frame
    void Update()
    {
        if (listening && Application.platform == RuntimePlatform.Android) 
        {
            string tmp;
            tmp = javaClassInstance.Call<string>("getData");
            /*if (tmp.Split(':')[0] == "R")
            {
                deb.text = "태그인식완료";
            }*/

            dataInput.data_in.Add(tmp); //List에 data입력 시작
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
