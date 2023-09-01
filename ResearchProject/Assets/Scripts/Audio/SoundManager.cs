using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// script eddited by Oliver Lancashire
// sid 1901981
public class SoundManager : MonoBehaviour
{
    [Header("Audio")]
    public static SoundManager instance;

    [SerializeField] private AudioSource  effectSource;

     /// <summary>
     /// play sound
     /// </summary>
     /// <param name="clip"></param>
    public void PlaySoundClip(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }

   

}
