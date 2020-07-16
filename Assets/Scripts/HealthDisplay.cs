using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public PlayerController playerController;
    public RectTransform heartsPlaceholder;
    public GameObject heartPrefab;

    private int displayedPlayerHp;

    private float heartHeight;
    private float heartWidth;

    private float spaceBetweenHearts;

    private GameObject[] hearts;

    public void Start()
    {
        float heartsPlaceholderWidth = heartsPlaceholder.sizeDelta[0];
        float heartsPlaceholderHeight = heartsPlaceholder.sizeDelta[1];

        heartWidth = heartsPlaceholderWidth / playerController.maxHp;
        heartHeight = heartsPlaceholderHeight;

        spaceBetweenHearts = heartWidth / 10;

        hearts = new GameObject[playerController.maxHp];

        displayedPlayerHp = 0;
    }


    void Update()
    {
        if(playerController.Hp >= 0)
        {
            if (displayedPlayerHp > playerController.Hp)
            {
                DisplayLessHeartsAfterHpLoss();
            }
            else if (displayedPlayerHp < playerController.Hp)
            {
                DisplayMoreHeartsAfterHpGain();
            }
        }

    }

    private void DisplayMoreHeartsAfterHpGain()
    {
        int howManyHpGained = playerController.Hp - displayedPlayerHp;

        for (int i = displayedPlayerHp; i < playerController.Hp; i++)
        {
            GameObject newHeartImage = CreateNewHeartAndPositionIt(i);
            hearts[i] = newHeartImage;
        }

        displayedPlayerHp = playerController.Hp;
    }

    private GameObject CreateNewHeartAndPositionIt(int i)
    {
        GameObject newHeartImage = Instantiate(heartPrefab, heartsPlaceholder);

        Vector2 newHeartImageSize = new Vector2(heartWidth, heartHeight);
        float newHeartImageX = (heartWidth / 2) + spaceBetweenHearts + i * (heartWidth + spaceBetweenHearts);
        float newHeartImageY = -(heartHeight / 2);


        (newHeartImage.transform as RectTransform).sizeDelta = newHeartImageSize;
        (newHeartImage.transform as RectTransform).localPosition = new Vector3(newHeartImageX, newHeartImageY, 0);
        return newHeartImage;
    }

    private void DisplayLessHeartsAfterHpLoss()
    {
        int howManyHpLost = displayedPlayerHp - playerController.Hp;

        for (int i = displayedPlayerHp; i > playerController.Hp; i--)
        {
            Destroy(hearts[i - 1]);
            hearts[i - 1] = null;
        }

        displayedPlayerHp = playerController.Hp;
    }
}
