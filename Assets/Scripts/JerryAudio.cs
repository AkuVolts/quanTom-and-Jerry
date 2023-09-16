using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JerryAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    void OnEnable()
    {
        PlayerController.OnJerryEnter += PlayAudio;
        PlayerController.OnJerryExit += StopAudio;
    }

    void OnDisable()
    {
        PlayerController.OnJerryEnter -= PlayAudio;
        PlayerController.OnJerryExit -= StopAudio;
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
