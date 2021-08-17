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

        if(tmp.Contains("rfT"))
        {
            return tmp;
        }

        string[] segments = tmp.Split('!');

        /*if (segments.Length > 2)
        {
            for (int i = segments.Length - 1; i > 0; i--) data_in.Insert(0, segments[i]);
            GameObject.Find("Debug_Data (1)").GetComponent<Text>().text = tmp.Split('!')[1];
        }*/
        //Debug.Log(segments.Length);
        return segments[0];
        
        
    }

    public static bool isConnected()
    {
        if (data_in != null && data_in.Count > 0) return true;
        else return false;
    }

}
