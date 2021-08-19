using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class aarcall : MonoBehaviour
{
    private AndroidJavaObject javaClassInstance;
    AndroidJavaClass jc;

    public Text deb;
    public string default_ble_address;

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
        { "b2f388211244904efb04d657dce3855a", "E2:39:AF:10:0A:73" }, // Labeled "5"
    };


    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        dataInput.initialize(); //datainput class
        Debug.Log("#1." + address.BTaddress);

        if (default_ble_address != "")
        {
            address.BTaddress = default_ble_address; // Default Bluetooth Address
        }
        Debug.Log("#1---." + address.BTaddress);
        //mainBtn = GetComponent<GameObject>();

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
        Debug.Log("#1.aarcall start");
    }
    

    public void connect()
    {
        if (Application.platform != RuntimePlatform.Android) return; 
        AndroidJavaClass androidJC = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = androidJC.GetStatic<AndroidJavaObject>("currentActivity");
        if (jc != null)
        {
            javaClassInstance = jc.CallStatic<AndroidJavaObject>("instance");
            javaClassInstance.Call("setContext", jo);
            javaClassInstance.Call<bool>("reconnect", jo, address.BTaddress);
            listening = true;
        }

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
    }

    // Update is called once per frame
    void Update()
    {
        if (listening && Application.platform == RuntimePlatform.Android) 
        {
            string tmp;
            tmp = javaClassInstance.Call<string>("getData");
            //Debug.Log(tmp);
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
