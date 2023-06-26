using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    public bool Placed { get; private set; }
    //area under the building
    public Vector3Int Size { get; private set; }
    //vertices from the bottom square of the collider
    private Vector3[] Vertices;

    /*
     * Get the local positions of the bottom square of an object's collider
     */
    private void GetColliderVertexPositionsLocal()
    {
        //get the collider component
        BoxCollider b = gameObject.GetComponent<BoxCollider>();
        //create an array
        Vertices = new Vector3[4];
        //get positions (in local space)
        Vertices[0] = b.center + new Vector3(-b.size.x, -b.size.y, -b.size.z) * 0.5f;
        Vertices[1] = b.center + new Vector3(b.size.x, -b.size.y, -b.size.z) * 0.5f;
        Vertices[2] = b.center + new Vector3(b.size.x, -b.size.y, b.size.z) * 0.5f;
        Vertices[3] = b.center + new Vector3(-b.size.x, -b.size.y, b.size.z) * 0.5f;
    }

    /*
     * Calculate the size of the collider in cells
     */
    private void CalculateSizeInCells()
    {
        //create an array
        Vector3Int[] vertices = new Vector3Int[Vertices.Length];
        
        //initialize the array
        for (int i = 0; i < vertices.Length; i++)
        {
            //convert a vertex to world space
            Vector3 worldPos = transform.TransformPoint(Vertices[i]);
            //get the cell position and initialize the array element
            vertices[i] = BuildingSystem.current.gridLayout.WorldToCell(worldPos);
        }

        //calculate the size
        Size = new Vector3Int(Math.Abs((vertices[0] - vertices[1]).x), 
                                Math.Abs((vertices[0] - vertices[3]).y), 
                                1);
    }
    
    /*
     * Gets the position of the first vertex in global space
     */
    public Vector3 GetStartPosition()
    {
        return transform.TransformPoint(Vertices[0]);
    }

    private void Start()
    {
        //initialization
        GetColliderVertexPositionsLocal();
        CalculateSizeInCells();
    }

    /*
     * Rotates an object by 90 degrees counter-clockwise
     */
    public void Rotate()
    {
        //rotate the object
        transform.Rotate(new Vector3(0, 90, 0));
        //rotate the size
        Size = new Vector3Int(Size.y, Size.x, 1);

        //shift vertices
        Vector3[] vertices = new Vector3[Vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = Vertices[(i + 1) % Vertices.Length];
        }

        //assign new vertices
        Vertices = vertices;
    }
    
    /*
     * Places an object onto the map
     * (made virtual because you might need to override it in the game)
     */
    public virtual void Place()
    {
        //get the object drag component
        ObjectDrag drag = gameObject.GetComponent<ObjectDrag>();
        //destroy object drag
        Destroy(drag);
        
        Placed = true;
        
        //invoke events of placement
    }
}
