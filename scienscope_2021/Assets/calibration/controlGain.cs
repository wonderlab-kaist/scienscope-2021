using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controlGain : MonoBehaviour
{
    public Text displayGain;
    public Text displayCoeff;
    public float initGain;
    public float currGain;

    public static float coeff = 1;

    public static void increaseCoeff()
    {
        coeff += 0.05f;
    }

    public static void decreaseCoeff()
    {
        coeff -= 0.05f;
    }

    public static void resetCoeff()
    {
        coeff = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        camera_movement call = GameObject.Find("scene_control").GetComponent<camera_movement>();
        initGain = call.gain;
        currGain = initGain;
    }

    public void setGain(float updateGain)
    {
        camera_movement call = GameObject.Find("scene_control").GetComponent<camera_movement>();
        call.gain = updateGain;
    }

    // Update is called once per frame
    void Update()
    {
        displayCoeff.text = string.Format("{0:0.00}", coeff);

        currGain = initGain * coeff;
        setGain(currGain);
        displayGain.text = "Gain Value : " + currGain.ToString();
    }
}
