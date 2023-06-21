using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NeighborhoodManager : MonoBehaviour
{
    private SaveData neighborSave;

    private void Start()
    {
        //get the text from the neighbor's save file
        string saveString = File.ReadAllText(SocialManager.filePath);
        //load a save object from that text
        neighborSave = JsonConvert.DeserializeObject<SaveData>(saveString);
        //if it is not null
        if (neighborSave != null)
        {
            //load the saved map
            LoadData();
        }
    }

    private void LoadData()
    {
        //go through each saved data
        foreach (var plObjData in neighborSave.placeableObjectDatas.Values)
        {
            //try-catch block in case something wasn't saved properly
            //to avoid errors
            try
            {
                //get the ShopItem from resources
                ShopItem item = Resources.Load<ShopItem>(GameManager.shopItemsPath + "/" + plObjData.assetName);
                //instantiate the prefab
                GameObject obj = BuildingSystem.current.InitializeWithObject(item.Prefab, plObjData.position, true);
                //get the placeable object component
                PlaceableObject plObj = obj.GetComponent<PlaceableObject>();
                //initialize the component
                plObj.Initialize(item, plObjData);
                //load to finalize placement
                plObj.Load();
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                //throw;
            }
        }
    }

    public void Return()
    {
        SceneManager.LoadScene("MainScene");
    }
}
