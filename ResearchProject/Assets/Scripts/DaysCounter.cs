using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DaysCounter : MonoBehaviour
{

    public int dayCount;
    private Daysystem lm;
    public float time;
    private bool newDay = true;
    public TextMeshProUGUI dayTXT;

    public Skybox skybox;

    

    // Start is called before the first frame update
    void Start()
    {
        lm = FindObjectOfType<Daysystem>();
        time = lm.TimeOfDay;
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
		}
        time = lm.TimeOfDay;
    }
}
