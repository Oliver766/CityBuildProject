using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
// script by Oliver lancashire
// sid 1901981
public class DaysCounter : MonoBehaviour
{
    [Header("int")]
    public int dayCount;
    [Header("UI")]
    public TextMeshProUGUI dayTXT;
    public Slider Slider;
    [Header("Float")]
    public float time;
    [Header("bool")]
    public bool newDay = true;
    public bool active;
    [Header("Reference")]
    private Daysystem lm;
    public AchievementManager achievementManager;
    public LevelSystemv2 systemv2;


    void Start()
    {
        // set variables 
        lm = FindObjectOfType<Daysystem>();
        time = lm.TimeOfDay;
        Slider.value = time;
        active = true;
     
    }

    void Update()
    {
        // update day text
        dayTXT.text = dayCount.ToString();
        // check day and time for new
        if (newDay && time < 2)
        {
            newDay = false;
            dayCount ++; 
        }
        // check time for new day and reset slider.
        if (time >= 23f)
		{
            newDay = true;
            Slider.value = 0;
		}
        time = lm.TimeOfDay; // set time
        // if day 2  is current then run function
        if(dayCount == 2 )
        {
            achievementManager.achievementToShow = AchievementID.TwoDaysinthecity;
            while (active == true)
            {
                 
                 achievementManager.UnlockAchievement(AchievementID.TwoDaysinthecity);
                    active = false;
                achievementManager.achievementToShow = AchievementID.AHelpingHand;
               

            }
  
        }

        // set slider value to current time.
        if(newDay == false)
        {
            Slider.value = time;
        }
    }
}
