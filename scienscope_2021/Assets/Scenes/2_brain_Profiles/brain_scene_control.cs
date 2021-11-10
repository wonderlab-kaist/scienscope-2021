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
    public AudioSource guide_audio;
    public AudioClip[] guide_sounds;
    public Toggle ear_btn;

    public GameObject guide_box;

    private int scene_sub_numb; // 0:front 1:side 2:back 3:high_side

    private bool isListening = false;
    private string frontal_guide = "이렇게 제가 말하는 동안 뇌의 어느 부분이 빛나는 지 살펴 봅시다.";
    private string side_guide = "소리를 내어 자극에 뇌의 어느 부분이 반응하는 지 살펴 봅시다.";
    private string back_guide = "대뇌의 가장 뒷 부분에 해당하는 후두엽은 시각과 관련이 있습니다.";
    private string touch_guide = "버튼을 문질러, 간지러운 자극에 뇌의 어느 부분이 빛나는지 살펴 봅시다.";

    private float duration = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        string RFID = address.GetLastRFID();
        for (int i = 0; i < stimulate_btn.Length; i++) stimulate_btn[i].interactable = false;
        ear_btn.interactable = false;

        //if (RFID=="4B1C20AD")
        if (RFID == "04587B9A")
        {
            //sub_brain[0].GetComponent<glowmanager_r>().SetGlow();
            stimulate_btn[2].interactable = true;
        }
        //if (RFID=="FD2F31F5")
        if (RFID == "04547B9A")
        {
            brain.transform.Rotate(new Vector3(0, 90, 0));
            //sub_brain[1].GetComponent<glowmanager_g>().SetGlow();
            ear_btn.interactable = true;
        }

        if (RFID == "04507B9A")
        {
            brain.transform.Rotate(new Vector3(0, 180, 0));
            //sub_brain[2].GetComponent<glowmanager_b>().SetGlow();
            stimulate_btn[0].interactable = true;
        }

        if (RFID == "04407B9A")
        {
            brain.transform.Rotate(new Vector3(0, 270, 0));
            //sub_brain[2].GetComponent<glowmanager_b>().SetGlow();
            stimulate_btn[1].interactable = true ;
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
        //Debug.Log("Quit!!!");
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
        if (!guide_audio.isPlaying && (index!=0 ||isListening))
        {
            guide_audio.clip = guide_sounds[index];
            guide_audio.Play();
            Debug.Log("asdf");
        }

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
        yield return new WaitForSeconds(12.5f);

        sub_brain[0].GetComponent<glowmanager_r>().SetGlow(false);
    }
}
