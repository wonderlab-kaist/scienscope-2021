using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pop_up : MonoBehaviour
{


    public GameObject popup;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Popup_on()
    {
        popup.SetActive(true);
    }

    public void Popup_off()
    {
        popup.SetActive(false);
    }
}
