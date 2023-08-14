using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DaysCounter : MonoBehaviour
{

    public int dayCount;
    private Daysystem lm;
    public float time;
    public bool newDay = true;
    public TextMeshProUGUI dayTXT;

    public bool active;

    public AchievementManager achievementManager;
    public LevelSystemv2 systemv2;

    public Slider Slider;

    

    // Start is called before the first frame update
    void Start()
    {
        lm = FindObjectOfType<Daysystem>();
        time = lm.TimeOfDay;
        Slider.value = time;
        active = true;
     
    }

    // Update is called once per frame
    void Update()
    {
        dayTXT.text = dayCount.ToString();
        if (newDay && time < 2)
        {
            newDay = false;
            dayCount ++;
            //
        }
        if (time >= 23f)
		{
            newDay = true;
            Slider.value = 0;
		}
        time = lm.TimeOfDay;

        if(dayCount == 2 )
        {
           
                while (active == true)
                {
                    achievementManager.UnlockAchievement(AchievementID.Dayinthecity);
                    active = false;
                }
            
          
           
            
        }

        if(newDay == false)
        {
            Slider.value = time;
        }
    }
}
