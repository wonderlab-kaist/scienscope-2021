using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LookCam : MonoBehaviour
{
    public GameObject Cam;

    void Update()
    {
        transform.rotation = Cam.transform.rotation;
    }
}
