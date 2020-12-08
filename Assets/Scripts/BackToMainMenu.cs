using Assets.Scripts.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenu : MonoBehaviour
{
    /// <summary>
    /// This method is called when user wants to return to MainMenu.
    /// </summary>
    public void Back()
    {
        SceneManager.LoadScene((int)SceneBuildIndex.MainMenu);
    }
}
