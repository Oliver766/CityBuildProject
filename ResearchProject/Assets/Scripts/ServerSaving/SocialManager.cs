using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LootLocker.Requests;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SocialManager : MonoBehaviour
{
    //file path to the neighbor save file
    public static string filePath;

    //the whole leaderboard window
    [SerializeField] private GameObject socialPanel;
    //a prefab for each player
    [SerializeField] private GameObject friendEntryPrefab;
    //content object in the scroll view
    [SerializeField] private Transform friendsContentDisplay;

    //player
    [SerializeField] private PlayerManager playerManager;

    //leaderboard key for easier access
    private string leaderboardKey = "main_leaderboard";
    //each player file in LootLocker has a purpose -> this indicates the right one for visiting
    //the purpose has to be in CAPITAL LETTERS
    private string saveFilePurpose = "PUBLIC_SAVE";

    private void Awake()
    {
        //initialize the path
        filePath = Application.dataPath + "/Saves/NeighborMap.sav";
    }

    private void Start()
    {
        //start the setup
        StartCoroutine(SetupRoutine());
    }

    private IEnumerator SetupRoutine()
    {
        //start the player session
        playerManager.StartSession();
        //wait until the player logged in
        yield return new WaitWhile(() => playerManager.playerLoggedIn == false);
        
        //submit a default score to the leaderboard
        LootLockerSDKManager.SubmitScore(playerManager.playerId.ToString(), 1, leaderboardKey, response => {});
        
        //gset up the leaderboard
        GetLeaderboard();
    }

    public void GetLeaderboard()
    {
        //leaderboard limits (players 0-50)
        int count = 50;
        int after = 0;
        
        //get the scores from the leaderboard
        LootLockerSDKManager.GetScoreList(leaderboardKey, count, after, response =>
        {
            if (response.success)
            {
                //get the leaderboard members
                var items = response.items;
                //initialize the social window with the leaderboard data
                InitializeLeaderboardUI(items);
            }
        });
    }

    public void InitializeLeaderboardUI(LootLockerLeaderboardMember[] members)
    {
        //go through each number
        foreach (var member in members)
        {
            //instantiate a player entry and add it to the scrollview
            GameObject userDisplay = Instantiate(friendEntryPrefab, friendsContentDisplay);
            //get the player from the member
            var player = member.player;
            //initialize the username
            userDisplay.transform.Find("User name").GetComponent<TextMeshProUGUI>().text = player.name;
            //initialize the visit button to start the visiting process
            userDisplay.transform.Find("Visit button").GetComponent<Button>().onClick.AddListener(delegate
            {
                //get public save files of a player
                GetSaveData(player.id);
            });
        }
    }
    
    public void GetSaveData(int playerId)
    {
        //get all public player files by id
        LootLockerSDKManager.GetAllPlayerFiles(playerId, response =>
        {
            if (response.success)
            {
                //get all the public files
                LootLockerPlayerFile[] playerFiles = response.items;
                //find the one with the right purpose
                var publicSaveFile = playerFiles.FirstOrDefault(x => x.purpose == saveFilePurpose);
                //check if it exists and start the visit
                if (publicSaveFile != null) StartCoroutine(VisitNeighbor(publicSaveFile.url));
            }
        });
    }

    private IEnumerator VisitNeighbor(string saveFileURL)
    {
        //create a web request from the file URL
        UnityWebRequest www = UnityWebRequest.Get(saveFileURL);
        //senf the request
        yield return www.SendWebRequest();
        
        //write the text you got to a file
        File.WriteAllText(filePath, www.downloadHandler.text);
        
        //refresh so the neighbor file appears
        AssetDatabase.Refresh();
        //wait for a bit
        yield return new WaitForSeconds(0.1f);

        //Load the new scene
        SceneManager.LoadScene("NeighborScene");
    }

    public void CreateSave()
    {
        //first, we have to delete the previous save
        LootLockerSDKManager.GetAllPlayerFiles(response =>
        {
            if (response.success)
            {
                //get all player files
                var playerFiles = response.items;
                //check each file
                foreach (var t in playerFiles)
                {
                    //if the purpose matches -> delete
                    if (t.purpose == saveFilePurpose)
                    {
                        //delete the file
                        LootLockerSDKManager.DeletePlayerFile(t.id, response => {});
                    }
                }
            }
        });
        
        //upload the new save file from path, give it a purpose and set it as public
        LootLockerSDKManager.UploadPlayerFile(SaveSystem.filePath, saveFilePurpose, true, response => {});
    }
    
    public void ChangePlayerName()
    {
        StartCoroutine(ChangePlayerNameRoutine());
    }

    public IEnumerator ChangePlayerNameRoutine()
    {
        // Wait until the name change is done
        yield return playerManager.SetPlayerName();
        Debug.Log("Name changed to " + playerManager.playerName);
    }
    
    public void OpenSocial()
    {
        socialPanel.SetActive(true);
    }

    public void CloseSocial()
    {
        socialPanel.SetActive(false);
    }
}
