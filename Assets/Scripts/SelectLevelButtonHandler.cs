using Assets.Scripts.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is used to handle button clicks in SelectLevel scene
/// </summary>
public class SelectLevelButtonHandler : MonoBehaviour
{
    /// <summary>
    /// This method is called when Back button is clicked
    /// </summary>
    public void BackToMainMenu()
    {
        SceneManager.LoadScene((int)SceneBuildIndex.MainMenu);
    }
}
