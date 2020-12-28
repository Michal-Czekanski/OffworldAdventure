using Assets.Scripts.Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages ResolutionDropdown which is in OptionsScene.
/// </summary>
public class ResolutionDropdownController : MonoBehaviour
{
    /// <summary>
    /// ResolutionDropdown
    /// </summary>
    TMP_Dropdown dropdown;
    private void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        FillDropdownWithResolutions();
    }

    /// <summary>
    /// Fills ResolutionDropdown with possible resolutions to choose.
    /// </summary>
    private void FillDropdownWithResolutions()
    {
        dropdown.options.Clear();
        foreach (Assets.Scripts.Data.Resolution resolution in Resolutions.allResolutions)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData(resolution.AsString));
        }

        dropdown.value = OptionsManager.GetResolution().Id;
    }

    /// <summary>
    /// Sets resolution chosen by user using <see cref="OptionsManager"/>.
    /// </summary>
    /// <param name="resolutionId"></param>
    public void ResolutionClick(int resolutionId)
    {
        OptionsManager.SetResolution(Resolutions.allResolutions[resolutionId]);
    }
}
