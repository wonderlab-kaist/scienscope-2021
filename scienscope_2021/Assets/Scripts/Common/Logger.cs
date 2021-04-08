using System;
using System.IO;
using UnityEngine;

public class Logger
{
    static StreamWriter currentWriter = null;
    static int numWrittenLines = 0;


    public Logger()
    {
    }


    public static void StartNew()
    {
        if (currentWriter != null)
        {
            if (numWrittenLines == 0)
            {
                // Previous log is empty, so keep using it
                return;
            }
            else
            {
                currentWriter.Close();
            }
        }

        int fileIndex = 0;
        for (; fileIndex < 100; fileIndex++)
        {
            if (!File.Exists(GetFilePath(fileIndex)))
            {
                break;
            }
        }
        if (fileIndex == 100)
        {
            currentWriter = null;
            numWrittenLines = 0;
            return;
        }

        currentWriter = File.CreateText(GetFilePath(fileIndex));
        numWrittenLines = 0;
        Debug.Log(currentWriter);
    }


    public static void WriteLine(String[] strArr)
    {
        String line = String.Join(",", strArr);
        currentWriter.WriteLine(line);
    }


    public static void Finish()
    {
        Debug.Log(currentWriter);
        if (currentWriter != null)
        {
            currentWriter.Close();
        }
    }


    private static string GetFilePath (int index)
    {
        string fileName = String.Format("log_{0}_{1}.csv", DateTime.Now.ToString("MMdd"), index.ToString("D2"));
        return System.IO.Path.Combine(Application.persistentDataPath, fileName);
    }
}
