using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataInput : MonoBehaviour
{
    public static List<string> data_in;

    public static void initialize()
    {
        data_in = new List<string>();
    }

    public static string getData()
    {
        string tmp = "";

        if (data_in.Count > 0)
        {
            tmp = data_in[0];
            data_in.RemoveAt(0);
        }

        return tmp;
    }

}
