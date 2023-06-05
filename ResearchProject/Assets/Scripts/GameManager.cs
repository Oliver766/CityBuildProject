using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SVS;
using System;

public class GameManager : MonoBehaviour
{
    public CameraMovement cameraMovement;
    public RoadManager roadManager;
    public InputManager inputManager;
    public UIController uIController;

    private void Start()
    {
        uIController.OnRoadPlacement += roadPlacementHandler;
        uIController.OnHousePlacement += HousePlacementHandler;
        uIController.OnSpecialPlacement += SpecialPlacementHandler;
       
    }

    private void SpecialPlacementHandler()
    {
        ClearInputActions();
    }

    private void HousePlacementHandler()
    {
        ClearInputActions();
    }

    private void roadPlacementHandler()
    {
        ClearInputActions();
        inputManager.OnMouseClick += roadManager.placeRoad;
        inputManager.OnMouseHold += roadManager.placeRoad;
        inputManager.OnMouseUp += roadManager.FinishingPlacingRoad;

    }

    private void ClearInputActions()
    {
        inputManager.OnMouseClick = null;
        inputManager.OnMouseHold = null;
        inputManager.OnMouseUp = null;
    }

    private void Update()
    {
        cameraMovement.MoveCamera(new Vector3(inputManager.CameraMovementVector.x,0,
            inputManager.CameraMovementVector.y));
    }
}
