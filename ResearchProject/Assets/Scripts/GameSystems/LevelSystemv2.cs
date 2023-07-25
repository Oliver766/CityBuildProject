using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelSystemv2 : MonoBehaviour
{
    public TextMeshProUGUI leveltext;
    public TextMeshProUGUI ExperienceText;
    public int level;
    public float currentXP;
    public float targetXP;
    public Image xpProgressbar;
    public AchievementManager AchievementManager;


    public void Update()
    {
        ExperienceText.text = currentXP + "/" + targetXP;

        ExperienceController();

        if(level == 5)
        {
            Debug.Log("You've won");    
        }

        if( level == 1)
        {
            AchievementManager.UnlockAchievement(AchievementID.NewOne);
        }

    }

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
