using Assets.Scripts.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Manages game pausing.
/// </summary>
public class PauseManagerController : MonoBehaviour
{
    public GameObject pauseScene;
    public GameObject pauseOptionsScene;

    /// <summary>
    /// Resume button from PauseScene
    /// </summary>
    public Button pS_resumeButton;

    /// <summary>
    /// Options button from PauseScene
    /// </summary>
    public Button pS_optionsButton;

    /// <summary>
    /// Main menu button from PauseScene
    /// </summary>
    public Button pS_mainMenuButton;

    /// <summary>
    /// Back button from PauseOptionsScene
    /// </summary>
    public Button pOS_backButton;

    public static bool gamePaused;

    private bool inOptions = false;

    /// <summary>
    /// On awake disable pauseScene and pauseOptionsScene. Add listeners to buttons.
    /// </summary>
    private void Awake()
    {
        pauseScene.SetActive(false);
        pauseOptionsScene.SetActive(false);

        pS_resumeButton.onClick.AddListener(new UnityEngine.Events.UnityAction(ResumeGame));
        pS_optionsButton.onClick.AddListener(new UnityEngine.Events.UnityAction(OpenOptions));
        pS_mainMenuButton.onClick.AddListener(new UnityEngine.Events.UnityAction(BackToMainMenu));

        pOS_backButton.onClick.AddListener(new UnityEngine.Events.UnityAction(BackToPauseMenu));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // On ESC click ...
        {
            if (!gamePaused)
            {
                PauseGame();
            }
            else
            {
                if (inOptions) // If we are in pause options menu -> go back to pause menu.
                {
                    BackToPauseMenu();
                }
                else // If we are in pause menu -> resume game.
                {
                    ResumeGame();
                }
            }
        }
    }

    
    /// <summary>
    /// Pauses game.
    /// </summary>
    private void PauseGame()
    {
        pauseScene.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }


    /// <summary>
    /// Resumes game.
    /// </summary>
    private void ResumeGame()
    {
        pauseScene.SetActive(false);
        UnfreezeGame();
    }

    /// <summary>
    /// Disables game freeze.
    /// </summary>
    private void UnfreezeGame()
    {
        Time.timeScale = 1f;
        gamePaused = false;
    }

    /// <summary>
    /// Returns to main menu.
    /// </summary>
    private void BackToMainMenu()
    {
        UnfreezeGame();
        SceneManager.LoadScene((int)SceneBuildIndex.MainMenu);
    }

    /// <summary>
    /// Returns from PauseOptionsScene to PauseScene.
    /// </summary>
    private void BackToPauseMenu()
    {
        pauseOptionsScene.SetActive(false);
        pauseScene.SetActive(true);
        inOptions = false;
    }

    /// <summary>
    /// Opens PauseOptionsScene.
    /// </summary>
    private void OpenOptions()
    {
        pauseScene.SetActive(false);
        pauseOptionsScene.SetActive(true);
        inOptions = true;
    }
}
