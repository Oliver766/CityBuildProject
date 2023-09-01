using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// script by Oliver lancashire
// sid 1901981
// script reference  - ketra Games - https://www.youtube.com/watch?v=MyVY-y_jK1I

public class Lerping : MonoBehaviour
{
    [Header("Vectors")]
    public Vector3 endPosition;
    public Vector3 startPosition;
    [Header("float")]
    private float desireDuration = 3f;
    private float elapseTime;
    [Header("Reference")]
    public DaysCounter counter;
    [Header("bools")]
    public bool restart;

    void Start()
    {
        startPosition = transform.position; // set position
    }

  
    void Update()
    {
        // checking the time and bool
        if (counter.time < 2 && restart == true)
        {
            Lerp();
            restart = false;
        }
        // checking if time is equal to 23 and more than
        if (counter.time >= 23f)
        {
            transform.position = startPosition; // reset position
            restart = true;
        }

    }
    /// <summary>
    /// lerp function from one point to another
    /// </summary>
    public void Lerp()
    {
        elapseTime += Time.deltaTime;
        float percentageComplete = elapseTime / desireDuration;
        transform.position = Vector3.Lerp(startPosition, endPosition,Mathf.SmoothStep(0,1, percentageComplete));
    }
}
