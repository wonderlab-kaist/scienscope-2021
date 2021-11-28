using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dataInput : MonoBehaviour
{
    public static List<byte[]> data_in;
    public static byte[] tmp;
    

    public static void initialize()
    {
        data_in = new List<byte[]>();
    }

    public static byte[] getData()
    {
        if (data_in.Count > 0)
        {
            
            tmp = data_in[0];
            data_in.RemoveAt(0);

        }else if(data_in.Count==0)
        {
            return null;
        }

        return tmp;
        
    }

    public static bool isConnected()
    {
        if (data_in != null && data_in.Count > 0) return true;
        else return false;
    }
}
