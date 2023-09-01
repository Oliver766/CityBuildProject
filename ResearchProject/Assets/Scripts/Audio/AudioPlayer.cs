// script reference by Sunny Valley Studio - https://www.youtube.com/watch?v=8ayFCDbfIIM&list=PLcRSafycjWFd6YOvRE3GQqURFpIxZpErI
// note used this script to make a audio controller

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SVS
{

    public class AudioPlayer : MonoBehaviour
    {
        [Header("Audio")]
        public AudioClip placementSound;
        public AudioSource audioSource;
        public static AudioPlayer instance;

        private void Awake()
        {
            // check there was an instance
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(this.gameObject);

        }
        /// <summary>
        /// play sound
        /// </summary>
        public void PlayPlacementSound()
        {
            if(placementSound != null)
            {
                audioSource.PlayOneShot(placementSound);
            }
        }
    }
}