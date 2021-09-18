using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dataInput : MonoBehaviour
{
    public static List<string> data_in;
    public static string tmp;
    

    public static void initialize()
    {
        data_in = new List<string>();
    }

    public static string getData()
    {
        tmp = "";

        if (data_in.Count > 0)
        {
            
            tmp += data_in[0];
            data_in.RemoveAt(0);
        }else if(data_in.Count==0)
        {
            return null;
        }

        if(tmp.Contains("rfID"))
        {
            //tmp.IndexOf("rfT");
            Debug.Log(tmp);
            if(tmp.Split(':').Length>2 && tmp.Split(':')[2].Length < 8)
            {
                //Debug.Log(tmp.Split('D')[1].Length);
                tmp += data_in[0];
                Debug.Log(tmp);
                data_in.RemoveAt(0);
            }

            return tmp;
        }

        string[] segments = tmp.Split('!');

        return segments[0];
        
    }

    public static bool isConnected()
    {
        if (data_in != null && data_in.Count > 0) return true;
        else return false;
    }

}
