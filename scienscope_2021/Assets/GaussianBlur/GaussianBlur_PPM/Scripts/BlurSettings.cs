/*
BlurSettings.cs
stores the settings for the blur effect and creates the mapping texture that will be used by the camera
*/

using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

/// <summary>
/// The serializable data representation of our custom Post Process effect.
/// Note that for serialization reasons the class name must match the file name.
/// </summary>
[Serializable]
[PostProcess(typeof(BlurRenderer), PostProcessEvent.BeforeStack, "Custom/GaussianBlur_PPM")]
public class BlurSettings: PostProcessEffectSettings
{
    /// <summary>
    /// the blur amount
    /// </summary>
    [Range(0, 100), Tooltip("The magnitude in texels of distortion fx.")]
    public IntParameter Iterations = new IntParameter { value = 0 };

    /// <summary>
    /// Lowers the resolution of the texture, thus allowing for a larger blur without so many iterations
    /// </summary>
    [Range(0, 4), Tooltip("The down-scale factor to apply to the generated texture.")]
    public IntParameter DownRes = new IntParameter { value = 0 };

    /// <summary>
    /// Light/Dark
    /// </summary>
    [Range(0, 2), Tooltip("Light/Dark")]
    public FloatParameter Lightness = new FloatParameter { value = 1 };

    /// <summary>
    /// The Saturation of Color
    /// </summary>
    [Range(-10, 10), Tooltip("The Saturation of Color")]
    public FloatParameter Saturation = new FloatParameter { value = 1 };

    /// <summary>
    /// The Tint of Color
    /// </summary>
    [Tooltip("The Tint of Color")]
    public ColorParameter Tint = new ColorParameter { value = UnityEngine.Color.white };

    /// <summary>
    /// Toggles the debug view to show the distortion effects in the world as color values.
    /// </summary>
    [Tooltip("Displays the Distortion Effects in debug view.")]
    public BoolParameter DebugView = new BoolParameter { value = false };
    
}

/// <summary>
/// The renderer for the custom Distortion Post Process Effect.
/// </summary>
public class BlurRenderer: PostProcessEffectRenderer<BlurSettings>
{
    /// <summary>
    /// Cached PropertyToID lookup for the shader uniform variable named "_GlobalDistortionTex".
    /// </summary>
    private int _globalBlurTexID;

    /// <summary>
    /// Cached reference to the shader containing our custom post process.
    /// </summary>
    private Shader _blurShader;

    /// <summary>
    /// Overridden to indicate our effect requires the camera depth texture.
    /// </summary>
    public override DepthTextureMode GetCameraFlags()
    {
        return DepthTextureMode.Depth;
    }

    /// <summary>
    /// Caches the shader property ID when the effect is initialized.
    /// </summary>
    public override void Init()
    {
        _globalBlurTexID = Shader.PropertyToID("_GlobalBlurTex");
        _blurShader = Shader.Find("Hidden/GaussianBlur_PPM");
        base.Init();
    }

    /// <summary>
    /// Renders the effect by targeting a temporary texture to render all registered distortion
    /// effects into, then by performing a full screen pass which offsets UVs by the float values
    /// in the temporary texture.
    /// </summary>
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(_blurShader);
        sheet.properties.SetFloat("_Iterations", settings.Iterations);
        sheet.properties.SetFloat("_Lightness", settings.Lightness);
        sheet.properties.SetFloat("_Saturation", settings.Saturation);
        sheet.properties.SetColor("_Tint", settings.Tint);

        if (!settings.DebugView)
        {
            context.command.GetTemporaryRT(_globalBlurTexID,
                context.camera.pixelWidth >> settings.DownRes,
                context.camera.pixelHeight >> settings.DownRes,
                0, FilterMode.Bilinear, RenderTextureFormat.RFloat);
            context.command.SetRenderTarget(_globalBlurTexID);
            context.command.ClearRenderTarget(false, true, Color.clear);

        }

        BlurManager.Instance.PopulateCommandBuffer(context.command);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);

    }
}
