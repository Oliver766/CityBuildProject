// script reference by Sunny Valley Studio - https://www.youtube.com/watch?v=8ayFCDbfIIM&list=PLcRSafycjWFd6YOvRE3GQqURFpIxZpErI
// note used this script to make a camera controller

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SVS
{

    public class CameraMovement : MonoBehaviour
    {
        [Header("camera")]
        public Camera gameCamera;

        [Header("float")]
        public float cameraMovementSpeed = 5;
        public float maxOrthographicSize = 5f, minOrthographicSize = 0.5f;
        public float sensitivity = 10;

        private void Start()
        {
            gameCamera = GetComponent<Camera>(); // get component
        }
        /// <summary>
        /// move camera
        /// </summary>
        /// <param name="inputVector"></param>
        public void MoveCamera(Vector3 inputVector)
        {
            var movementVector = Quaternion.Euler(0,30,0) * inputVector;
            gameCamera.transform.position += movementVector * Time.deltaTime * cameraMovementSpeed;
        }

        private void Update()
        {
            //upate camera variables
            var scrollInput = Input.GetAxis("Mouse ScrollWheel") * sensitivity;
            gameCamera.orthographicSize = Mathf.Clamp(gameCamera.orthographicSize - scrollInput, minOrthographicSize, maxOrthographicSize);
        }
    }
}