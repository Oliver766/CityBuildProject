using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadFixer : MonoBehaviour
{
    public GameObject deadEnd, roadStraight, corner, threeWay, fourWay;

    public void FixRoadAtPosition(PlaceMentManager placeMentManager, Vector3Int temporaryPosition)
    {
        //[right, up,left down]
        var result = placeMentManager.GetNeighbourTypesFor(temporaryPosition);
        int roadCount = 0;
        roadCount = result.Where(x => x == CellType.Road).Count();
        if(roadCount == 0 || roadCount == 1)
        {
            CreateDeadEnd(placeMentManager, result, temporaryPosition);
        }
        else if( roadCount == 2)
        {
            if (CreateStraightRoad(placeMentManager, result, temporaryPosition))
                return;
            CreateCorner(placeMentManager, result, temporaryPosition);
        }
        else if( roadCount == 3)
        {
            Create3Way(placeMentManager, result, temporaryPosition);
        }
        else
        {
            Create4Way(placeMentManager, result, temporaryPosition);
        }
    }

    private void Create4Way(PlaceMentManager placeMentManager, CellType[] result, Vector3Int temporaryPosition)
    {
        placeMentManager.ModifyStructureModel(temporaryPosition, fourWay, Quaternion.identity);
    }

    private void Create3Way(PlaceMentManager placeMentManager, CellType[] result, Vector3Int temporaryPosition)
    {
       if(result[1] == CellType.Road && result[2] == CellType.Road && result[3] == CellType.Road)
       {
            placeMentManager.ModifyStructureModel(temporaryPosition, threeWay, Quaternion.identity);
       }
       else if(result[2] == CellType.Road && result[3] == CellType.Road && result[0] == CellType.Road)
       {
            placeMentManager.ModifyStructureModel(temporaryPosition, threeWay, Quaternion.Euler(0, 90, 0));
       }
       else if( result[3] == CellType.Road && result[0] == CellType.Road && result[1] == CellType.Road)
       {
            placeMentManager.ModifyStructureModel(temporaryPosition, threeWay, Quaternion.Euler(0, 180, 0));
       }
        else if (result[0] == CellType.Road && result[1] == CellType.Road && result[2] == CellType.Road)
        {
            placeMentManager.ModifyStructureModel(temporaryPosition, threeWay, Quaternion.Euler(0, 270, 0));
        }
    }

    private void CreateCorner(PlaceMentManager placeMentManager, CellType[] result, Vector3Int temporaryPosition)
    {
        if (result[1] == CellType.Road && result[2] == CellType.Road )
        {
            placeMentManager.ModifyStructureModel(temporaryPosition, corner, Quaternion.Euler(0,90,0));
        }
        else if (result[2] == CellType.Road && result[3] == CellType.Road )
        {
            placeMentManager.ModifyStructureModel(temporaryPosition, corner, Quaternion.Euler(0, 180, 0));
        }
        else if (result[3] == CellType.Road && result[0] == CellType.Road)
        {
            placeMentManager.ModifyStructureModel(temporaryPosition, corner, Quaternion.Euler(0, 270, 0));
        }
        else if (result[0] == CellType.Road && result[1] == CellType.Road )
        {
            placeMentManager.ModifyStructureModel(temporaryPosition, corner, Quaternion.identity);
        }
    }

    private bool CreateStraightRoad(PlaceMentManager placeMentManager, CellType[] result, Vector3Int temporaryPosition)
    {
        if (result[0] == CellType.Road && result[2] == CellType.Road )
        {
            placeMentManager.ModifyStructureModel(temporaryPosition, roadStraight, Quaternion.identity);
            return true;
        }
        else if(result[1] == CellType.Road && result[3] == CellType.Road)
        {
            placeMentManager.ModifyStructureModel(temporaryPosition, roadStraight, Quaternion.Euler(0,90,0));
            return true;
        }
        return false;
    }

    private void CreateDeadEnd(PlaceMentManager placeMentManager, CellType[] result, Vector3Int temporaryPosition)
    {
        if (result[1] == CellType.Road)
        {
            placeMentManager.ModifyStructureModel(temporaryPosition, deadEnd, Quaternion.Euler(0, 270, 0));
        }
        else if (result[2] == CellType.Road )
        {
            placeMentManager.ModifyStructureModel(temporaryPosition, deadEnd, Quaternion.identity);
        }
        else if (result[3] == CellType.Road )
        {
            placeMentManager.ModifyStructureModel(temporaryPosition, deadEnd, Quaternion.Euler(0, 90, 0));
        }
        else if (result[0] == CellType.Road )
        {
            placeMentManager.ModifyStructureModel(temporaryPosition, deadEnd, Quaternion.Euler(0,180,0));
        }
    }

   
}
