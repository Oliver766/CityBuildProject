using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetection : MonoBehaviour
{
    public LayerMask groundMask;

    public Vector3Int? raycastGround(Ray ray)
    {
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit, Mathf.Infinity, groundMask))
        {
            Transform objectHit = hit.transform;
            Vector3Int positionInt = Vector3Int.RoundToInt(hit.point);
            return positionInt;
        }
        return null;
    }

    public GameObject RaycastAll(Ray ray)
    {
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            return hit.transform.gameObject;
        }
        return null;
    }
}
