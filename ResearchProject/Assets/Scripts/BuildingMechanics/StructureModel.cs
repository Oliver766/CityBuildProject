using cityBuilder.AI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// script reference by Sunny Valley Studio - https://www.youtube.com/watch?v=8ayFCDbfIIM&list=PLcRSafycjWFd6YOvRE3GQqURFpIxZpErI
// script eddited by Oliver Lancashire
// sid 1901981
public class StructureModel : MonoBehaviour, INeedingRoad
{
    [Header("Float")]
    float yHeight = 0;
    public Vector3Int RoadPosition { get; set; }
    /// <summary>
    /// instatiate an empty object
    /// </summary>
    /// <param name="model"></param>
    public void CreateModel(GameObject model)
    {
        var structure = Instantiate(model, transform);
        yHeight = structure.transform.position.y;
    }
    /// <summary>
    /// swap object and change it's stats
    /// </summary>
    /// <param name="model"></param>
    /// <param name="rotation"></param>
    public void SwapModel(GameObject model, Quaternion rotation)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        var structure = Instantiate(model, transform);
        structure.transform.localPosition = new Vector3(0, yHeight, 0);
        structure.transform.localRotation = rotation;
    }
    /// <summary>
    /// find nearest game object marker transform
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public Vector3 GetNearestPedestrianMarkerTo(Vector3 position)
    {
        return transform.GetChild(0).GetComponent<RoadHelper>().GetClosestPedestrainPosition(position);
    }
    /// <summary>
    ///  find nearest game object marker transform
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public Marker GetPedestrianSpawnMarker(Vector3 position)
    {
        return transform.GetChild(0).GetComponent<RoadHelper>().GetpositioForPedestrianToSpwan(position);
    }
    /// <summary>
    /// get marker
    /// </summary>
    /// <returns></returns>
    public List<Marker> GetPedestrianMarkers()
    {
        return transform.GetChild(0).GetComponent<RoadHelper>().GetAllPedestrianMarkers();
    }
    /// <summary>
    /// get marker
    /// </summary>
    /// <returns></returns>
    internal List<Marker> GetCarMarkers()
    {
        return transform.GetChild(0).GetComponent<RoadHelper>().GetAllCarMarkers();
    }
    /// <summary>
    /// get marker nearest
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public Vector3 GetNearestCarMarkerTo(Vector3 position)
    {
        return transform.GetChild(0).GetComponent<RoadHelper>().GetClosestCarMarkerPosition(position);
    }
    /// <summary>
    /// get spawn point
    /// </summary>
    /// <param name="nextPathPosition"></param>
    /// <returns></returns>
    public Marker GetCarSpawnMarker(Vector3Int nextPathPosition)
    {
        return transform.GetChild(0).GetComponent<RoadHelper>().GetPositioForCarToSpawn(nextPathPosition);
    }
    /// <summary>
    /// end marker
    /// </summary>
    /// <param name="previousPathPosition"></param>
    /// <returns></returns>
    public Marker GetCarEndMarker(Vector3Int previousPathPosition)
    {
        return transform.GetChild(0).GetComponent<RoadHelper>().GetPositioForCarToEnd(previousPathPosition);
    }
}
