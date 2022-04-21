using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CamTransition_Controller))]
public class CanvasScript : MonoBehaviour {


    public Slider speedSlider;
    public Slider MaxBlurSlider;

    public CamTransition_Controller CTC;

	// Use this for initialization
	void Start () {
        CTC = GetComponent<CamTransition_Controller>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        CTC.animSpeed = speedSlider.value;
        CTC.maxBlur = (int)MaxBlurSlider.value;
	}
}
