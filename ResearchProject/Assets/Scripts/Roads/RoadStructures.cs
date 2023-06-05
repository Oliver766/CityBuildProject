using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadStructures : MonoBehaviour
{
    float yHeight = 0;

    public void CreateModel(GameObject model)
    {
        var structure = Instantiate(model, transform);
        yHeight = model.transform.position.y;
    }

    public void SwapModel(GameObject model, Quaternion rotation)
    {
        foreach (Transform child in transform)
        {
            Destroy(child);
        }
        var structure = Instantiate(model, transform);
        structure.transform.localPosition = new Vector3(0, yHeight, 0);
        structure.transform.localRotation = rotation;
    }
}
