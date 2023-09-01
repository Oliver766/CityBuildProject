using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// script reference by Sunny Valley Studio - https://www.youtube.com/watch?v=8ayFCDbfIIM&list=PLcRSafycjWFd6YOvRE3GQqURFpIxZpErI
// script eddited by Oliver Lancashire
// sid 1901981
public class UIController : MonoBehaviour
{
    // actions
    public Action OnRoadPlacement, OnHousePlacement, OnSpecialPlacement, OnBigStructurePlacement;
    public Button placeRoadButton, placeHouseButton, placeSpecialButton, placeBigStructureButton;
    [Header("Color")]
    public Color outlineColor;
    [Header("UI")]
    List<Button> buttonList;
    [Header("References")]
    public CurrencySystemv2 currency;

    private void Start()
    {
        // set slist
        buttonList = new List<Button> { placeHouseButton, placeRoadButton, placeSpecialButton, placeBigStructureButton };
        // add listner
        placeRoadButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeRoadButton);
            OnRoadPlacement?.Invoke();
           

        });
        // add listner
        placeHouseButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeHouseButton);
            OnHousePlacement?.Invoke();

        });
        // add listner
        placeSpecialButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeSpecialButton);
            OnSpecialPlacement?.Invoke();

        });
        // add listner
        placeBigStructureButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeBigStructureButton);
            OnBigStructurePlacement?.Invoke();

        });
    }
    /// <summary>
    /// change outline of button
    /// </summary>
    /// <param name="button"></param>
    private void ModifyOutline(Button button)
    {
        var outline = button.GetComponent<Outline>();
        outline.effectColor = outlineColor;
        outline.enabled = true;
    }
    /// <summary>
    /// reset button color
    /// </summary>
    public void ResetButtonColor()
    {
        foreach (Button button in buttonList)
        {
            button.GetComponent<Outline>().enabled = false;
        
        }
    }
}
