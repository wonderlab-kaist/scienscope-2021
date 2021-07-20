using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
        if (data_in.Count > 0)
        {
            tmp = "";
            tmp += data_in[0];
            data_in.RemoveAt(0);
        }

        return tmp.Split('!')[0];

        return tmp;
        /*string[] segments = tmp.Split('!');
        

        string segment;

        if (segments.Length >= 2)
        {
            segment = segments[0];
            tmp = "";
            for (int i = 1; i < segments.Length; i++) tmp += segments[i];
            return segment;
        }else
        {
            return "";
        }*/
        
        
    }

}
