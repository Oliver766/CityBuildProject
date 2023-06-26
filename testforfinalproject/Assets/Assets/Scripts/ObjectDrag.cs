using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    //distance from the click position to the object's center
    private Vector3 offset;

    private void OnMouseDown()
    {
        //initialize
        offset = transform.position - BuildingSystem.GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        //get the position of the mouse with offset
        Vector3 pos = BuildingSystem.GetMouseWorldPosition() + offset;
        //snap to grid and initialize object's position
        transform.position = BuildingSystem.current.SnapCoordinateToGrid(pos);
    }
}
