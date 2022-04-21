using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CamTransition_Controller : MonoBehaviour
{

    public Image image;
    public Material blurMaterial;

    public Camera camera1;
    public Camera camera2;

    private BlurRenderer_CamTransition BRCT1;
    private BlurRenderer_CamTransition BRCT2;

    [Range(0,1)]
    public float transition;

    public AnimationCurve blur1;
    public AnimationCurve blur2;

    public float animSpeed = 0.02f;
    public int maxBlur = 50;

    // Use this for initialization
    void Start ()
    {
        image = gameObject.GetComponent<Image>();
        image.material = blurMaterial;
        image.enabled = false;

        BRCT1 = camera1.GetComponent<BlurRenderer_CamTransition>();
        if (BRCT1 == null)
        {
            BRCT1 = BlurRenderer_CamTransition.Create(camera1);
        }
        BRCT1._Blur = BlurRenderer_CamTransition._BlurNum._Blur01;
        BRCT1.UpdateBlur = false;

        BRCT2 = camera2.GetComponent<BlurRenderer_CamTransition>();
        if (BRCT2 == null)
        {
            BRCT2 = BlurRenderer_CamTransition.Create(camera2);
        }
        BRCT2._Blur = BlurRenderer_CamTransition._BlurNum._Blur02;
        BRCT2.UpdateBlur = false;
    }

    public bool playTransition = false;
    public void DoBlurTransition()
    {
        playTransition = true;
    }

    public void setAnimSpeed(float value)
    {
        animSpeed = value;
    }

    public void setMaxBlur(int value)
    {
        maxBlur = value;
    }

    void Update()
    {
        if (playTransition)
        {
            if (transition == 0f)
            {
                //StopCoroutine("Cam2to1");
                StartCoroutine("Cam1to2");
            }
            else if (transition == 1f)
            {
                //StopCoroutine("Cam1to2");
                StartCoroutine("Cam2to1");
            }
            playTransition = false;

            
        }
    }

    private IEnumerator Cam1to2()
    {
        //camera2.depth = -1;
        //camera1.depth = 0;

        //BRCT1.blurScale = 0;
        //BRCT2.blurScale = 0;

        image.enabled = true;

        int i = 0;
        while (transition < 1f)
        {
            if (i % 2 == 0)
            {
                BRCT1.UpdateBlurNow = true;
            }
            else
            {
                BRCT2.UpdateBlurNow = true;
            }
            i++;

            //if (transition < 0.5f)
            //{
            //    BRCT1.UpdateBlurNow = true;
            //}
            //else
            //{
            //    BRCT2.UpdateBlurNow = true;
            //}

            //BRCT1.UpdateBlurNow = true;
            //BRCT2.UpdateBlurNow = true;

            transition += animSpeed;
            transition = Mathf.Clamp(transition, 0f, 1f);

            BRCT1.blurScale = (int)(blur1.Evaluate(transition) * maxBlur);
            BRCT2.blurScale = (int)(blur2.Evaluate(transition) * maxBlur);
            //BRCT1.blurScale = (int)(blur1.Evaluate(transition));
            //BRCT2.blurScale = (int)(blur2.Evaluate(transition));
            blurMaterial.SetFloat("_BlurTransition", transition);

            //print(transition);
            print("transition: " + transition.ToString());


            yield return null;
        }
        //blurMaterial.SetFloat("_BlurTransition", transition);
        image.enabled = false;
        camera2.depth = 0;
        camera1.depth = -1;

    }

    private IEnumerator Cam2to1()
    {
        //camera2.depth = 0;
        //camera1.depth = -1;

        //BRCT1.blurScale = 0;
        //BRCT2.blurScale = 0;

        image.enabled = true;

        int i = 0;
        while (transition > 0f)
        {
            if (i % 2 == 0)
            {
                BRCT2.UpdateBlurNow = true;
            }
            else
            {
                BRCT1.UpdateBlurNow = true;
            }
            i++;

            //if (transition < 0.5f)
            //{
            //    BRCT1.UpdateBlurNow = true;
            //}
            //else
            //{
            //    BRCT2.UpdateBlurNow = true;
            //}

            //BRCT2.UpdateBlurNow = true;
            //BRCT1.UpdateBlurNow = true;

            transition -= animSpeed;
            transition = Mathf.Clamp(transition, 0f, 1f);

            BRCT1.blurScale = (int)(blur1.Evaluate(transition) * maxBlur);
            BRCT2.blurScale = (int)(blur2.Evaluate(transition) * maxBlur);
            //BRCT1.blurScale = (int)(blur1.Evaluate(transition));
            //BRCT2.blurScale = (int)(blur2.Evaluate(transition));
            blurMaterial.SetFloat("_BlurTransition", transition);

            print("transition: " + transition.ToString());

            yield return null;
        }
        //blurMaterial.SetFloat("_BlurTransition", transition);
        image.enabled = false;
        camera2.depth = -1;
        camera1.depth = 0;


    }
}
