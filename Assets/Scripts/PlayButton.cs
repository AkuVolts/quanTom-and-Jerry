using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System;

public class PlayButton : MonoBehaviour
{
    [SerializeField] string gameSceneName = "GameScene";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeToGameScene()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}
