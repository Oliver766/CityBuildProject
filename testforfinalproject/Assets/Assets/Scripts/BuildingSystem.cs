using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingSystem : MonoBehaviour
{
    //singleton to access the system from other scripts
    public static BuildingSystem current;

    //grid layout component
    public GridLayout gridLayout;
    //grid comonent
    private Grid grid;
    //tilemap for indication
    [SerializeField] private Tilemap MainTilemap;
    //taken tile
    [SerializeField] private TileBase whiteTile;

    //buildings prefabs
    public GameObject prefab1;
    public GameObject prefab2;

    //object that is currently being placed
    private PlaceableObject objectToPlace;

    #region Unity methods

    private void Awake()
    {
        //initialize
        current = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }

    private void Update()
    {
        //Input
        if (Input.GetKeyDown(KeyCode.A))
        {
            InitializeWithObject(prefab1);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            InitializeWithObject(prefab2);
        }

        //prevent null errors
        if (!objectToPlace)
        {
            //don't have a building to place -> return
            return;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            //rotate by new Vector3(0, 90, 0)
            objectToPlace.Rotate();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            //check placement
            if (CanBePlaced(objectToPlace))
            {
                //place
                objectToPlace.Place();
                //get the start tile position to start painting
                Vector3Int start = gridLayout.WorldToCell(objectToPlace.GetStartPosition());
                //paint on the tilemap
                TakeArea(start, objectToPlace.Size);
            }
            else
            {
                //placement not possible -> destroy the object
                Destroy(objectToPlace.gameObject);
            }
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            //cancel placement
            Destroy(objectToPlace.gameObject);
        }
    }

    #endregion

    #region Utils

    /*
     * Gem mouse position in world space with raycasting
     * @return Vector3 position
     */
    public static Vector3 GetMouseWorldPosition()
    {
        //create a ray from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //raycast
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            //return the point iy hit
            return raycastHit.point;
        }
        else
        {
            //return default position
            return Vector3.zero;
        }
    }

    /*
     * Snap a position to the grid
     * @param Vector3 position you want to snap
     * @return Vector3 snapped position
     */
    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        //convert position to cell position
        Vector3Int cellPos = gridLayout.WorldToCell(position);
        //convert the cell center position to a world position
        position = grid.GetCellCenterWorld(cellPos);
        return position;
    }

    /*
     * Gets a block of tile bases on the tilemap
     * @param BoundsInt area on which to get tiles
     * @param Tilemap tilemap from which to get tiles
     * @return TileBase[] array of tiles
     */
    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        //create an array to store tiles
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        //initialize counter
        int counter = 0;

        //go through each position withing the are
        foreach (var v in area.allPositionsWithin)
        {
            //get the position
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            //get tile and add to array
            array[counter] = tilemap.GetTile(pos);
            //increase counter
            counter++;
        }

        return array;
    }

    #endregion

    #region Building Placement

    /*
     * Instantiate an object on the map and initialize the system with it
     * @param GameObject prefab to instantiate
     */
    public void InitializeWithObject(GameObject prefab)
    {
        //get the position to instantiate at (you can pass it as a parameter too)
        Vector3 position = SnapCoordinateToGrid(Vector3.zero);

        //instantiate an object at the position
        GameObject obj = Instantiate(prefab, position, Quaternion.identity);
        //initialize the system
        objectToPlace = obj.GetComponent<PlaceableObject>();
        //add component to provide drag
        obj.AddComponent<ObjectDrag>();
    }

    /*
     * Check the area availability for object placement
     * @param PlaceableObject placeable object which is being placed
     * @return bool @true - can be placed @false - cannot be placed
     */
    private bool CanBePlaced(PlaceableObject placeableObject)
    {
        //create an area
        BoundsInt area = new BoundsInt();
        //initialize area position with object start position
        area.position = gridLayout.WorldToCell(objectToPlace.GetStartPosition());
        //initialize the size of the area
        area.size = placeableObject.Size;
        area.size = new Vector3Int(area.size.x + 1, area.size.y + 1, area.size.z);
        
        //get tiles on the area from the main tilemap
        TileBase[] baseArray = GetTilesBlock(area, MainTilemap);

        //check each tile
        foreach (var b in baseArray)
        {
            if (b == whiteTile)
            {
                //white tile means the place is taken
                return false;
            }
        }

        return true;
    }

    /*
     * Paints the tilemap with white tiles to indicate that the area is taken
     * @param Vector3Int start of the area to paint
     * @param Vector3Int size of the area to paint
     */
    public void TakeArea(Vector3Int start, Vector3Int size)
    {
        //paint tilemap
        MainTilemap.BoxFill(start, whiteTile, start.x, start.y, 
                        start.x + size.x, start.y + size.y);
    }

    #endregion
}
