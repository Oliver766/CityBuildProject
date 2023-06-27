using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Daysystem : MonoBehaviour
{
    [SerializeField]
    private float timeMultiplier;
    [SerializeField]
    private float startHour;
    [SerializeField]
    private TextMeshProUGUI timeText;

    private DateTime currentTime;

    private void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);
    }


    private void UpdateTimeOfDay()
    {

    }

}
