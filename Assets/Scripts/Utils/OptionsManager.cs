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
}
