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

        // add <mspace> tagg to the text to add a monospace font
        //timerText.text = "<mspace=0.85em>" + ((int)timer) + "</mspace>" + "." + "<mspace=0.85em>" + ((int)(timer * 100) % 100).ToString() + "</mspace>";
        var timerString = Mathf.Repeat(timer, 1f).ToString();
        var timerLen = timerString.Length;
        timerText.text = "<mspace=0.85em>" + ((int)timer) + "</mspace>" + "." + "<mspace=0.85em>" + timerString.Substring(2, 2) + "</mspace>";
    }

    public void StopTimer()
    {
        isRunning = false;
    }
}
