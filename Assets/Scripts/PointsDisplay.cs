using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsDisplay : MonoBehaviour
{
    private const string displayString = "Points: {0}";
    public Text textDisplay;
    public PlayerController playerController;

    private int playerPoints;

    // Update is called once per frame
    void Update()
    {
        playerPoints = playerController.Points;
        textDisplay.text = string.Format(displayString, playerPoints);
    }
}
