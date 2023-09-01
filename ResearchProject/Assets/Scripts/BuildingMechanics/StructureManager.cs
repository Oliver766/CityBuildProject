using SVS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using cityBuilder.AI;
using UnityEngine.UI;
// script reference by Sunny Valley Studio - https://www.youtube.com/watch?v=8ayFCDbfIIM&list=PLcRSafycjWFd6YOvRE3GQqURFpIxZpErI
// script eddited by Oliver Lancashire
// sid 1901981
public class StructureManager : MonoBehaviour
{
    [Header("Arrays")]
    public StructurePrefabWeighted[] housesPrefabe, specialPrefabs, bigStructuresPrefabs;
    private float[] houseWeights, specialWeights, bigStructureWeights;
    [Header("References")]
    public PlacementManager placementManager;
    public LevelSystemv2 LevelSystemv2;
    public CurrencySystemv2 systemv2;
    public AiDirector director;
    public GameManager manager;
    public AchievementManager achievementManager;
    [Header("Floats")]
    public float HouseAmount;
    public float SpecialAmount;
    public float BigAmount;
    [Header("ints")]
    public int pop;
    [Header("bools")]
    public bool Active = false;
    public bool builder = true;
    [Header("UI")]
    public Button spawnAI;
    public Button SpawnCar;
    [Header("Vectors")]
    public Vector3 newPositionone;
    public Quaternion newRotationone;
    public Vector3 newPositiontwo;
    public Quaternion newRotationtwo;
    public Vector3 newPositionthree;
    public Quaternion newRotationthree;
    public Vector3 newPositionfive;
    public Quaternion newRotationfive;
    public Vector3 newPositionfour;
    public Quaternion newRotationfour;
    [Header("Gameobjects")]
    public GameObject cantplaceherePrefab;
    public GameObject OutofBoundsPrefab;
    public GameObject NotEmptyPrefab;
    public GameObject POP1Prefab;
    public GameObject POP2Prefab;
    [Header("Transforms")]
    public Transform Parentone;
    public Transform Parenttwo;
    public Transform Parentthree;
    public Transform Parentfour;
    public Transform Parentfive;


    private void Start()
    {
        // setting arrays
        houseWeights = housesPrefabe.Select(prefabStats => prefabStats.weight).ToArray();
        specialWeights = specialPrefabs.Select(prefabStats => prefabStats.weight).ToArray();
        bigStructureWeights = bigStructuresPrefabs.Select(prefabStats => prefabStats.weight).ToArray();

        // checking if bool is active
        if(Active == false)
        {
            spawnAI.enabled = false;
            SpawnCar.enabled = false;
        }
    }

