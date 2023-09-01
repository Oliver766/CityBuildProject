using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
// script by Oliver lancashire
// sid 1901981
public class LevelSystemv2 : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI leveltext;
    public TextMeshProUGUI ExperienceText;
    public Image xpProgressbar;
    [Header("Float")]
    public float currentXP;
    public float targetXP;
    [Header("Int")]
    public int level;
    [Header("Reference")]
    public AchievementManager AchievementManager;

    public void Update()
    {
        ExperienceText.text = currentXP + "/" + targetXP; // updating text when adding xp

        ExperienceController(); // run function

        // win function
        if(level == 5)
        {
            Debug.Log("You've won");    
        }

    }

    /// <summary>
    /// function updates slider and text
    /// </summary>
    public void ExperienceController()
    {
        leveltext.text = level.ToString();
        xpProgressbar.fillAmount = (currentXP / targetXP);

        if(currentXP >= targetXP) // level up
        {
            currentXP = currentXP - targetXP;
            level++;
            targetXP += 50;
        }
    }
}
