using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class brain_scene_control : MonoBehaviour
{
    public GameObject brain;
    public GameObject[] sub_brain; //2:watching
    public Button[] stimulate_btn;
    public Toggle ear_btn;

    public GameObject guide_box;

    private bool isListening = false;
    private string frontal_guide = "�̷��� ���� ���ϴ� ���� ���� ��� �κ��� ������ �� ���� ���ô�.";
    private string side_guide = "�Ҹ��� ���� �ڱؿ� ���� ��� �κ��� �����ϴ� �� ���� ���ô�.";
    private string back_guide = "����� ���� �� �κп� �ش��ϴ� �ĵο��� �ð��� ������ �ֽ��ϴ�.";
    private string touch_guide = "��ư�� ������, �������� �ڱؿ� ���� ��� �κ��� �������� ���� ���ô�.";
    private float duration = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        string RFID = address.GetLastRFID();
        sub_brain[3].GetComponent<glowmanager_p>().SetGlow();

        //if (RFID=="4B1C20AD")
        if (RFID == "04387B9A")
        {
            //sub_brain[0].GetComponent<glowmanager_r>().SetGlow();
        }
        //if (RFID=="FD2F31F5")
        if (RFID == "043C7B9A")
        {
            brain.transform.Rotate(new Vector3(0, 90, 0));
            //sub_brain[1].GetComponent<glowmanager_g>().SetGlow();
        }

        if (RFID == "2B0534AD")
        {
            brain.transform.Rotate(new Vector3(0, 180, 0));
            //sub_brain[2].GetComponent<glowmanager_b>().SetGlow();
        }
    }

        // Update is called once per frame
    void Update()
    {
        if (isListening) ear_btn.image.color = new Color(250f / 255f, 171 / 255f, 0f);
        else ear_btn.image.color = Color.white;
    }

    public void activate_sub_brain(int index)
    {
        switch (index)
        {
            case 0:
                sub_brain[0].GetComponent<glowmanager_r>().SetGlow();
                break;
            case 1:
                sub_brain[1].GetComponent<glowmanager_g>().SetGlow();
                break;
            case 2:
                sub_brain[2].GetComponent<glowmanager_b>().SetGlow();
                break;
        }

    }

    public void touching_skin()
    {
        //Debug.Log("touching...");
        Vector3 delta = Input.GetTouch(0).deltaPosition;

        sub_brain[3].GetComponent<glowmanager_y>().set_intensity(delta.magnitude * 0.1f);


    }

    public void listening()
    {
        isListening = !isListening;
        //Debug.Log(isListening);
        if (isListening) activate_sub_brain(1);
        else quit_glowing(1);
    }

    public void quit_glowing(int index)
    {
        Debug.Log("Quit!!!");
        switch (index)
        {
            case 0:
                sub_brain[0].GetComponent<glowmanager_r>().SetGlow(false);
                break;
            case 1:
                sub_brain[1].GetComponent<glowmanager_g>().SetGlow(false);
                break;
            case 2:
                sub_brain[2].GetComponent<glowmanager_b>().SetGlow(false);
                break;
            case 3:
                sub_brain[3].GetComponent<glowmanager_y>().SetGlow(false);
                break;
        }
    }
    public void pop_up_guide(int index)
    {
        switch (index)
        {
            case 0:
                guide_box.transform.GetChild(0).gameObject.GetComponent<Text>().text = side_guide;
                break;
            case 1:
                guide_box.transform.GetChild(0).gameObject.GetComponent<Text>().text = back_guide;
                break;
            case 2:
                guide_box.transform.GetChild(0).gameObject.GetComponent<Text>().text = frontal_guide;
                break;
            case 3:
                guide_box.transform.GetChild(0).gameObject.GetComponent<Text>().text = touch_guide;
                break;
        }

        if (index == 0 && !isListening) return;

        if(!guide_box.active) StartCoroutine("pop_up", duration);
        else
        {
            StopCoroutine("pop_up");
            StartCoroutine("pop_up", duration);
        }
    }

    IEnumerator pop_up(float duration)
    {
        guide_box.SetActive(true);
        yield return new WaitForSeconds(duration);
        guide_box.SetActive(false);
    }

    public void watching()
    {
        StartCoroutine("watching_something");
    }

    IEnumerator watching_something()
    {

        sub_brain[2].GetComponent<glowmanager_b>().SetGlow(true);
        yield return new WaitForSeconds(duration);

        sub_brain[2].GetComponent<glowmanager_b>().SetGlow(false);
    }

    public void speaking()
    {
        StartCoroutine("speaking_something");
    }

    IEnumerator speaking_something()
    {
        sub_brain[0].GetComponent<glowmanager_r>().SetGlow(true);
        yield return new WaitForSeconds(duration);

        sub_brain[0].GetComponent<glowmanager_r>().SetGlow(false);
    }
    

}
