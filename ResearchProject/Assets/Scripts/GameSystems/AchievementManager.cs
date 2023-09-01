using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// script reference by - Zee Vasilyev - https://www.youtube.com/watch?v=H2qMxWcO9dg&list=PLFY3TFPG0dkomvWyb2fcPeqDSSvIO0NYb&index=13
// script edited by Oliver lancashire
// sid 1901981
public class AchievementManager : MonoBehaviour {
    [Header("references")]
    public AchievementDatabase database;
    public AchievementNotificationController achievementNotificationController;
    public AchievementID achievementToShow;
    public AchievementDropdownController achievementDropdownController;
    [Header("gameobjects")]
    public GameObject achievementItemPrefab;
    [Header("transform")]
    public Transform scrollViewContent;

    

    [SerializeField] [HideInInspector]
    private List<AchievementItemController> achievementItems;

   

    private void Start()
    {
        achievementDropdownController.onValueChanged += HandleAchievementDropdownValueChanged; // update drop down
        LoadAchievementsTable(); // load table
        //LoadTable();
    }
    /// <summary>
    /// show notification of particular achievement
    /// </summary>
    public void ShowNotification()
    {
        Achievement achievement = database.achievements[(int)achievementToShow];
        achievementNotificationController.ShowNotification(achievement);
    }
    /// <summary>
    /// show achievements
    /// </summary>
    /// <param name="achievement"></param>
    private void HandleAchievementDropdownValueChanged(AchievementID achievement)
    {
        achievementToShow = achievement;
    }
    /// <summary>
    /// load table
    /// </summary>
    [ContextMenu("LoadAchievementsTable()")]
    private void LoadAchievementsTable()
    {
        //foreach (AchievementItemController item in achievementItems)
        //{
        //    Destroy(item.gameObject);
        //}
        achievementItems.Clear();
        foreach (Achievement achievement in database.achievements)
        {
            GameObject obj = Instantiate(achievementItemPrefab, scrollViewContent);
            AchievementItemController item = obj.GetComponent<AchievementItemController>();
            //bool unlocked = PlayerPrefs.GetInt(achievement.id, 0) == 1;
            //item.unlocked = unlocked;
            item.achievement = achievement;
            item.RefreshView();
            achievementItems.Add(item);
        }
    }

    //[ContextMenu("LoadTable()")]
    //public void LoadTable()
    //{
    //    //foreach(AchievementItemController items in achievementItems)
    //    //{
    //    //    Destroy(items.gameObject);
    //    //}

    //    achievementItems.Clear();
    //    foreach(Achievement achievement in database.achievements)
    //    {
    //        GameObject obj = Instantiate(achievementItemPrefab, scrollViewContent);
    //        AchievementItemController controller = obj.GetComponent<AchievementItemController>();
    //        controller.achievement = achievement;
    //        controller.RefreshView();
    //        achievementItems.Add(controller);
    //    }
    //}

    /// <summary>
    /// unlock achievements
    /// </summary>
    public void UnlockAchievement()
    {
        UnlockAchievement(achievementToShow);
    }
    /// <summary>
    /// unlock achievements
    /// </summary>
    /// <param name="achievement"></param>
    public void UnlockAchievement(AchievementID achievement) 
    {
        AchievementItemController item = achievementItems[(int)achievement];
        if (item.unlocked)
            return;

        ShowNotification();
        //PlayerPrefs.SetInt(item.achievement.id, 1);
        item.unlocked = true;
        item.RefreshView();
    }
    /// <summary>
    /// lock all achievements function
    /// </summary>
    public void LockAllAchievements()
    {
        foreach (Achievement achievement in database.achievements)
        {
            PlayerPrefs.DeleteKey(achievement.id);
        }
        foreach (AchievementItemController item in achievementItems)
        {
            item.unlocked = false;
            item.RefreshView();
        }
    }

}
