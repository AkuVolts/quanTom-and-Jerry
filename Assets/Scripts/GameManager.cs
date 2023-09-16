using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] float timeToWin;
    [SerializeField] float decreaseRate = 1f;
    float timer = 0f;

    [SerializeField] PlayerController player;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject); 
        }
    }

    void Update()
    {
        
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
    }

    public bool HasWon()
    {
        return timer >= timeToWin;
    }
}
