using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to manage levels
/// </summary>
public static class LevelManager
{
    public static int levelsNum = 8;

    // If player completed level 1, 2, 3 then last unlocked level = 3
    private readonly static string lastUnlockedLevelPrefsString = "lastUnlockedLevel";

    /// <summary>
    /// This method tells if level is unlocked
    /// </summary>
    /// <param name="levelNum"></param>
    /// <returns></returns>
    public static bool IsLevelUnlocked(int levelNum)
    {
        return levelNum <= PlayerPrefs.GetInt(lastUnlockedLevelPrefsString, 1);
    }
}
