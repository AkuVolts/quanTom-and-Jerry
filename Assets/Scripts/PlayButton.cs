using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System;

public class PlayButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] string gameSceneName = "GameScene";

    void Start()
    {
        
    }

    public void ChangeToGameScene()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}
