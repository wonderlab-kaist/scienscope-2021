using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class watch_control : MonoBehaviour
{
    public aarcall BLEcontroller;


    private bool connected = false;

    // Start is called before the first frame update
    void Start()
    {
        Screen.sleepTimeout = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            if(!connected && dataInput.data_in.Count < 2)
            {
                //BLEcontroller.connect();

            }
            else
            {
                connected = true;

                string tmp = dataInput.getData();
                if(tmp != "") Debug.Log(tmp);

                if (tmp.Contains("3B971EAD")) SceneManager.LoadScene(1, LoadSceneMode.Single);
            }
        }

    }
}
