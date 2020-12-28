using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages FullscreenToggle which is in OptionsScene.
/// </summary>
public class FullscreenToggleController : MonoBehaviour
{
    private Toggle toggle;

    /// <summary>
    /// On start switch toggle off if fullscreen is disabled.
    /// </summary>
    private void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.isOn = OptionsManager.GetFullscreen();
    }

    /// <summary>
    /// <para>This method is called when fullscreen toggle value is changed.</para>
    /// <para>Sets fullscreen using <see cref="OptionsManager"/>.</para>
    /// </summary>
    /// <param name="fullscreen">Boolean value which tells if fullscreen should be enabled or disabled.</param>
    public void ValueChanged(bool fullscreen)
    {
        OptionsManager.SetFullscreen(fullscreen);
    }
}
