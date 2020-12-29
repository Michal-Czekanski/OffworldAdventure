using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;

public class ButtonHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// Button text.
    /// </summary>
    private TextMeshProUGUI textView;

    void Start()
    {
        textView = GetComponentInChildren<TextMeshProUGUI>();
    }

    /// <summary>
    /// Change text when button is highlighted.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        textView.text = Regex.Replace(textView.text, @"<(.*)>", @"< $1 >");
    }

    /// <summary>
    /// Make text normal when button is no longer highlighted.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        textView.text = Regex.Replace(textView.text, @"< (.*) >", @"<$1>");
    }
}
