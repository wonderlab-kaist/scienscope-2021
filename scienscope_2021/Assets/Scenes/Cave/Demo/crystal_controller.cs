using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crystal_controller : MonoBehaviour
{
    public GameObject Cam;
    public GameObject cystal1;
    public GameObject cystal2;
    private float distance;

    // Update is called once per frame
    void Update()
    {
        VisibleCry(cystal1, 20);
        VisibleCry(cystal2, 68);
    }

    public void VisibleCry(GameObject crys, float limit)
    {
        distance = Vector3.Distance(crys.transform.position, Cam.transform.position);


        if (distance <= limit)
        {
            crys.SetActive(true);
        }
    }
}
