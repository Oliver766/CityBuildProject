using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
// script reference by Sunny Valley Studio - https://www.youtube.com/watch?v=8ayFCDbfIIM&list=PLcRSafycjWFd6YOvRE3GQqURFpIxZpErI
// script eddited by Oliver Lancashire
// sid 1901981
public class InputManager : MonoBehaviour
{
    public event Action<Ray> OnMouseClick, OnMouseHold;
    public event Action OnMouseUp, OnEscape;
    private Vector2 mouseMovementVector = Vector2.zero;
    public Vector2 CameraMovementVector { get => mouseMovementVector; }
    [SerializeField]
    [Header("Camera")]
    Camera mainCamera;
    [Header("Gameobject")]
    public GameObject canceledPrefab;
    [Header("vector")]
    public Vector3 newPositiontwo;
    public Quaternion newRotationtwo;
    [Header("Transform")]
    public Transform Parenttwo;

    void Update()
    {
        // updated and run function events
        CheckClickDownEvent();
        CheckClickHoldEvent();
        CheckClickUpEvent();
        CheckArrowInput();
        CheckEscClick();
    }

    //function for click hold event
    private void CheckClickHoldEvent()
    {
        if (Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {

            OnMouseClick?.Invoke(mainCamera.ScreenPointToRay(Input.mousePosition));
        }
    }

    // function to check to click up
    private void CheckClickUpEvent()
    {
        if (Input.GetMouseButtonUp(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            OnMouseUp?.Invoke();
        }
    }
    /// <summary>
    /// function to check click down event
    /// </summary>
    private void CheckClickDownEvent()
    {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            OnMouseClick?.Invoke(mainCamera.ScreenPointToRay(Input.mousePosition));
        }
    }
    /// <summary>
    ///  function that cancels all input
    /// </summary>
    private void CheckEscClick()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnEscape.Invoke();
            Debug.Log("Building Mode canceled");
            Instantiate(canceledPrefab, newPositiontwo, newRotationtwo, Parenttwo);
        }
    }
    /// <summary>
    /// check if player is moving with keys
    /// </summary>
    private void CheckArrowInput()
    {
        mouseMovementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
    /// <summary>
    /// function that cancels all possible input actions
    /// </summary>
    public void ClearEvents()
    {
        OnMouseClick = null;
        OnMouseHold = null;
        OnEscape = null;
        OnMouseUp = null;
        Debug.Log("Building Mode canceled");
     
    }
}
