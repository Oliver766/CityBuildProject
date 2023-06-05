using SVS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public PlaceMentManager placeMentManager;

    public List<Vector3Int> temporaryPlacementPositions = new List<Vector3Int>();
    public List<Vector3Int> roadPositionsToRecheck = new List<Vector3Int>();

    private Vector3Int startPosition;
    private bool placementMode = false;

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
        if(placementMode == false)
        {
            temporaryPlacementPositions.Clear();
            roadPositionsToRecheck.Clear();

            placementMode = true;
            startPosition = position;

            temporaryPlacementPositions.Add(position);
            placeMentManager.PlaceTemporaryStructure(position, roadFixer.deadEnd, CellType.Road);
          
        }
        else
        {
            placeMentManager.RemoveAllTemporaryStructures();
            temporaryPlacementPositions.Clear();

            foreach(var positionToFix in roadPositionsToRecheck)
            {
                roadFixer.FixRoadAtPosition(placeMentManager, positionToFix);
            }

            roadPositionsToRecheck.Clear();

            temporaryPlacementPositions = placeMentManager.GetPathbetween(startPosition, position);

            foreach(var temporaryPosition in temporaryPlacementPositions)
            {
                placeMentManager.PlaceTemporaryStructure(temporaryPosition, roadFixer.deadEnd, CellType.Road);
            }
        }

        FixRoadPrefabs();
    }

    private void FixRoadPrefabs()
    {
        foreach(var temporaryPosition in temporaryPlacementPositions)
        {
            roadFixer.FixRoadAtPosition(placeMentManager, temporaryPosition);
            var neighbours = placeMentManager.GetNeighbourTypesFor(temporaryPosition, CellType.Road);
            foreach(var roadposition in neighbours)
            {
                if(roadPositionsToRecheck.Contains(roadposition) == false)
                {
                    roadPositionsToRecheck.Add(roadposition);
                }
                
            }
        }
        foreach(var positionToFix in roadPositionsToRecheck)
        {
            roadFixer.FixRoadAtPosition(placeMentManager, positionToFix);
        }
    }

    public void FinishingPlacingRoad()
    {
        placementMode = false;
        placeMentManager.AddtemporaryStructureDictionary();
        if(temporaryPlacementPositions.Count > 0)
        {
            AudioPlayer.instance.PlayPlacementSound();
        }
        temporaryPlacementPositions.Clear();
        startPosition = Vector3Int.zero;
    }

}
