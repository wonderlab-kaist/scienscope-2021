/*
BlurRenderer_Mobile.cs
Creates global textures that have been passed through the GaussianBlur_Mobile(Hidden).shader
*/


using System.Collections.Generic;
using UnityEngine.Rendering;

/// <summary>
/// A manager that keeps tracks of objects that need to be rendered to the distortion buffer just
/// before rendering our custom after-stack Post Process effect.
/// </summary>
public class BlurManager
{
    #region Singleton

    /// <summary>
    /// Singleton backing field.
    /// </summary>
    private static BlurManager _instance;

    /// <summary>
    /// Singleton accessor. Replacing this for whatever ServiceLocator/Injection pattern your game
    /// uses would be a good idea when implementing a system like this!
    /// </summary>
    public static BlurManager Instance
    {
        get
        {
            return _instance = _instance ?? new BlurManager();
        }
    }

    #endregion

    /// <summary>
    /// The collection of distortion effects 
    /// </summary>
    private readonly List<BlurEffect> _blurEffect = new List<BlurEffect>();

    /// <summary>
    /// Registers an effect with the manager.
    /// </summary>
    public void Register(BlurEffect blurEffect)
    {
        _blurEffect.Add(blurEffect);
    }

    /// <summary>
    /// Deregisters an effect from the manager.
    /// </summary>
    public void Deregister(BlurEffect blurEffect)
    {
        _blurEffect.Remove(blurEffect);
    }

    /// <summary>
    /// Adds the commands which draw the registered renderers to the target CommandBuffer.
    /// </summary>
    public void PopulateCommandBuffer(CommandBuffer commandBuffer)
    {
        for (int i = 0, len = _blurEffect.Count; i < len; i++)
        {
            var effect = _blurEffect[i];
            commandBuffer.DrawRenderer(effect.Renderer, effect.Material);
        }
    }
}
