using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StructureController : MonoBehaviour
{
    public structurePrefabWeighted[] housePrefab, specialPrefab;
    public PlaceMentManager PlaceMentManager;

    private float[] houseWeights, specialWeights;

    private void Start()
    {
        houseWeights = housePrefab.Select(preabStats => preabStats.weight).ToArray();
        specialWeights = specialPrefab.Select(preabStats => preabStats.weight).ToArray();
    }

    public void PlaceHouse(Vector3Int position)
    {
        if (checkBeforePosition(position))
        {

        }
    }

    private bool checkBeforePosition(Vector3Int position)
    {
        throw new NotImplementedException();
    }
}


[Serializable]
 public struct structurePrefabWeighted
 {
    public GameObject prefab;
    [Range(0,1)]
    public float weight;
 }
