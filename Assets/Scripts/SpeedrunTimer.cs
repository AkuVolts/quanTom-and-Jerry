using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeedrunTimer : MonoBehaviour
{
    float timer = 0f;
    [SerializeField] TMP_Text timerText;

    bool isRunning = true;

    void Update()
    {
        timer += Time.deltaTime;
        timerText.text = $"{timer.ToString("F2")}";
    }

    public void StopTimer()
    {
        isRunning = false;
    }
}
