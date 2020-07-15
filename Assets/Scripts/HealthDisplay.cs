using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    private const string displayString = "Health: {0}";
    public Text textDisplay;
    public PlayerController playerController;

    private int playerHp;
    
    // Update is called once per frame
    void Update()
    {
        playerHp = playerController.Hp;
        textDisplay.text = string.Format(displayString, playerHp);
    }
}
