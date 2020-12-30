using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class ButtonHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// Button text.
    /// </summary>
    private TextMeshProUGUI textView;

    /// <summary>
    /// Button which will be animated when highlighted.
    /// </summary>
    Button button;

    void Start()
    {
        textView = GetComponentInChildren<TextMeshProUGUI>();
        button = GetComponent<Button>();
    }

    /// <summary>
    /// Change text when button is highlighted.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button.interactable)
        {
            Highlight();
        }
    }

    /// <summary>
    /// Highlights button by changing its text from <...> to < ... >.
    /// </summary>
    private void Highlight()
    {
        textView.text = Regex.Replace(textView.text, @"<(.*)>", @"< $1 >");
    }

    /// <summary>
    /// Make text normal when button is no longer highlighted.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (button.interactable)
        {
            StopHighlight();
        }
    }

    /// <summary>
    /// Stops highlighting button by changing its text from < ... > to <...>.
    /// </summary>
    private void StopHighlight()
    {
        textView.text = Regex.Replace(textView.text, @"< (.*) >", @"<$1>");
    }

    /// <summary>
    /// On button disable stop highlighting it.
    /// </summary>
    private void OnDisable()
    {
        StopHighlight();
    }
}
