using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    //player id given by LootLocker
    public int playerId { get; private set; }
    //unique device id used to start the session for the player
    public string playerIdentifier { get; private set; }
    
    //indicator that player logged in successfully 
    public bool playerLoggedIn;
    
    //The name of the player
    public string playerName;
    
    // The placeholder text in the input field
    public TextMeshProUGUI inputFieldText;
    
    // The input field for the player to change their name
    public TMP_InputField playerNameInputField;

    [Obsolete]
    public void StartSession()
    {
        //get the device id
        playerIdentifier = SystemInfo.deviceUniqueIdentifier;
        //start the LootLocker session for the player
        LootLockerSDKManager.StartSession(playerIdentifier, response =>
        {
            //if it was successful
            if (response.success)
            {
                //get the player id from LootLocker
                playerId = response.player_id;
                //player successfully logged in
                playerLoggedIn = true;
                
                //try to get the name of the player
                TrySetPlayerNameToInputField();
            }
        });
    }

    public void TrySetPlayerNameToInputField()
    {
        //get the player name from LootLocker
        LootLockerSDKManager.GetPlayerName((response) =>
        {
            if (response.success)
            {
                //if the name is not empty
                if (response.name != "")
                {
                    //set the name text in the input field
                    playerNameInputField.text = response.name;
                }
            }
        });
    }
    
    public IEnumerator SetPlayerName()
    {
        //set the player's name from the input field
        LootLockerSDKManager.SetPlayerName(inputFieldText.text, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully set player name");
                //set the player name field
                playerName = inputFieldText.text;
            }
            else
            {
                Debug.Log("Error setting player name");
            }
        });
        
        yield return null;
    }
}
