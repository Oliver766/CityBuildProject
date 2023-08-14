using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerping : MonoBehaviour
{
    public Vector3 endPosition;
    public Vector3 startPosition;
    private float desireDuration = 3f;
    private float elapseTime;
    public DaysCounter counter;
    public bool restart;


    public 
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    
 
    }

    // Update is called once per frame
    void Update()
    {
        if (counter.time < 2 && restart == true)
        {
            Lerp();
            restart = false;
        }
        if (counter.time >= 23f)
        {
            transform.position = startPosition;
            restart = true;
        }

    }

    public void Lerp()
    {
        elapseTime += Time.deltaTime;
        float percentageComplete = elapseTime / desireDuration;

        transform.position = Vector3.Lerp(startPosition, endPosition,Mathf.SmoothStep(0,1, percentageComplete));
        
    }
}
