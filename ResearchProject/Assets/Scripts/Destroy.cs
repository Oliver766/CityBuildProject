using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// script  by Oliver lancashire
public class Destroy : MonoBehaviour
{
    [Header("Time")]
    public float time;
    void Start()
    {
        Destroy(gameObject, time); // destroy object it's attached too after an amount of time
    }

}
