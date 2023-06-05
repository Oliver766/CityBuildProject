using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public PlaceMentManager placeMentManager;

    public List<Vector3Int> temporaryPlacementPoistions = new List<Vector3Int>();

    public GameObject roadStraight;

    public void placeRoad(Vector3Int position)
    {
        if (placeMentManager.checkIfPoistionInBound(position) == false)
            return;
        if (placeMentManager.CheckIfPositionIsFree(position) == false)
            return;
        placeMentManager.PlaceTemporaryStructure(position, roadStraight, CellType.Road);
    }
}
