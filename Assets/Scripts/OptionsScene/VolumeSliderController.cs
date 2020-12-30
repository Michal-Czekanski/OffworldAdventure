using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages VolumeSlider which is in Options scene.
/// </summary>
public class VolumeSliderController : MonoBehaviour
{
    private Slider volumeSlider;

    /// <summary>
    /// On awake set VolumeSlider's value to be the same as in the preferences. 
    /// </summary>
    private void Awake()
    {
        volumeSlider = GetComponent<Slider>();
        volumeSlider.value = OptionsManager.GetSoundLevel().Level;
    }
}
