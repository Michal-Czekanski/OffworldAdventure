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
}
