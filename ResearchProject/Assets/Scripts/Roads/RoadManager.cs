using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public PlaceMentManager placeMentManager;

    public List<Vector3Int> temporaryPlacementPoistions = new List<Vector3Int>();
    public List<Vector3Int> roadPositionsToRecheck = new List<Vector3Int>();


    public GameObject roadStraight;

    public RoadFixer roadFixer;


    public void Start()
    {
        roadFixer = GetComponent<RoadFixer>();
    }
    public void placeRoad(Vector3Int position)
    {
        if (placeMentManager.checkIfPoistionInBound(position) == false)
            return;
        if (placeMentManager.CheckIfPositionIsFree(position) == false)
            return;
        temporaryPlacementPoistions.Clear();
        temporaryPlacementPoistions.Add(position);
        placeMentManager.PlaceTemporaryStructure(position, roadStraight, CellType.Road);
        FixRoadPrefabs();
    }

    private void FixRoadPrefabs()
    {
        foreach(var temporaryPosition in temporaryPlacementPoistions)
        {
            roadFixer.FixRoadAtPosition(placeMentManager, temporaryPosition);
            var neighbours = placeMentManager.GetNeighbourTypesFor(temporaryPosition, CellType.Road);
            foreach(var roadposition in neighbours)
            {
                roadPositionsToRecheck.Add(roadposition);
            }
        }
        foreach(var positionToFix in roadPositionsToRecheck)
        {
            roadFixer.FixRoadAtPosition(placeMentManager, positionToFix);
        }
    }
}
