using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class aarcall : MonoBehaviour
{
    public Text deb;
    private AndroidJavaObject javaClassInstance;
    AndroidJavaClass jc;

    public GameObject plugged;
    public GameObject unplugged;

    private AndroidJavaObject plugin;
    private bool listening = false;
    //public GameObject cursor;
    

    void Start()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            plugin = new AndroidJavaObject("com.beom.myble_v10.MainActivity");
            deb.text += plugin.CallStatic<string>("UnityCall", "CE:5F:E8:39:1B:7F");
            deb.text += "\n" + address.BTaddress;

            AndroidJavaClass androidJC = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = androidJC.GetStatic<AndroidJavaObject>("currentActivity");
            jc = new AndroidJavaClass("com.beom.myble_v10.MainActivity");

            if (jc != null)
            {
                //deb.text = jc.ToString();
                javaClassInstance = jc.CallStatic<AndroidJavaObject>("instance");
                javaClassInstance.Call("setContext", jo);
                javaClassInstance.Call("service_init", jo, address.BTaddress);
                listening = true;
            }
        }
        
        dataInput.initialize();
        //javaClassInstance.Call("makeToastMessage", jo, "Test it worked?");
        Debug.Log("Method1 done");
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
            javaClassInstance.Call("reconnect", jo, address.BTaddress);
            listening = true;
        }

        unplugged.SetActive(false);
        plugged.SetActive(true);
    }
    public void disconnect()
    {
        if (Application.platform != RuntimePlatform.Android) return;
        AndroidJavaClass androidJC = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = androidJC.GetStatic<AndroidJavaObject>("currentActivity");
        if (jc != null)
        {
            //deb.text = jc.ToString();
            javaClassInstance = jc.CallStatic<AndroidJavaObject>("instance");
            javaClassInstance.Call("setContext", jo);
            javaClassInstance.Call("service_end");
            
            
            listening = false;
        }

        unplugged.SetActive(true);
        plugged.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if (listening && Application.platform == RuntimePlatform.Android)
        {
            string tmp;

            tmp = javaClassInstance.Call<string>("getData");
            //deb.text = tmp;

            dataInput.data_in.Add(tmp);
            
        }

        /*if (Input.touchCount == 2)
        {
            vibrate_phone(address.vib_patterns[0]);
        }else if(Input.touchCount > 2)
        {
            vibrate_phone(address.vib_patterns[1]);
        }*/
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
