using cityBuilder.AI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// script reference by Sunny Valley Studio - https://www.youtube.com/watch?v=MpGfSbMikeQ&list=PLcRSafycjWFe50Nz4OBZC-5dYBKf3581v
//https://www.youtube.com/watch?v=mu7f3Z1lRsE&list=PLcRSafycjWFdDou6OTCmGbRZ9lwLXjuMO
// script eddited by Oliver Lancashire
// sid 1901981
public class PathVisualizer : MonoBehaviour
{
    [Header("line renderer")]
    LineRenderer lineRenderer;
    [Header("agen")]
    AiAgent currentAgent;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>(); // get component
        lineRenderer.positionCount = 0; // set position
    }
    /// <summary>
    /// show ai path
    /// </summary>
    /// <param name="path"></param>
    /// <param name="agent"></param>
    /// <param name="color"></param>
    public void ShowPath(List<Vector3> path, AiAgent agent, Color color)
    {
        ResetPath();
        lineRenderer.positionCount = path.Count;
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        for (int i = 0; i < path.Count; i++)
        {
            lineRenderer.SetPosition(i, path[i] + new Vector3(0, agent.transform.position.y, 0));
        }
        currentAgent = agent;
        currentAgent.OnDeath += ResetPath;
    }
    /// <summary>
    /// reset ai path
    /// </summary>
    public void ResetPath()
    {
        if(lineRenderer != null)
        {
            lineRenderer.positionCount = 0;
        }
        if(currentAgent != null)
        {
            currentAgent.OnDeath -= ResetPath;
        }
        currentAgent = null;
    }
}
