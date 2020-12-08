using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Each LevelButton in SelectLevel scene should have this script attached.
/// </summary>
public class SelectLevelButtonController : MonoBehaviour
{
    public int levelNum;
    private Button selectLevelButton;

    private void Start()
    {
        selectLevelButton = GetComponent<Button>();
        if (!LevelManager.IsLevelUnlocked(levelNum))
        {
            selectLevelButton.enabled = false;
        }
    }
}
