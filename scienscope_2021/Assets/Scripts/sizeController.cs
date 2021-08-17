using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sizeController : MonoBehaviour
{   
    public GameObject myGO;
    Canvas myCanvas;
    Vector2 canvasSize;
    

    // Start is called before the first frame update
    void Start()
    {
        myCanvas = myGO.GetComponent<Canvas>();
        canvasSize = myCanvas.renderingDisplaySize;

        //Debug.Log(myCanvas);
        //Debug.Log(myCanvas.renderingDisplaySize);



        transform.localScale = new Vector3((canvasSize.y)*0.01f, (canvasSize.y)*0.01f, 1.0f); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
