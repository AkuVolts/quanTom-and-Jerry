using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] float timeToWin;
    [SerializeField] float decreaseRate = 1f;
    float timer = 0f;

    [SerializeField] PlayerController player;
    
    [SerializeField] UnityEvent onWin;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    public void UpdateTimer()
    {
        if (player.OnJerry)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer -= Time.deltaTime * decreaseRate;
        }
        if (HasWon())
        {
            onWin.Invoke();
        }
    }

    public bool HasWon()
    {
        return timer >= timeToWin;
    }
}