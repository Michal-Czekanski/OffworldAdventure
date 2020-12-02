using Assets.Scripts.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is used to handle button clicks in MainMenu scene
/// </summary>
public class MainMenuButtonHandler : MonoBehaviour
{
    /// <summary>
    /// This method is called when Quit button is clicked
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// This method is called when SelectLevel button is clicked.
    /// It opens SelectLevel screen.
    /// </summary>
    public void SelectLevelClick()
    {
        SceneManager.LoadScene((int)SceneBuildIndex.SelectLevel);
    }
}
