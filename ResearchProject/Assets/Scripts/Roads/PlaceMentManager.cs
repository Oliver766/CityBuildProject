using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceMentManager : MonoBehaviour
{
    public int width, height;
    Grid placementGrid;
    private Dictionary<Vector3Int, RoadStructures> temporaryRoadObjects = new Dictionary<Vector3Int, RoadStructures>();
    private Dictionary<Vector3Int, RoadStructures> structureDictionary = new Dictionary<Vector3Int, RoadStructures>();

    private void Start()
    {
        placementGrid = new Grid(width, height);
    }

    internal CellType[] GetNeighbourTypesFor(Vector3Int position)
    {
       return placementGrid.GetAllAdjacentCellTypes(position.x, position.z);
    }

    internal bool checkIfPoistionInBound(Vector3Int position)
    {
        if(position.x >= 0 && position.x < width && position.z >= 0 && position.z < height)
        {
            return true;
        }
        return false;
    }

    internal bool CheckIfPositionIsFree(Vector3Int position)
    {
        return CheckIfPositionIsOfType(position, CellType.Empty);
    }

    private bool CheckIfPositionIsOfType(Vector3Int position, CellType type)
    {
        return placementGrid[position.x, position.z] == type;
    }

    internal void PlaceTemporaryStructure(Vector3Int position, GameObject RoadPrefab, CellType type)
    {
        placementGrid[position.x, position.z] = type;
        RoadStructures structure = CreateNewStructureModel(position, RoadPrefab, type);
        temporaryRoadObjects.Add(position, structure);
    }

    internal List<Vector3Int>GetPathbetween(Vector3Int startPosition, Vector3Int Endposition)
    {
        var resultPath = GridSearch.AStarSearch(placementGrid, new Point(startPosition.x, startPosition.z), new Point
            (Endposition.x, Endposition.z));
        List<Vector3Int> path = new List<Vector3Int>();
        foreach(Point point in resultPath)
        {
            path.Add(new Vector3Int(point.X,0, point.Y));
        }
        return path;
    }

    internal void RemoveAllTemporaryStructures()
    {
        foreach( var structure in temporaryRoadObjects.Values)
        {
            var position = Vector3Int.RoundToInt(structure.transform.position);
            placementGrid[position.x, position.z] = CellType.Empty;
            Destroy(structure.gameObject);
        }
        temporaryRoadObjects.Clear();
    }

    internal List<Vector3Int> GetNeighbourTypesFor(Vector3Int temporaryPosition, CellType type)
    {
        var neighboursVertices = placementGrid.GetAdjacentCellsOfType(temporaryPosition.x, temporaryPosition.z, type);
        List<Vector3Int> neighbours = new List<Vector3Int>();
        foreach(var point in neighboursVertices)
        {
            neighbours.Add(new Vector3Int(point.X, 0, point.Y));
        }
        return neighbours;
    }

    private RoadStructures CreateNewStructureModel(Vector3Int position, GameObject roadPrefab, CellType type)
    {
        GameObject structure = new GameObject(type.ToString());
        structure.transform.SetParent(transform);
        structure.transform.localPosition = position;
        var roadModel = structure.AddComponent<RoadStructures>();
        roadModel.CreateModel(roadPrefab);
        return roadModel;
    }

    internal void AddtemporaryStructureDictionary()
    {
        foreach(var structure in temporaryRoadObjects)
        {
            structureDictionary.Add(structure.Key, structure.Value);
        }
        temporaryRoadObjects.Clear();
    }

    public void ModifyStructureModel(Vector3Int position, GameObject newModel, Quaternion rotation)
    {
        if (temporaryRoadObjects.ContainsKey(position))
        {
            temporaryRoadObjects[position].SwapModel(newModel, rotation);
        }
        else if (structureDictionary.ContainsKey(position))
            structureDictionary[position].SwapModel(newModel, rotation);
    }

}

   
