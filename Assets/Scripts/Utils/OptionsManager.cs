﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
