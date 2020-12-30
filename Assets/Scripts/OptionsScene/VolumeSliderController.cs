using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Assets.Scripts.Data;

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

        volumeSlider.onValueChanged.AddListener(new UnityAction<float>(ValueChanged));
    }

    /// <summary>
    /// Sets new <see cref="SoundLevel"/> using <see cref="OptionsManager"/>.
    /// </summary>
    /// <param name="soundLevel">Sound level to set.</param>
    private void ValueChanged(float soundLevel)
    {
        OptionsManager.SetSoundLevel(new SoundLevel((int)soundLevel));
    }
}
