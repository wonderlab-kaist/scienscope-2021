/*
BlurRenderer_Mobile.cs
Creates global textures that have been passed through the GaussianBlur_Mobile(Hidden).shader
*/

using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class BlurRenderer_CamTransition : MonoBehaviour
{



 #region BlurScaleSlider

    //this section will enable a slider that controls both the Iterations and DownRes
    //Uncomment the code below to use it


    [Range(0, 100)]
    /// <summary>
    /// The blur scale.
    /// </summary>
    public int blurScale;

    public void Update()
    {
        int tempBlurScale = blurScale;

        Iterations = 0;
        DownRes = 0;

        while (tempBlurScale > 20 && DownRes < 5)
        {
            DownRes += 1;
            tempBlurScale -= 15;
        }

        Iterations = tempBlurScale;

        tempBlurScale = 0;
    }

#endregion

    [Range(0, 25)]
    /// <summary>
    /// how many Iterations should the blur be applied for
    /// </summary>
    [HideInInspector] //not needed since we are using the blur slider 
    public int Iterations;

    [Range(0, 5)]
    /// <summary>
    /// Lowers the resolution of the texture, thus allowing for a larger blur without so many iterations
    /// </summary>
    [HideInInspector] //not needed since we are using the blur slider 
    public int DownRes;

    /// <summary>
    /// update blur now...overrides UpdateBlur and UpdateRate
    /// </summary>
    public bool UpdateBlurNow = false;

    /// <summary>
    /// Weather to update the blur
    /// </summary>
    public bool UpdateBlur = true;

    /// <summary>
    /// amount of time that will pass before re-rendering the blur
    /// </summary>
    public float UpdateRate = 0.001f;

    /// <summary>
    /// update using time.deltatime ...or the time in the game. if false it will update using real time (i.e. unscaled time).
    /// </summary>
    public bool updateUsingGameTime = true;

    /// <summary>
    /// amount of time since last update.
    /// </summary>
    private float timeSinceUpdate = 0.0f;

    /// <summary>
    /// Stores the blur texture between renders
    /// </summary>
    private RenderTexture BlurTexture;


    /// <summary>
    /// The material where we'll pass the screen texture though to create the blur
    /// </summary>
    private Material mat;

    public enum _BlurNum
    {
        _Blur01,
        _Blur02,
    }

    public _BlurNum _Blur = _BlurNum._Blur01;

    private void Start()
	{
        //set up the blur texture

        BlurTexture = new RenderTexture(256, 256, 16, RenderTextureFormat.ARGB32);

        /*
        //we can swap the code above for a texture that will take up less ram...if needed.
        BlurTexture = new RenderTexture(128, 128, 4, RenderTextureFormat.ARGB4444);
        */

        //create the texture
        BlurTexture.Create();

        //create our material if we have not already
        if (mat == null)
        {
            mat = new Material(Shader.Find("Hidden/GaussianBlur_CamTransition"));
        }


        //print(_Blur.ToString());
	}

    
    //OnRenderImage will execute before each frame renders
    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        // if we need to re-render
        if (
            timeSinceUpdate >= UpdateRate && UpdateBlur
            || UpdateBlurNow
            )
        {

            UpdateBlurNow = false;

            //set the width and height
            int width = src.width >> DownRes;
            int height = src.height >> DownRes;

            //create a temp texture
            RenderTexture rt = RenderTexture.GetTemporary(width, height);

            //move the screen image to our temp texture
            Graphics.Blit(src, rt);

            //loop to add the blur to our temp texture
            for (int i = 0; i < Iterations; i++)
            {
                RenderTexture rt2 = RenderTexture.GetTemporary(width, height);
                Graphics.Blit(rt, rt2, mat);
                RenderTexture.ReleaseTemporary(rt);
                rt = rt2;
            }

            //store our texture in BlurTexture
            Graphics.Blit(rt, BlurTexture);

            //set the global texture for our shader to use
            Shader.SetGlobalTexture(_Blur.ToString(), rt);

            //remove the temp texture
            RenderTexture.ReleaseTemporary(rt);

            //reset time since last update
            timeSinceUpdate = 0.0f;

        }
        else
        {
            //set the global texture again...must be done for each frame
            Shader.SetGlobalTexture(_Blur.ToString(), BlurTexture);
        }

        //update time since last update
        //if (updateUsingGameTime)
        //{
        //    timeSinceUpdate += Time.deltaTime;
        //}
        //else
        //{
        //    timeSinceUpdate += 0.02f;
        //}

        timeSinceUpdate += (updateUsingGameTime) ? Time.deltaTime : 0.02f;

        //make sure the camera renders as normal.
        Graphics.Blit(src, dst);

    }



    /// <summary>
    /// Creates a BlurRenderer_Mobile on the main camera
    /// </summary>
    static public BlurRenderer_CamTransition Create()
    {
        BlurRenderer_CamTransition BRM = Camera.main.gameObject.GetComponent<BlurRenderer_CamTransition>();

        if (BRM == null)
        {
            BRM = Camera.main.gameObject.AddComponent<BlurRenderer_CamTransition>();
        }

        return BRM;
    }

    //override to allow different camera
    static public BlurRenderer_CamTransition Create(Camera ThisCamera)
    {

        BlurRenderer_CamTransition BRM = ThisCamera.gameObject.GetComponent<BlurRenderer_CamTransition>();

        if (BRM == null)
        {
            BRM = ThisCamera.gameObject.AddComponent<BlurRenderer_CamTransition>();
        }

        return BRM;
    }

}