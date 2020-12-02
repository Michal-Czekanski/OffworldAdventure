using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
