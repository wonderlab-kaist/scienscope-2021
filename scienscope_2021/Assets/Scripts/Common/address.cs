using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class address
{
    public static string BTaddress = "";
    private static string lastRFID = "";
    //public static GameObject aimed_object;
    


    public static Dictionary<string, int[]> RFID_numbers = new Dictionary<string, int[]>
    {   //Dictionary 자료구조. 태그값

        // Order does not matter
        {"10006E69F8EF", new int[]{ 2, 1 } },
        {"10006E605A44", new int[]{ 2, 2 } },
        {"10006E600E10", new int[]{ 2, 3 } },
        {"10006E682731", new int[]{ 2, 4 } },
        {"5700A4B75C18", new int[]{ 3, 0 } },
        {"5700A4E9A9B3", new int[]{ 3, 1 } },
        {"5700A4AC401F", new int[]{ 3, 2 } },
        {"5700A4C11725", new int[]{ 3, 3 } },
        {"5700A50714E1", new int[]{ 3, 4 } },
        {"5700A4BAE7AE", new int[]{ 4, 0 } },
        {"5700A4C09EAD", new int[]{ 4, 1 } },
        {"5700A4D61336", new int[]{ 4, 2 } },

        {"5700A48DDCA2", new int[]{ 2, 1 } },
        {"5700A4B50244", new int[]{ 2, 2 } },
        {"5700A48A631A", new int[]{ 2, 3 } },
        {"5700A4BCA2ED", new int[]{ 2, 4 } },
        {"5700A4D40027", new int[]{ 3, 0 } },
        {"5700A4E6BEAB", new int[]{ 3, 1 } },
        {"5700A504DE28", new int[]{ 3, 2 } },
        {"5700A4D3AB8B", new int[]{ 3, 3 } },
        {"5700A4915D3F", new int[]{ 3, 4 } },
        {"5700A4D8FFD4", new int[]{ 4, 0 } },
        {"5700A4B982C8", new int[]{ 4, 1 } },
        {"5700A4AA2178", new int[]{ 4, 2 } },
    };
    
    public static void SetLastRFID(string rfid)
    {
        lastRFID = rfid;
    }

    public static string GetLastRFID()
    {
        return lastRFID;
    }

    public static int GetCurrentSceneNumber() //현재 씬넘버
    {
        
        if (!RFID_numbers.ContainsKey(lastRFID))
        {
            return -1;
        }
        return RFID_numbers[lastRFID][0];
    }

    public static int GetCurrentSubSceneNumber() //현재 서브 씬넘버
    {
        if (!RFID_numbers.ContainsKey(lastRFID))
        {
            return -1;
        }
        return RFID_numbers[lastRFID][1];
    }

    /// <summary>
    /// volcano data for under the sea scene 
    /// total number : the number of volcanos
    /// state_of_vol : arrays for each volcanos activation (1~60, 60:max, activated)
    /// reset_all : reset the all status
    /// </summary>
    public static class volcanos
    {
        public static int total_number = 42;
        //public static int activated_vol = 0;
        public static int[] state_of_vol;
        
        public static void initialize()
        {
            state_of_vol = new int[total_number];
        }

        public static int activated_vol()
        {
            int tmp = 0;
            for(int i=0;i<total_number;i++)
            {
                if (state_of_vol[i] >= 50) tmp++; 
            }

            return tmp;
        }

        public static void reset_all()
        {
            GameObject.Find("BLEcontroller").GetComponent<aarcall>().makeToastMessage("모든 활동을 리셋했습니다.");
            initialize();
        }
    }


    /// Vibration pattern
    /// long[] sets for smartphone vibration pattern
    /// 
    public static long[][] vib_patterns = new long[][]
    {
        new long[] { 0,200,20, 200, 20, 200, 20, 200, 20, 200, 20},
        new long[] { 0,80, 80, 80, 80, 80, 80, 80, 80, 80 }
    };

    
}
