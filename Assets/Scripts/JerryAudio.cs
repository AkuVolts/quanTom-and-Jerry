using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JerryAudio : MonoBehaviour
{
    AudioSource audioSource;
    
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        PlayerController.OnJerryEnter += PlayAudio;
        PlayerController.OnJerryExit += StopAudio;
    }

    public void PlayAudio()
    {
        audioSource.Play();
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }
}
