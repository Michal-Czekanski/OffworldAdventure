using Assets.Scripts.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    /// <summary>
    /// Loads level if it is unlocked.
    /// </summary>
    /// <param name="levelNum">Level number to load.</param>
    /// <returns>True if level was loaded, false otherwise.</returns>
    public static bool LoadLevel(int levelNum)
    {
        if (IsLevelUnlocked(levelNum))
        {
            SceneManager.LoadScene((int)SceneBuildIndex.Level1 + levelNum - 1);
        }
        return false;
    }
}
