using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam_lookat : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform campoint;
    

    void Start()
    {
        campoint = GameObject.FindGameObjectWithTag("brain_point").transform; //
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(campoint);
    }
}
