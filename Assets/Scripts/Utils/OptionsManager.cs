using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Data;

/// <summary>
/// <para> Class used to manage game options.</para>
/// Possible options:
/// <list type="bullet">
/// <item>Resolution</item>
/// <item>Fullscreen</item>
/// <item>Sound</item>
/// </list>
///
/// </summary>
public static class OptionsManager
{

    private static readonly string resolutionPrefsKey = "Resolution";
    private static readonly string fullscreenPrefsKey = "Fullscreen";
    private static readonly string soundLevelPrefsKey = "SoundLevel";

    /// <summary>
    /// Retrieves currently set resolution from preferences.
    /// </summary>
    /// <returns>Currently set resolution.</returns>
    public static Assets.Scripts.Data.Resolution GetResolution()
    {
        int resolutionId = PlayerPrefs.GetInt(resolutionPrefsKey, Resolutions.minId);
        return Resolutions.allResolutions[resolutionId];
    }

    /// <summary>
    /// Sets resolution and saves it's id in preferences.
    /// </summary>
    /// <param name="resolution">Resolution to use and save.</param>
    public static void SetResolution(Assets.Scripts.Data.Resolution resolution)
    {
        Screen.SetResolution(resolution.Width, resolution.Height, Screen.fullScreen);
        PlayerPrefs.SetInt(resolutionPrefsKey, resolution.Id);
    }

    /// <summary>
    /// Tells if fullscreen is enabled. Retrieves information from preferences.
    /// </summary>
    /// <returns>True if fullscreen, false otherwise.</returns>
    public static bool GetFullscreen()
    {
        return PlayerPrefs.GetInt(fullscreenPrefsKey, 1) > 0;
    }

    /// <summary>
    /// Enables or disables fullscreen.
    /// </summary>
    /// <param name="fullscreen">Fullscreen on or off.</param>
    public static void SetFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
        if (fullscreen)
        {
            PlayerPrefs.SetInt(fullscreenPrefsKey, 1);
        }
        else
        {
            PlayerPrefs.SetInt(fullscreenPrefsKey, 0);
        }
        
    }
}
