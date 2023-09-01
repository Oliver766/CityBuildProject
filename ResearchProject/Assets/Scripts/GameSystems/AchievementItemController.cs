using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// script reference by - Zee Vasilyev - https://www.youtube.com/watch?v=H2qMxWcO9dg&list=PLFY3TFPG0dkomvWyb2fcPeqDSSvIO0NYb&index=13
// script edited by Oliver lancashire
// sid 1901981
public class AchievementItemController : MonoBehaviour {

    [Header("UI")]
    [SerializeField] Image unlockedIcon;
    [SerializeField] Image lockedIcon;
    [SerializeField] Text titleLabel;
    [SerializeField] Text descriptionLabel;
    [Header("bool")]
    public bool unlocked;
    [Header("references")]
    public Achievement achievement;
	
    /// <summary>
    /// function to update UI
    /// </summary>
    public void RefreshView()
    {
        titleLabel.text = achievement.title;
        descriptionLabel.text = achievement.description;

        unlockedIcon.enabled = unlocked;
        lockedIcon.enabled = !unlocked;
    }

    /// <summary>
    /// Will update UI when object is active
    /// </summary>
    private void OnValidate()
    {
        RefreshView();
    }

}
