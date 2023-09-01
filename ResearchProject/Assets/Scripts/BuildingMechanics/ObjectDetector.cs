using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// script reference by Sunny Valley Studio - https://www.youtube.com/watch?v=8ayFCDbfIIM&list=PLcRSafycjWFd6YOvRE3GQqURFpIxZpErI
// script eddited by Oliver Lancashire
// sid 1901981
public class ObjectDetector : MonoBehaviour
{
    [Header("Layer")]
    public LayerMask groundMask;
    /// <summary>
    /// check for ground collision
    /// </summary>
    /// <param name="ray"></param>
    /// <returns></returns>
    public Vector3Int? RaycastGround(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
        {
            Transform objectHit = hit.transform;
            Vector3Int positionInt = Vector3Int.RoundToInt(hit.point);
            return positionInt;
        }
        return null;
    }
    /// <summary>
    /// return the collision
    /// </summary>
    /// <param name="ray"></param>
    /// <returns></returns>
    public GameObject RaycastAll(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            return hit.transform.gameObject;
        }
        return null;
    }
}