    /// <summary>
    /// function to place random houses
    /// </summary>
    /// <param name="position"></param>
    public void PlaceHouse(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position))
        {
            int randomIndex = GetRandomWeightedIndex(houseWeights);
            placementManager.PlaceObjectOnTheMap(position, housesPrefabe[randomIndex].prefab, CellType.Structure);
            systemv2.amount = systemv2.amount - HouseAmount;
            StartCoroutine(systemv2.TakeAaway());
            manager.Population += 4;
            Instantiate(POP1Prefab, newPositionfour, newRotationfour, Parentfour);
            LevelSystemv2.currentXP += 10;
            director.SpawnAllAagents();
            director.SpawnACar();
            AudioPlayer.instance.PlayPlacementSound();
           

        }
    }

    /// <summary>
    /// function to run certain achievement
    /// </summary>
    public void Builder()
    {
        while (builder == true)
        {
            achievementManager.UnlockAchievement(AchievementID.Builder);
            LevelSystemv2.currentXP += 20;
            builder = false;
        }
       
    }
    /// <summary>
    /// function to place big structure
    /// </summary>
    /// <param name="position"></param>
    internal void PlaceBigStructure(Vector3Int position)
    {
        int width = 2;
        int height = 2;
        if(CheckBigStructure(position, width , height))
        {
            int randomIndex = GetRandomWeightedIndex(bigStructureWeights);
            placementManager.PlaceObjectOnTheMap(position, bigStructuresPrefabs[randomIndex].prefab, CellType.Structure, width, height);
            systemv2.amount = systemv2.amount - BigAmount;
            StartCoroutine(systemv2.AddCoins());
            manager.Population += 10;
            Instantiate(POP2Prefab, newPositionfive, newRotationfive, Parentfive);
            LevelSystemv2.currentXP += 40;
            director.SpawnAllAagents();
            director.SpawnACar();
            AudioPlayer.instance.PlayPlacementSound();
        }
    }

    /// <summary>
    /// function to check if building is near road or not
    /// </summary>
    /// <param name="position"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    private bool CheckBigStructure(Vector3Int position, int width, int height)
    {
        bool nearRoad = false;
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                var newPosition = position + new Vector3Int(x, 0, z);
                
                if (DefaultCheck(newPosition)==false)
                {
                    return false;
                }
                if (nearRoad == false)
                {
                    nearRoad = RoadCheck(newPosition);
                }
            }
        }
        return nearRoad;
    }

    /// <summary>
    /// function to place special building.
    /// </summary>
    /// <param name="position"></param>
    public void PlaceSpecial(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position))
        {
            int randomIndex = GetRandomWeightedIndex(specialWeights);
            placementManager.PlaceObjectOnTheMap(position, specialPrefabs[randomIndex].prefab, CellType.SpecialStructure);
            systemv2.amount = systemv2.amount - SpecialAmount;
            LevelSystemv2.currentXP += 30;
            director.SpawnAllAagents();
            director.SpawnACar();
            StartCoroutine(systemv2.AddCoins());
            AudioPlayer.instance.PlayPlacementSound();
            Active = true;
           
        }
    }
    
    public void Update()
    {
        // if bool is true then  buttons will be enabled
        if(Active == true)
        {
            spawnAI.enabled = true;
            SpawnCar.enabled = true;
        }
    }

    /// <summary>
    ///  function to randomise prefab index to spawn.
    /// </summary>
    /// <param name="weights"></param>
    /// <returns></returns>
    private int GetRandomWeightedIndex(float[] weights)
    {
        float sum = 0f;
        for (int i = 0; i < weights.Length; i++)
        {
            sum += weights[i];
        }

        float randomValue = UnityEngine.Random.Range(0, sum);
        float tempSum = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            //0->weihg[0] weight[0]->weight[1]
            if(randomValue >= tempSum && randomValue < tempSum + weights[i])
            {
                return i;
            }
            tempSum += weights[i];
        }
        return 0;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    private bool CheckPositionBeforePlacement(Vector3Int position)
    {
        if (DefaultCheck(position) == false)
        {
            return false;
        }

        if (RoadCheck(position) == false)
            return false;
        
        return true;
    }

    /// <summary>
    /// checks if buildings are near roads  or not when tried to be placed.
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    private bool RoadCheck(Vector3Int position)
    {
        if (placementManager.GetNeighboursOfTypeFor(position, CellType.Road).Count <= 0)
        {
            Debug.Log("Must be placed near a road");
            Instantiate(cantplaceherePrefab, newPositionone, newRotationone, Parentone);
            return false;
        }
        return true;
    }

    /// <summary>
    /// check if object isn't on griad or object is being placed on another object
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    private bool DefaultCheck(Vector3Int position)
    {
        if (placementManager.CheckIfPositionInBound(position) == false)
        {
            Debug.Log("This position is out of bounds");
            //Instantiate(OutofBoundsPrefab, newPositiontwo, newRotationtwo, Parenttwo);
            return false;
        }
        if (placementManager.CheckIfPositionIsFree(position) == false)
        {
            Debug.Log("This position is not EMPTY");
            Instantiate(NotEmptyPrefab, newPositionthree, newRotationthree, Parentthree);
            return false;
        }
        return true;
    }
}
// set a random number for weight.
[Serializable]
public struct StructurePrefabWeighted
{
    public GameObject prefab;
    [Range(0,1)]
    public float weight;
}
