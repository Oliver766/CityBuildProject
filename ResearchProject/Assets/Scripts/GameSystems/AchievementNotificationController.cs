using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// script reference by - Zee Vasilyev - https://www.youtube.com/watch?v=H2qMxWcO9dg&list=PLFY3TFPG0dkomvWyb2fcPeqDSSvIO0NYb&index=13
// script edited by Oliver lancashire
// sid 1901981
[RequireComponent(typeof(Animator))]
public class AchievementNotificationController : MonoBehaviour {

    [Header("UI")]
    [SerializeField] Text achievementTitleLabel;
    [Header("Animator")]
    private Animator m_animator;

    private void Awake()
    {
        m_animator = GetComponent<Animator>(); // get component
    }
    /// <summary>
    /// show notification function
    /// </summary>
    /// <param name="achievement"></param>
    public void ShowNotification(Achievement achievement)
    {
        achievementTitleLabel.text = achievement.title;
        m_animator.SetTrigger("Appear");
    }	
}

